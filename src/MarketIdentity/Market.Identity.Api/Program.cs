using AutoMapper.Internal;
using Market.Identity.Api;
using Market.Identity.Api.Data;
using Market.Identity.Api.Email;
using Market.Shared;
using Market.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder();
if (builder.Environment.IsEnvironment("Docker"))
{
    builder.Configuration.AddUserSecrets(typeof(DatabaseSeedWorker).Assembly);
}

IdentityModelEventSource.ShowPII = true;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("auth-db");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString,
            optionsBuilder => { optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null); });
    //options.UseOpenIddict();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(mce =>
{
    mce.Internal().MethodMappingEnabled = false;
}, AppDomain.CurrentDomain.GetAssemblies());

// Register ASP.NET Core Identity services
builder.Services
    .AddDefaultIdentity<AuthUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<AuthRole>()
    .AddRoleManager<RoleManager<AuthRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddEmail(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddSharedDependencies();
builder.Services.AddSharedServices();

// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
builder.Services.AddQuartz(options =>
{
    //options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

builder.Services.AddOpenIddict()

    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        // Configure OpenIddict to use the Entity Framework Core stores and models.
        // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
        options.UseEntityFrameworkCore()
            .UseDbContext<ApplicationDbContext>();

        // Enable Quartz.NET integration.
        options.UseQuartz();
    })

    // Register the OpenIddict server components.
    .AddServer(options =>
    {
        // Enable the authorization, logout, token and userinfo endpoints.
        options
            .SetAuthorizationEndpointUris("connect/authorize")
            .SetDeviceEndpointUris("connect/device")
            .SetVerificationEndpointUris("connect/verify")
            .SetLogoutEndpointUris("connect/logout")
            .SetTokenEndpointUris("connect/token")
            .SetUserinfoEndpointUris("connect/userinfo");

        // Mark the "email", "profile" and "roles" scopes as supported scopes.
        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

        // Disable access token encryption, as it is not used in Identity Server sample
        options.DisableAccessTokenEncryption();

        // Enable necessary flows
        options.AllowClientCredentialsFlow();
        options.AllowDeviceCodeFlow();
        options.AllowAuthorizationCodeFlow();
        options.AllowRefreshTokenFlow();

        // Register the signing and encryption credentials.
        // TODO: Use proper certificates for non-development environment
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserinfoEndpointPassthrough()
            .EnableVerificationEndpointPassthrough()
            .EnableStatusCodePagesIntegration();
    })

    // Register the OpenIddict validation components.
    .AddValidation(options =>
    {
        // Import the configuration from the local OpenIddict server instance.
        options.UseLocalServer();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

// Register the worker responsible for seeding the database.
// Note: in a real world application, this step should be part of a setup script.
builder.Services.AddHostedService<DatabaseSeedWorker>();

builder.Services.AddAuthentication();
builder.Services.AddApplicationServices();

// CORS policy to allow SwaggerUI and React clients
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            "default", policy =>
            {
                policy
                    .WithOrigins(
                        builder.Configuration.GetServiceUri("product-catalog-api")!.ToString().TrimEnd('/'),
                        builder.Configuration.GetServiceUri("ui-client")!.ToString().TrimEnd('/'))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("default");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();


public partial class Program { }