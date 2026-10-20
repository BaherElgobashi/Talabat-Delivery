using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Identity;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthenticationService(UserManager<ApplicationUser> userManager , IConfiguration configuration , IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
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
                    Token = await CreateTokenAsync(User)
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
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                // Validation.

                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto dto)
        {
            var User = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email == Email)
                            ?? throw new UserNotFound(Email);

            if(User.Address is not null)
            {
                User.Address.Street = dto.Street;
                User.Address.City = dto.City;
                User.Address.Country = dto.Country;
                User.Address.FirstName = dto.FirstName;
                User.Address.LastName = dto.LastName;
            }
            else
            {
                User.Address = mapper.Map< AddressDto, Address>(dto);
            }

            await userManager.UpdateAsync(User);

            return mapper.Map<Address,AddressDto>(User.Address);
        }

        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await userManager.FindByEmailAsync(Email);

            //return User is not null;
            return User is null ? false : true;
            
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var User = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email ==Email);

            if(User is not null)
            {
                return mapper.Map<Address , AddressDto>(User.Address);
            }
            else
            {
                throw new AddressNotFoundException(User.DisplayName);
            }
        }

        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
            var User = await userManager.FindByEmailAsync(Email);   
            if(User is not null)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User),

                };
            }
            else
            {
                throw new UserNotFound(Email);
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

            var SecurityKey = configuration.GetSection("JWTOptions")["SecurityKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                    issuer : configuration.GetSection("JWTOptions")["Issuer"],
                    audience : configuration.GetSection("JWTOptions")["Audience"],
                    claims : UserClaims,
                    expires : DateTime.Now.AddDays(2),
                    signingCredentials : Creds

                );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

    }
}
