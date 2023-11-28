using AutoMapper;
using FluentValidation;
using Market.Identity.Api.Data;
using Market.Shared.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Application.Users.Commands;

public class AddUserCommand : IRequest<string>
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ProfileImageName { get; set; } = string.Empty;
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, string>
{
    private readonly UserManager<AuthUser> _userManager;
    private readonly IMapper _mapper;

    public AddUserCommandHandler(UserManager<AuthUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<string> Handle(AddUserCommand request,
        CancellationToken cancellationToken)
    {
        var newUser = new AuthUser()
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ProfileImageName = request.ProfileImageName
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            throw new MarketException(result.Errors.Count() > 0 ? result.Errors.First().Description : "Error while creating user");
        }

        return newUser.Id;
    }
}

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}