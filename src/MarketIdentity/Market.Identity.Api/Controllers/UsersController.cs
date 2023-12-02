using Market.Identity.Api.Application.Users.Commands;
using Market.Identity.Api.Application.Users.Models;
using Market.Identity.Api.Application.Users.Queries;
using Market.Shared.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Identity.Api.Controllers;

[Route("api/v{version:apiVersion}/users")]
[Produces("application/json")]
[ApiVersion("1.0")]
public class UsersController : ApiControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] AddUserCommand addUserCommand)
    {
        return Ok(await Mediator.Send(addUserCommand));
    }
    
    [HttpGet("{pageNumber}/{pageSize}")]
    public async Task<ActionResult<PaginatedList<UserViewModel>>> GetUsers([FromQuery] int pageNumber, 
        [FromQuery]  int pageSize)
    {
        return Ok(await Mediator.Send(new GetUsersQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        }));
    }    
    
    [HttpPut("update-email")]
    public async Task<ActionResult> UpdateUserEmail([FromBody] ChangeUserEmailCommand changeUserEmailCommand)
    {
        return Ok(await Mediator.Send(changeUserEmailCommand));
    }
    
    [HttpPut("update-password")]
    public async Task<ActionResult> UpdateUserPassword([FromBody] ChangeUserPasswordCommand changeUserPasswordCommand)
    {
        return Ok(await Mediator.Send(changeUserPasswordCommand));
    } 
    
}