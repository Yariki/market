
using Market.EventBus.RabbitMq;
using Market.Shared.CorrelationId;
using Market.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSharedServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();
builder.Services.AddCorrelationIdServices();

builder.Services.AddServiceBus(builder.Configuration);
builder.Services.AddEventBusSender();

builder.Services.AddControllers();

builder.Services.AddHealthChecks();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration.GetServiceUri("identity-api")!.ToString().TrimEnd('/');
        options.MapInboundClaims = true;
        options.Audience = "basket-api";
        options.BackchannelHttpHandler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        options.TokenValidationParameters = new TokenValidationParameters() { ValidateAudience = true };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("basket-api", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "basket-api");
    });
});

// CORS policy to allow the React sample client to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "default", policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    app.UseOpenApi();
    app.UseSwaggerUi3();
   
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.AddCorrelationMiddleware();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("default");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();

public partial class Program { }