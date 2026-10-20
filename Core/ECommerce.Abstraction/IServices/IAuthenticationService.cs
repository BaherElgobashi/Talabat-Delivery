using ECommerce.Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto dto);
        Task<UserDto> RegisterAsync(RegisterDto dto);

        Task<bool> CheckEmailAsync(string Email);

        Task<AddressDto> GetCurrentUserAddress(string Email);

        Task<AddressDto> UpdateCurrentUserAddressAsync(string Email , AddressDto dto);

        Task<UserDto> GetCurrentUserAsync(string Email);

    }
}
