using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var User = await serviceManager.authenticationService.LoginAsync(dto);

            return Ok(User);

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var User = await serviceManager.authenticationService.RegisterAsync(dto);

            return Ok(User);

        }
    }
}
