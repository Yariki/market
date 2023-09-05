using System.Data.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProductCatalog.Application.IntegrationTests.Data;
using ProductCatalog.Infrastructure.Persistence;
using Respawn;
using Respawn.Graph;

namespace ProductCatalog.Application.IntegrationTests;

[SetUpFixture]
public partial class AppicationTesting
{
    private static ProductCatalogApplicationFactory _factory = null;
    private static IConfiguration _configuration = null;

    public static IServiceScopeFactory ScopeFactory { get; private set; } = null!;

    private static string _currentUserId = null!;


    private static Respawner _respawner = null;
    private static DbConnection _connection = null;

    [OneTimeSetUp]
    public void RunBeforeAnyTest()
    {
        _factory = new ProductCatalogApplicationFactory();
        ScopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        InitiRespawner().GetAwaiter().GetResult();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        _factory?.Dispose();
        _factory = null;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = ScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = ScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public static async Task SeedDataAsync()
    {
        using var scope = ScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();
        await SeedData.SeedAdditionalDataAsync(context);
    }

    private static async Task InitiRespawner()
    {
        var repawnerOptions = new RespawnerOptions()
        {
            TablesToIgnore = new Table[] { "__EFMigrationsHistory" },
            DbAdapter = DbAdapter.SqlServer,
        };
        using var scope = ScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();
        _connection = context.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, repawnerOptions);
    }

    public static async Task ResetState()
    {
        using var scope = ScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();
        _connection = context.Database.GetDbConnection();
        await _connection.OpenAsync();
        await _respawner.ResetAsync(_connection);
    }

}
