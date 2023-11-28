using System.Data.Common;
using FluentAssertions;
using Market.Identity.Api.Application.Users.Commands;
using Market.Identity.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Respawn;
using Respawn.Graph;

namespace Market.Identity.Api.IntegrationTests;

[SetUpFixture]
public partial class IndentityApplicationTesting
{
    private static IdentityApplicationFactory _factory = null!;
    private static IConfiguration _configuration = null!;

    public static IServiceScopeFactory ScopeFactory { get; private set; } = null!;

    private static string _currentUserId = null!;

    private static Respawner _respawner = null!;
    private static DbConnection _connection = null!;

    [OneTimeSetUp]
    public void RunBEforeAnyTest()
    {
        _factory = new IdentityApplicationFactory();
        ScopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        InitiRespawner().GetAwaiter().GetResult();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTest()
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

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static Task SeedDataAsync()
    {
        return Task.CompletedTask;
    }

    public static async Task<string> AddUserAsync(AddUserCommand addUser)
    {
        var userId = await SendAsync(addUser);
        userId.Should().NotBeNull();

        return userId;
    }

    private static async Task InitiRespawner()
    {
        var respawnerOptions = new RespawnerOptions()
        {
            TablesToIgnore = new Table[] { "__EFMigrationsHistory" },
            DbAdapter = DbAdapter.SqlServer,
        };
        using var scope = ScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _connection = context.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);
    }

    public static async Task ResetState()
    {
        using var scope = ScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _connection = context.Database.GetDbConnection();
        await _connection.OpenAsync();
        await _respawner.ResetAsync(_connection);
    }

}