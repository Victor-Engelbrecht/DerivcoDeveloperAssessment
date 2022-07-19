using Microsoft.AspNetCore.Mvc;
using Models;
using Services.ServiceMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DerivcoDeveloperAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        ILogger<PlayController> logger;
        IUserService userService;
        public PlayController(ILogger<PlayController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        // GET: api/<PlayController>
        [HttpGet("PlaceBet")]
        public async Task<IActionResult> PlaceBet(BetModel bet)
        {

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // GET api/<PlayController>/5
        [HttpGet("{number1},{number2}")]
        public async Task<IActionResult> Get(int number1, int number2)
        {
            var number = await userService.Add(number1, number2);
            return Ok(new {number = number });
        }


        // PUT api/<PlayController>/5
        [HttpGet("Spin")]
        public async Task<IActionResult> Spin()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE api/<PlayController>/5
        [HttpGet("Payout")]
        public void Payout(UserModel user)
        {
        }

        // DELETE api/<PlayController>/5
        [HttpGet("ShowPreviousSpins")]
        public void ShowPreviousSpins(UserModel user)
        {
        }
    }
}
