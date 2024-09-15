using Market.Identity.Api.Data;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Market.Identity.Api;

// Note: in a real world application, this should be part of a setup script.
public class DatabaseSeedWorker : IHostedService
{
    private const string ProductCatalogApiScope = Permissions.Prefixes.Scope + "product-catalog-api";
    private const string BasketApiScope = Permissions.Prefixes.Scope + "basket-api";

    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public DatabaseSeedWorker(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var clientUrl = _configuration.GetServiceUri("ui-client")!.ToString();

        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        if (await scopeManager.FindByNameAsync("product-catalog-api", cancellationToken) == null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = "product-catalog-api",
                Description = "Product Catalog API resource",
                DisplayName = "Product Catalog API"
            }, cancellationToken);
        }
        if(await scopeManager.FindByNameAsync("basket-api", cancellationToken) == null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = "basket-api",
                Description = "Basket API resource",
                DisplayName = "Basket API"
            }, cancellationToken);
        }
        var clientId = "ui-client";
        if (await applicationManager.FindByClientIdAsync(clientId, cancellationToken) == null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = clientId,
                DisplayName = "UI Client",
                RedirectUris =
                {
                    new Uri($"{clientUrl}signin-oidc")
                },
                PostLogoutRedirectUris =
                {
                    new Uri($"{clientUrl}signout-callback-oidc")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Scopes.OpenId,
                    Scopes.OfflineAccess,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    ProductCatalogApiScope,
                    BasketApiScope
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}