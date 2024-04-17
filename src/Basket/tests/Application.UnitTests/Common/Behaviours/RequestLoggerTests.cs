using Basket.Application.Common.Behaviours;
using Basket.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Basket.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    //private Mock<ILogger<CreateSampleItemCommand>> _logger = null!;
    //private Mock<ICurrentUserService> _currentUserService = null!;

    //[SetUp]
    //public void Setup()
    //{
    //    _logger = new Mock<ILogger<CreateSampleItemCommand>>();
    //    _currentUserService = new Mock<ICurrentUserService>();
    //}

    //[Test]
    //public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    //{
    //    _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

    //    var requestLogger = new LoggingBehaviour<CreateSampleItemCommand>(_logger.Object, _currentUserService.Object);

    //    await requestLogger.Process(new CreateSampleItemCommand { Title = "title" }, new CancellationToken());

    //}

    //[Test]
    //public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    //{
    //    var requestLogger = new LoggingBehaviour<CreateSampleItemCommand>(_logger.Object, _currentUserService.Object);

    //    await requestLogger.Process(new CreateSampleItemCommand { Title = "title" }, new CancellationToken());

    //}
}
