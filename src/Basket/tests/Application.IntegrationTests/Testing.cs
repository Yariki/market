﻿using Basket.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;

namespace Basket.Application.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static CustomWebApplicationFactory _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Respawner _checkpoint = null!;
    private static string? _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public static async Task ResetState()
    {
        throw new NotImplementedException();
    }

    public static async Task<Domain.Entities.Basket?> FindAsync(string userId)
    {
        using var scope = _scopeFactory.CreateScope();

        var basketRepository = scope.ServiceProvider.GetRequiredService<IBasketRepository>();

        return await basketRepository.GetBasketAsync(userId) ;
    }

    public static async Task UpdateAsync(Domain.Entities.Basket entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var basketRepository = scope.ServiceProvider.GetRequiredService<IBasketRepository>();

        await basketRepository.UpdateBasketAsync(entity);

    }

    public static async Task DeleteAsync(string userId)
    {
        using var scope = _scopeFactory.CreateScope();

        var basketRepository = scope.ServiceProvider.GetRequiredService<IBasketRepository>();

        await basketRepository.DeleteBasketAsync(userId);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}
