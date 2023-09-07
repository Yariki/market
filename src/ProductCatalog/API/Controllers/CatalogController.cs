using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Catalogs.Commands.AddCatalog;
using ProductCatalog.Application.Catalogs.Commands.DeleteCatalog;
using ProductCatalog.Application.Catalogs.Commands.UpdateCatalog;
using ProductCatalog.Application.Catalogs.Models;
using ProductCatalog.Application.Catalogs.Queries.GetCatalogs;
using ProductCatalog.WebUI.Filters;

namespace ProductCatalog.WebUI.Controllers;

[ApiExceptionFilter]
[Route("api/v{version:apiVersion}/catalog")]
[ApiVersion("1.0")]
public class CatalogController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogs()
    {
        return Ok(await Mediator.Send(new GetCatalogsQuery()));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> AddCatalog(AddCatalogCommand addCatalogCommand)
    {
        var guid = await Mediator.Send(addCatalogCommand);
        return Ok(guid);
    }

    [HttpPut("{catalogId:guid}")]
    public async Task<ActionResult<Guid>> UpdateCatalog(Guid catalogId, [FromBody] UpdateCatalogCommand updateCatalogCommand)
    {
        var updatedCatalogId = await Mediator.Send(updateCatalogCommand);
        return Ok(updatedCatalogId);
    }


    [HttpDelete("{catalogId:guid}")]
    public async Task<ActionResult> DeleteCatalog(Guid catalogId)
    {
        await Mediator.Send(new DeleteCatalogCommand() { Id = catalogId });

        return Ok();
    }
    
    
}