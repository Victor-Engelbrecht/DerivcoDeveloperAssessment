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
        IBetService betService;
        IRouletteService rouletteService; 
        IUserService userService;
        public PlayController(ILogger<PlayController> logger, IBetService betService,IRouletteService rouletteService, IUserService userService)
        {
            this.logger = logger;
            this.betService = betService;
            this.rouletteService = rouletteService;
            this.userService = userService;
        }

        // GET: api/<PlayController>
        [HttpPost("PlaceBet")]
        public async Task<IActionResult> PlaceBet(BetModel bet)
        {

            var result = await betService.PlaceBet(bet);
            return Ok(result);
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
            var result= await rouletteService.Spin();
            return Ok(result);
        }

        // DELETE api/<PlayController>/5
        [HttpPost("Payout")]
        public void Payout(UserModel user)
        {
            var result = betService.Payout(user);
        }

        // DELETE api/<PlayController>/5
        [HttpPost("ShowPreviousSpins")]
        public void ShowPreviousSpins(UserModel user)
        {
        }
    }
}
