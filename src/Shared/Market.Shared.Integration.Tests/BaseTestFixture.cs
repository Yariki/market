namespace Market.Shared.Integration.Tests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetTestState();
    }

    protected virtual async Task ResetTestState()
    {

    }
}

