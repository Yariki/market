
using FluentAssertions;
using FluentValidation;
using Market.Identity.Api.Application.Roles.Commands;
using Market.Identity.Api.Data;
using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests.Roles;

public class AddRoleTests : IdentityApplicationBaseFixture
{
    [Test]
    public async Task AddRole_ValidationFailed()
    {
        var add = new AddRoleCommand();
        
        await FluentActions.Invoking(() => SendAsync(add))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task AddRole_Success()
    {
        var addCommand = new AddRoleCommand() { RoleName = "User" };

        var result = await SendAsync(addCommand);

        result.Should().NotBeNull();

        var newRole = await FindAsync<AuthRole>(result);

        newRole.Should().NotBeNull();
        newRole.Name.Should().Be("User");
    }
}