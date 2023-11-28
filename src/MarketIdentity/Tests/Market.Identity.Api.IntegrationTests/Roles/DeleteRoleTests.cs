using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Market.Identity.Api.Application.Roles.Commands;
using Market.Identity.Api.Data;
using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests.Roles;
public class DeleteRoleTests : IdentityApplicationBaseFixture
{
    [Test]
    public async Task DeleteRole_ValidationFailed()
    {
        var deleteRole = new DeleteRoleCommand();

        await FluentActions
            .Invoking(() => SendAsync(deleteRole))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task DeleteRole_Success()
    {
        var roleName = "User";
        var roleId = await CreateRoleAsync(roleName);

        var deleteRole = new DeleteRoleCommand() { Id = roleId };

        await SendAsync(deleteRole);

        var role = await FindAsync<AuthRole>(roleId);

        role.Should().BeNull();
    }

    private async Task<string> CreateRoleAsync(string roleName)
    {
        var addCommand = new AddRoleCommand() { RoleName = roleName };

        var result = await SendAsync(addCommand);

        result.Should().NotBeNull();

        return result;
    }
}
