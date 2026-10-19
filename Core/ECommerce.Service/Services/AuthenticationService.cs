using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Identity;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var User = await userManager.FindByEmailAsync(dto.Email) ?? throw new UserNotFound(dto.Email);

            var ValidePassword = await userManager.CheckPasswordAsync(User , dto.Password);

            if (ValidePassword)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = "Token - ToDo."
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }

        }

        public Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
