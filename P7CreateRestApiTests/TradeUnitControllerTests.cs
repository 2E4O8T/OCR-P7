using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApiTests
{
    public class TradeUnitControllerTests
    {
        [Fact]
        public async Task GetTrades_ReturnsOkResult_WhenTradesExist()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            var fakeTrades = new List<Trade>
        {
            new Trade { TradeId = 1, /* other properties */ },
            new Trade { TradeId = 2, /* other properties */ },
        };

            mockRepository.Setup(repo => repo.GetAllTrades()).ReturnsAsync(fakeTrades);

            // Act
            var result = await controller.GetTrades();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var trades = Assert.IsAssignableFrom<IEnumerable<Trade>>(okResult.Value);
            Assert.Equal(2, trades.Count());
        }

        [Fact]
        public async Task GetTrade_ReturnsOkResult_WhenTradeExists()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            var fakeTrade = new Trade { TradeId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync(fakeTrade);

            // Act
            var result = await controller.GetTrade(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var trade = Assert.IsAssignableFrom<Trade>(okResult.Value);
            Assert.Equal(1, trade.TradeId);
        }

        [Fact]
        public async Task GetTrade_ReturnsNotFound_WhenTradeDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync((Trade)null);

            // Act
            var result = await controller.GetTrade(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutTrade_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            var fakeTrade = new Trade { TradeId = 1, /* other properties */ };
            var idToMismatch = 2;

            // Act
            var result = await controller.PutTrade(idToMismatch, fakeTrade);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutTrade_ReturnsNoContent_WhenTradeUpdatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            var fakeTrade = new Trade { TradeId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.UpdateTradeAsync(fakeTrade)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutTrade(1, fakeTrade);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostTrade_ReturnsCreatedAtAction_WhenTradeCreatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            var fakeTrade = new Trade { /* initialize with necessary properties */ };

            mockRepository.Setup(repo => repo.CreateTradeAsync(fakeTrade)).ReturnsAsync(1); // Assuming a new ID is generated.

            // Act
            var result = await controller.PostTrade(fakeTrade);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetTrade", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteTrade_ReturnsNoContent_WhenTradeDeletedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradesController(mockRepository.Object);

            var tradeIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteTradeAsync(tradeIdToDelete)).ReturnsAsync(1); // Assuming one row is affected.

            // Act
            var result = await controller.DeleteTrade(tradeIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}