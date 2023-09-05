using Market.Shared.Integration.Tests;

using static ProductCatalog.Application.IntegrationTests.AppicationTesting;

namespace ProductCatalog.Application.IntegrationTests;
public class ApplicationBaseFixture : BaseTestFixture
{
    protected override async Task ResetTestState()
    {
        ResetState().GetAwaiter().GetResult();
        SeedDataAsync().GetAwaiter().GetResult();
    }
}
