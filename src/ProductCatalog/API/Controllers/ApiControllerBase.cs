using ProductCatalog.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.WebUI.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
