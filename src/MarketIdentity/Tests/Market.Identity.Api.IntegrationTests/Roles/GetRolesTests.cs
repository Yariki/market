using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Market.Identity.Api.Application.Roles.Commands;
using Market.Identity.Api.Application.Roles.Queries;
using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests.Roles;
public class GetRolesTests : IdentityApplicationBaseFixture
{

    [Test]
    public async Task GetRoles_Empty()
    {
        var get = new GetRolesQuery() { PageNumber = 1, PageSize = 10 };

        var get2 = await SendAsync(get);

        get2.Should().NotBeNull();
        get2.Items.Should().NotBeNull();
        get2.Items.Should().BeEmpty();
    }

    [Test]
    public async Task GetRoles_Success()
    {
        await AddRolesAsync();

        var get = new GetRolesQuery() { PageNumber = 1, PageSize = 10 };

        var get2 = await SendAsync(get);

        get2.Should().NotBeNull();
        get2.Items.Should().NotBeNull();
        get2.Items.Count().Should().Be(2);
    }

    private async Task AddRolesAsync()
    {
        var addCommand = new AddRoleCommand() { RoleName = "User" };
        await SendAsync(addCommand);

        addCommand = new AddRoleCommand() { RoleName = "Admin" };
        await SendAsync(addCommand);

    }


}
