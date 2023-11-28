using FluentValidation;
using Market.Identity.Api.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Roles.Commands;

public class AddRoleCommand : IRequest<string>
{
    public string RoleName { get; set; }
}

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, string>
{
    private readonly RoleManager<AuthRole> _roleManager;

    public AddRoleCommandHandler(RoleManager<AuthRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<string> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new AuthRole(request.RoleName);
        await _roleManager.CreateAsync(role);
        return role.Id;
    }
}

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty();
    }
}

