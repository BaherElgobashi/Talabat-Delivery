using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await serviceManager.authenticationService.CheckEmailAsync(Email);

            return Ok(Result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var AppUser = await serviceManager.authenticationService.GetCurrentUserAsync(Email);

            return Ok(AppUser);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var Address = await serviceManager.authenticationService.GetCurrentUserAddressAsync(Email);

            return Ok(Address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto dto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var UpdatedAddress = await serviceManager.authenticationService.UpdateCurrentUserAddressAsync(Email , dto);

            return Ok(UpdatedAddress);
        }


    }
}
