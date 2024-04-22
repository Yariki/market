using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Market.Shared.CorrelationId;
public static class AppBuilderExtensions
{

    public static IApplicationBuilder AddCorrelationMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();

        return applicationBuilder;
    }

}
