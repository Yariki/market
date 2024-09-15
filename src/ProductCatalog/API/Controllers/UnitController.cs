using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Units.Commands.AddUnit;
using ProductCatalog.Application.Units.Commands.DeleteUnit;
using ProductCatalog.Application.Units.Commands.UpdateUnit;
using ProductCatalog.Application.Units.Models;
using ProductCatalog.Application.Units.Queries;
using ProductCatalog.WebUI.Controllers;
using ProductCatalog.WebUI.Filters;

namespace API.Controllers;

[ApiExceptionFilter]
[Route("api/v{version:apiVersion}/unit")]
[ApiVersion("1.0")]
[Produces("application/json")]
//[Authorize(Policy = "product-catalog-api")]
public class UnitController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitDto>>> GetUnits()
    {
        return Ok(await Mediator.Send(new GetUnitQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddUnit([FromBody] AddUnitCommand addUnitCommand)
    {
        var guid = await Mediator.Send(addUnitCommand);
        return Ok(guid);
    }

    [HttpPut("{unitId:guid}")]
    public async Task<ActionResult<Guid>> UpdateUnit(Guid unitId, [FromBody] UpdateUnitCommand updateUnitCommand)
    {
        var updatedUnitId = await Mediator.Send(updateUnitCommand);
        return Ok(updatedUnitId);
    }

    [HttpDelete("{unitId:guid}")]
    public async Task<ActionResult> DeleteUnit(Guid unitId)
    {
        await Mediator.Send(new DeleteUnitCommand() { UnitId = unitId });

        return Ok();
    }

}
