using FluentAssertions;
using FluentValidation;
using Market.Identity.Api.Application.Users.Commands;
using Market.Identity.Api.Data;
using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests.Users;
public class ChangeUserEmailTests : IdentityApplicationBaseFixture
{
    [Test]
    public async Task ChangeUserEmail_UserIdValidationFailed()
    {
        var changeUserEmail = new ChangeUserEmailCommand()
        {
            UserId = string.Empty,
            NewEmail = "test@test.com"
        };

        await FluentActions
            .Invoking(() => SendAsync(changeUserEmail))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ChangeUserEmail_EmailEmptyValidationFailed()
    {
        var changeUserEmail = new ChangeUserEmailCommand()
        {
            UserId = Guid.NewGuid().ToString(),
            NewEmail = string.Empty
        };

        await FluentActions
            .Invoking(() => SendAsync(changeUserEmail))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ChangeUserEmail_EmailNotValidValidationFailed()
    {
        var changeUserEmail = new ChangeUserEmailCommand()
        {
            UserId = Guid.NewGuid().ToString(),
            NewEmail = "test.com"
        };

        await FluentActions
            .Invoking(() => SendAsync(changeUserEmail))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ChangeEmail_Success()
    {
        var userId = await AddUserAsync(new AddUserCommand()
        {
            UserName = "testuser",
            Email = "test@test.com",
            Password = "Test123!",
            FirstName = "test",
            LastName = "test"
        });

        var changeUserEmail = new ChangeUserEmailCommand()
        {
            UserId = userId,
            NewEmail = "testuser@test.com"
        };

        await SendAsync(changeUserEmail);

        var user = await FindAsync<AuthUser>(userId);

        user.Should().NotBeNull();

        user.Email.Should().Be(changeUserEmail.NewEmail);
    }





}
