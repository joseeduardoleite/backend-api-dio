using WebAPI.Models.Users;

namespace WebAPI.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserViewModelOutput userViewModelOutput);
    }
}