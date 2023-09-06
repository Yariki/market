using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Catalogs.Commands.AddCatalog;

namespace ProductCatalog.WebUI.Controllers;

public class CatalogController : ApiControllerBase
{

    [HttpPost()]
    public async Task<ActionResult<Guid>> AddCatalog(AddCatalogCommand addCatalogCommand)
    {
        var guid = await Mediator.Send(addCatalogCommand);

        return Ok(guid);
    }
    
}