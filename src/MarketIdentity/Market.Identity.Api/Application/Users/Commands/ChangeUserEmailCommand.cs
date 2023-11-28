using FluentValidation;
using Market.Identity.Api.Data;
using Market.Shared.Application.Exceptions;
using Market.Shared.Infrastructure.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Users.Commands;

public class ChangeUserEmailCommand : IRequest<Unit>
{
    public string UserId { get; set; }
    
    public string NewEmail { get; set; }
}

public class ChangeUserEmailCommandHandler : IRequestHandler<ChangeUserEmailCommand, Unit>
{
    private readonly UserManager<AuthUser> _userManager;

    public ChangeUserEmailCommandHandler(UserManager<AuthUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user.IsNull())
        {
            throw new NotFoundException($"The user with id: {request.UserId} was not found");
        }

        await _userManager.ChangeEmailAsync(user, request.NewEmail, await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail));

        return Unit.Value;
    }
}

public class ChangeUserEmailCommandValidator : AbstractValidator<ChangeUserEmailCommand>
{
    public ChangeUserEmailCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.NewEmail).NotEmpty().EmailAddress();
    }
}





