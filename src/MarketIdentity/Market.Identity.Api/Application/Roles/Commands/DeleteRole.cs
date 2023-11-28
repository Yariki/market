using FluentValidation;
using Market.Identity.Api.Data;
using Market.Shared.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Roles.Commands;

public class DeleteRoleCommand : IRequest<Unit>
{
    public string Id { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly RoleManager<AuthRole> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<AuthRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        if (role is null)
            throw new NotFoundException(nameof(AuthRole), request.Id);
        await _roleManager.DeleteAsync(role);
        return Unit.Value;
    }
}

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
