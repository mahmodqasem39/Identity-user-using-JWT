using JWT.Models;

namespace JWT.Services
{
    public interface IUserService
    {
        Task<UserModel> Register(RegisterModel resource, CancellationToken cancellationToken);
        Task<LoginResponse> Login(LoginModel resource, CancellationToken cancellationToken);

    }
}
