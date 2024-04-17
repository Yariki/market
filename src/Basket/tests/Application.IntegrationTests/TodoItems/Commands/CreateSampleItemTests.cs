using Basket.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Basket.Application.IntegrationTests.SampleItems.Commands;

using static Testing;

public class CreateTodoItemTests : BaseTestFixture
{
    //[Test]
    //public async Task ShouldRequireMinimumFields()
    //{
    //    var command = new CreateSampleItemCommand();

    //    await FluentActions.Invoking(() =>
    //        SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task ShouldCreateTodoItem()
    //{
    //    var command = new CreateSampleItemCommand
    //    {
    //        Title = "Tasks"
    //    };

    //    var itemId = await SendAsync(command);

    //    var item = await FindAsync<SampleItem>(itemId);

    //    item.Should().NotBeNull();
    //    item.Title.Should().Be(command.Title);
    //    item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    //    item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    //}
}
