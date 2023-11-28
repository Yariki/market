using FluentValidation;
using Market.Identity.Api.Data;
using Market.Shared.Application.Exceptions;
using Market.Shared.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Users.Commands;

public class ChangeUserPasswordCommand : IRequest<Unit>
{
    public string UserId { get; set; }
    
    public string OldPassword { get; set; }
    
    public string NewPassword { get; set; }
}

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Unit>
{
    private readonly UserManager<AuthUser> _userManager;

    public ChangeUserPasswordCommandHandler(UserManager<AuthUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException(nameof(AuthUser), request.UserId);
        }

        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new MarketException(result.Errors.Count() > 0 ? result.Errors.First().Description : "Problem changing password");
        }

        return Unit.Value;
    }
}

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.OldPassword).NotEmpty();
        RuleFor(v => v.NewPassword).NotEmpty();
    }
}