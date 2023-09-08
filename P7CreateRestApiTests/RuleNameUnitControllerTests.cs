using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApiTests
{
    public class RuleNameUnitControllerTests
    {
        [Fact]
        public async Task GetRuleNames_ReturnsOkResult_WhenRuleNamesExist()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            var fakeRuleNames = new List<RuleName>
        {
            new RuleName { RuleNameId = 1, /* other properties */ },
            new RuleName { RuleNameId = 2, /* other properties */ },
        };

            mockRepository.Setup(repo => repo.GetAllRuleNames()).ReturnsAsync(fakeRuleNames);

            // Act
            var result = await controller.GetRuleNames();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var ruleNames = Assert.IsAssignableFrom<IEnumerable<RuleName>>(okResult.Value);
            Assert.Equal(2, ruleNames.Count());
        }

        [Fact]
        public async Task GetRuleName_ReturnsOkResult_WhenRuleNameExists()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            var fakeRuleName = new RuleName { RuleNameId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync(fakeRuleName);

            // Act
            var result = await controller.GetRuleName(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var ruleName = Assert.IsAssignableFrom<RuleName>(okResult.Value);
            Assert.Equal(1, ruleName.RuleNameId);
        }

        [Fact]
        public async Task GetRuleName_ReturnsNotFound_WhenRuleNameDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync((RuleName)null);

            // Act
            var result = await controller.GetRuleName(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutRuleName_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            var fakeRuleName = new RuleName { RuleNameId = 1, /* other properties */ };
            var idToMismatch = 2;

            // Act
            var result = await controller.PutRuleName(idToMismatch, fakeRuleName);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutRuleName_ReturnsNoContent_WhenRuleNameUpdatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            var fakeRuleName = new RuleName { RuleNameId = 1, /* other properties */ };

            mockRepository.Setup(repo => repo.UpdateRuleNameAsync(fakeRuleName)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutRuleName(1, fakeRuleName);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostRuleName_ReturnsCreatedAtAction_WhenRuleNameCreatedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            var fakeRuleName = new RuleName { /* initialize with necessary properties */ };

            mockRepository.Setup(repo => repo.CreateRuleNameAsync(fakeRuleName)).ReturnsAsync(1); // Assuming a new ID is generated.

            // Act
            var result = await controller.PostRuleName(fakeRuleName);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetRuleName", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteRuleName_ReturnsNoContent_WhenRuleNameDeletedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var controller = new RuleNamesController(mockRepository.Object);

            var ruleNameIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteRuleNameAsync(ruleNameIdToDelete)).ReturnsAsync(1); // Assuming one row is affected.

            // Act
            var result = await controller.DeleteRuleName(ruleNameIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}