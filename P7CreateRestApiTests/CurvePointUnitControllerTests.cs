using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApiTests
{
    public class CurvePointUnitControllerTests
    {
        [Fact]
        public async Task GetCurvePoints_ReturnsOkResult_WhenCurvePointsExist()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            var fakeCurvePoints = new List<CurvePoint>
        {
            new CurvePoint { CurvePointId = 1, /* other properties */ },
            new CurvePoint { CurvePointId = 2, /* other properties */ },
        };

            mockRepository.Setup(repo => repo.GetAllCurvePoints()).ReturnsAsync(fakeCurvePoints);

            // Act
            var result = await controller.GetCurvePoints();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var curvePoints = Assert.IsAssignableFrom<IEnumerable<CurvePoint>>(okResult.Value);
            Assert.Equal(2, curvePoints.Count());
        }

        [Fact]
        public async Task GetCurvePoint_ReturnsOkResult_WhenCurvePointExists()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            var fakeCurvePoint = new CurvePoint { CurvePointId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync(fakeCurvePoint);

            // Act
            var result = await controller.GetCurvePoint(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var curvePoint = Assert.IsAssignableFrom<CurvePoint>(okResult.Value);
            Assert.Equal(1, curvePoint.CurvePointId);
        }

        [Fact]
        public async Task GetCurvePoint_ReturnsNotFound_WhenCurvePointDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await controller.GetCurvePoint(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutCurvePoint_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            var fakeCurvePoint = new CurvePoint { CurvePointId = 1, /* other properties */ };
            var idToMismatch = 2;

            // Act
            var result = await controller.PutCurvePoint(idToMismatch, fakeCurvePoint);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutCurvePoint_ReturnsNoContent_WhenCurvePointUpdatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            var fakeCurvePoint = new CurvePoint { CurvePointId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.UpdateCurvePointAsync(fakeCurvePoint)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutCurvePoint(1, fakeCurvePoint);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostCurvePoint_ReturnsCreatedAtAction_WhenCurvePointCreatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            var fakeCurvePoint = new CurvePoint { /* initialize with necessary properties */ };

            mockRepository.Setup(repo => repo.CreateCurvePointAsync(fakeCurvePoint)).ReturnsAsync(1); // Assuming a new ID is generated.

            // Act
            var result = await controller.PostCurvePoint(fakeCurvePoint);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetCurvePoint", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteCurvePoint_ReturnsNoContent_WhenCurvePointDeletedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockRepository.Object);

            var curvePointIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteCurvePointAsync(curvePointIdToDelete)).ReturnsAsync(1); // Assuming one row is affected.

            // Act
            var result = await controller.DeleteCurvePoint(curvePointIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}