using Microsoft.AspNetCore.Mvc;
using Models;
using Services.ServiceMethods;

namespace DerivcoDeveloperAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger<UserController> logger;
        IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(UserModel model)
        {
            var result = await userService.InsertUser(model);
            return Ok(result);
        }
    }
}
