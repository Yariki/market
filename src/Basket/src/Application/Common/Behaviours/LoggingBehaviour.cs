﻿using Basket.Application.Common.Interfaces;
using Market.Shared.Application.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;

        _logger.LogInformation("Basket Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);
    }
}