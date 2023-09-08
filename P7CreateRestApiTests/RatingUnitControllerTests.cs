using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApiTests
{
    public class RatingUnitControllerTests
    {
        [Fact]
        public async Task GetRatings_ReturnsOkResult_WhenRatingsExist()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            var fakeRatings = new List<Rating>
        {
            new Rating { RatingId = 1, /* other properties */ },
            new Rating { RatingId = 2, /* other properties */ },
        };

            mockRepository.Setup(repo => repo.GetAllRatings()).ReturnsAsync(fakeRatings);

            // Act
            var result = await controller.GetRatings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var ratings = Assert.IsAssignableFrom<IEnumerable<Rating>>(okResult.Value);
            Assert.Equal(2, ratings.Count());
        }

        [Fact]
        public async Task GetRating_ReturnsOkResult_WhenRatingExists()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            var fakeRating = new Rating { RatingId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync(fakeRating);

            // Act
            var result = await controller.GetRating(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var rating = Assert.IsAssignableFrom<Rating>(okResult.Value);
            Assert.Equal(1, rating.RatingId);
        }

        [Fact]
        public async Task GetRating_ReturnsNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync((Rating)null);

            // Act
            var result = await controller.GetRating(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutRating_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            var fakeRating = new Rating { RatingId = 1, /* other properties */ };
            var idToMismatch = 2;

            // Act
            var result = await controller.PutRating(idToMismatch, fakeRating);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutRating_ReturnsNoContent_WhenRatingUpdatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            var fakeRating = new Rating { RatingId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.UpdateRatingAsync(fakeRating)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutRating(1, fakeRating);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostRating_ReturnsCreatedAtAction_WhenRatingCreatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            var fakeRating = new Rating { /* initialize with necessary properties */ };

            mockRepository.Setup(repo => repo.CreateRatingAsync(fakeRating)).ReturnsAsync(1); // Assuming a new ID is generated.

            // Act
            var result = await controller.PostRating(fakeRating);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetRating", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteRating_ReturnsNoContent_WhenRatingDeletedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingsController(mockRepository.Object);

            var ratingIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteRatingAsync(ratingIdToDelete)).ReturnsAsync(1); // Assuming one row is affected.

            // Act
            var result = await controller.DeleteRating(ratingIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}