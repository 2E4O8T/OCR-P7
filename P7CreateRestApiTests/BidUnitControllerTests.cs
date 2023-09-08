using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApiTests
{
    public class BidUnitControllerTests
    {
        //[Fact]
        //public async Task GetBids_ReturnsOkResult_WhenBidsExist()
        //{
        //    // Arrange
        //    var mockRepository = new Mock<IBidRepository>();
        //    var controller = new BidsController(mockRepository.Object);

        //    var fakeBids = new List<Bid>
        //    {
        //    new Bid { BidId = 1, /* other properties */ },
        //    new Bid { BidId = 2, /* other properties */ },
        //};

        //    mockRepository.Setup(repo => repo.GetAllBids()).ReturnsAsync(fakeBids);

        //    // Act
        //    var result = await controller.GetBids();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var bids = Assert.IsAssignableFrom<IEnumerable<Bid>>(okResult.Value);
        //    Assert.Equal(2, bids.Count());
        //}

        //[Fact]
        //public async Task GetBid_ReturnsOkResult_WhenBidExists()
        //{
        //    // Arrange
        //    var mockRepository = new Mock<IBidRepository>();
        //    var controller = new BidsController(mockRepository.Object);

        //    var fakeBid = new Bid { BidId = 1, /* other properties */ };

        //    mockRepository.Setup(repo => repo.GetBidByIdAsync(1)).ReturnsAsync(fakeBid);

        //    // Act
        //    var result = await controller.GetBid(1);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var bid = Assert.IsAssignableFrom<Bid>(okResult.Value);
        //    Assert.Equal(1, bid.BidId);
        //}

        //[Fact]
        //public async Task GetBid_ReturnsNotFound_WhenBidDoesNotExist()
        //{
        //    // Arrange
        //    var mockRepository = new Mock<IBidRepository>();
        //    var controller = new BidsController(mockRepository.Object);

        //    mockRepository.Setup(repo => repo.GetBidByIdAsync(1)).ReturnsAsync((Bid)null);

        //    // Act
        //    var result = await controller.GetBid(1);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        [Fact]
        public async Task GetBids_ReturnsOkResult_WhenBidsExist()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            var fakeBids = new List<Bid>
        {
            new Bid { BidId = 1, /* other properties */ },
            new Bid { BidId = 2, /* other properties */ },
        };

            mockRepository.Setup(repo => repo.GetAllBids()).ReturnsAsync(fakeBids);

            // Act
            var result = await controller.GetBids();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var bids = Assert.IsAssignableFrom<IEnumerable<Bid>>(okResult.Value);
            Assert.Equal(2, bids.Count());
        }

        [Fact]
        public async Task GetBid_ReturnsOkResult_WhenBidExists()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            var fakeBid = new Bid { BidId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.GetBidByIdAsync(1)).ReturnsAsync(fakeBid);

            // Act
            var result = await controller.GetBid(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var bid = Assert.IsAssignableFrom<Bid>(okResult.Value);
            Assert.Equal(1, bid.BidId);
        }

        [Fact]
        public async Task GetBid_ReturnsNotFound_WhenBidDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetBidByIdAsync(1)).ReturnsAsync((Bid)null);

            // Act
            var result = await controller.GetBid(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutBid_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            var fakeBid = new Bid { BidId = 1, /* other properties */ };
            var idToMismatch = 2;

            // Act
            var result = await controller.PutBid(idToMismatch, fakeBid);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutBid_ReturnsNoContent_WhenBidUpdatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            var fakeBid = new Bid { BidId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.UpdateBidAsync(fakeBid)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutBid(1, fakeBid);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostBid_ReturnsCreatedAtAction_WhenBidCreatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            var fakeBid = new Bid { /* initialize with necessary properties */ };

            mockRepository.Setup(repo => repo.CreateBidAsync(fakeBid)).ReturnsAsync(1); // Assuming a new ID is generated.

            // Act
            var result = await controller.PostBid(fakeBid);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBid", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteBid_ReturnsNoContent_WhenBidDeletedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IBidRepository>();
            var controller = new BidsController(mockRepository.Object);

            var bidIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteBidAsync(bidIdToDelete)).ReturnsAsync(1); // Assuming one row is affected.

            // Act
            var result = await controller.DeleteBid(bidIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}