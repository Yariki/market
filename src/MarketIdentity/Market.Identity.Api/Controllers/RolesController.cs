using Market.Identity.Api.Application.Roles.Commands;
using Market.Identity.Api.Application.Roles.Models;
using Market.Identity.Api.Application.Roles.Queries;
using Market.Shared.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Identity.Api.Controllers;


[Route("api/v{version:apiVersion}/roles")]
[Produces("application/json")]
[ApiVersion("1.0")]
public class RolesController : ApiControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] AddRoleCommand addRoleCommand)
    {
        return Ok(await Mediator.Send(addRoleCommand));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] string id)
    {
        return Ok(await Mediator.Send(new DeleteRoleCommand(){Id = id}));;
    }
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<RoleViewModel>>> GetRoles([FromBody] GetRolesQuery getRolesQuery)
    {
        return Ok(await Mediator.Send(getRolesQuery));
    }

}
