using Orders.Application.Common.Behaviours;
using Orders.Application.Common.Interfaces;
using Orders.Application.SampleItems.Commands.CreateSampleItem;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Orders.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateSampleItemCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateSampleItemCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
    }
}
