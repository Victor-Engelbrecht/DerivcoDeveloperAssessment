using DerivcoDeveloperAssessment.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Services.ServiceMethods;
using Xunit;

namespace DerivcoDeveloperAssessment.Tests
{
    public class UserServiceTests
    {
        public readonly Mock<ILogger<PlayController>> _logger = new Mock<ILogger<PlayController>>();
        public readonly Mock<IBetService> _betService = new Mock<IBetService>();
        public readonly Mock<IRouletteService> _rouletteService = new Mock<IRouletteService>();
        public readonly Mock<IUserService> _userService = new Mock<IUserService>();

        [Theory]
        [InlineData(4,3, "{ number = 7 }")]
        [InlineData(10,5, "{ number = 15 }")]
        public async Task AdditionTest(int x, int y, string expected)
        {
            var controller = new PlayController(_logger.Object, _betService.Object, _rouletteService.Object, _userService.Object);
            var something = _userService.Setup(a => a.Add(x, y)).Returns(Task.Run(() => x + y));

            OkResult statuscode = new OkResult();

            // Act
            var result = await controller.Get(x, y);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(statuscode.StatusCode, okResult.StatusCode);
            Assert.Equal(expected, okResult.Value.ToString());
        }

        [Fact]
        public async Task PlaceBet()
        {
            BetModel model = new BetModel()
            {
                UserId = 1,
                Amount = 5000,
                BetOnNumber =20
            };

            var controller = new PlayController(_logger.Object, _betService.Object, _rouletteService.Object, _userService.Object);
            //var something = _betService.Setup(a => a.PlaceBet(model)).Returns();

            OkResult statuscode = new OkResult();

            // Act
            var result = await controller.PlaceBet(model);
            var okResult = result as OkObjectResult;
            var test = okResult.Value.ToString();
            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(statuscode.StatusCode, okResult.StatusCode);
            Assert.Equal("hi", okResult.Value.ToString());
        }


    }
}
