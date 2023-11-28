using FluentAssertions;
using FluentValidation;
using Market.Identity.Api.Application.Users.Commands;
using Market.Identity.Api.Data;
using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests.Users;
public class ChangeUserPasswordTests
{
    [Test]
    public async Task ChangeUserPassword_UserIdValidationFailed()
    {
        var changePass = new ChangeUserPasswordCommand()
        {
            UserId = "",
            OldPassword = "oldPass",
            NewPassword = "newPass"
        };

        await FluentActions
            .Invoking(() => SendAsync(changePass))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ChangeUserPassword_OldPasswordValidationFailed()
    {
        var changePass = new ChangeUserPasswordCommand()
        {
            UserId = Guid.NewGuid().ToString(),
            OldPassword = string.Empty,
            NewPassword = "newPass"
        };

        await FluentActions
            .Invoking(() => SendAsync(changePass))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ChangeUserPassword_NewPasswordValidationFailed()
    {
        var changePass = new ChangeUserPasswordCommand()
        {
            UserId = Guid.NewGuid().ToString(),
            OldPassword = "oldpass",
            NewPassword = string.Empty
        };

        await FluentActions
            .Invoking(() => SendAsync(changePass))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ChangeUserPassword_Success()
    {
        var userId = await AddUserAsync(new AddUserCommand()
        {
            UserName = "testuser",
            Email = "test@test.com",
            Password = "Test123!",
            FirstName = "test",
            LastName = "test"
        });

        var user = await FindAsync<AuthUser>(userId);

        user.Should().NotBeNull();
        var tempOldPass = user.PasswordHash;

        var changePassword = new ChangeUserPasswordCommand()
        {
            UserId = userId,
            OldPassword = "Test123!",
            NewPassword = "Test1234!"
        };

        await SendAsync(changePassword);

        user = await FindAsync<AuthUser>(userId);

        user.Should().NotBeNull();
        user.PasswordHash.Should().NotBeNullOrEmpty();
        user.PasswordHash.Should().NotBe(tempOldPass);
    }

}

