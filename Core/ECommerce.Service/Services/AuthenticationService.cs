using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Identity;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.UserName
            };

            var Result = await userManager.CreateAsync(User , dto.Password);

            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    Email = User.Email,
                    DisplayName = User.Email,
                    Token = "Token - ToDo."
                };
            }
            else
            {
                // Validation.

                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var UserClaims = new List<Claim>()
            {
                new (ClaimTypes.Email , user.Email),
                new (ClaimTypes.Name , user.UserName),
                new (ClaimTypes.NameIdentifier , user.Id),
            };

            var Roles = await userManager.GetRolesAsync(user);

            foreach(var Role in Roles)
            {
                UserClaims.Add(new Claim(ClaimTypes.Role , Role));
            }

            var SecurityKey = "MySecurityKey";

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                    issuer : "MyIssuer",
                    audience : "MyAudience",
                    claims : UserClaims,
                    expires : DateTime.Now.AddDays(2),
                    signingCredentials : Creds

                );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

    }
}
