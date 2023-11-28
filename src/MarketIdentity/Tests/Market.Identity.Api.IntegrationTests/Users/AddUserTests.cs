using FluentAssertions;
using FluentValidation;
using Market.Identity.Api.Application.Users.Commands;
using Market.Identity.Api.Data;
using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests.Users;
public class AddUserTests : IdentityApplicationBaseFixture
{

    [Test]
    public async Task AddUser_ValidationFailed()
    {
        var addUser = new AddUserCommand();

        await FluentActions
            .Invoking(() => SendAsync(addUser))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task AddUser_Success()
    {
        var addUSer = new AddUserCommand()
        {
            UserName = "test",
            Email = "test@test.com",
            Password = "Test123!",
            FirstName = "test",
            LastName = "test"
        };

        var userId = await SendAsync(addUSer);
        userId.Should().NotBeNull();

        var user = await FindAsync<AuthUser>(userId);

        user.Should().NotBeNull();
        user.UserName.Should().Be(addUSer.UserName);
    }

}
