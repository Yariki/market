using Market.Shared.Integration.Tests;

using static Market.Identity.Api.IntegrationTests.IndentityApplicationTesting;

namespace Market.Identity.Api.IntegrationTests;

public class IdentityApplicationBaseFixture : BaseTestFixture
{
    protected override async Task ResetTestState()
    {
        ResetState().GetAwaiter().GetResult();
        SeedDataAsync().GetAwaiter().GetResult();
    }
}