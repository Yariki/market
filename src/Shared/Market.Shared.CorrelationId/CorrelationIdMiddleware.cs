using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Market.Shared.CorrelationId;
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    private const string CorrelationIdHeader = "X-Correlation-Id";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICorrelationIdService correlationIdService)
    {
        var correlationId = GetCorrelationId(context, correlationIdService);
        AddCorrelationIdHeader(context, correlationId);
        await _next(context);
    }

    private string GetCorrelationId(HttpContext context, ICorrelationIdService correlationIdService)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            correlationIdService.Set(correlationId);
            return correlationId;
        }

        return correlationIdService.Get();
    }

    private static void AddCorrelationIdHeader(HttpContext context, string correlationId)
    {
        context.Response.OnStarting(() =>         {
            context.Response.Headers.Add(CorrelationIdHeader, correlationId);
            return Task.CompletedTask;
        });
    }


}
