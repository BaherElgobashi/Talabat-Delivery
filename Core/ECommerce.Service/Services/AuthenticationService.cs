using ECommerce.Abstraction.IServices;
using ECommerce.Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<UserDto> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
