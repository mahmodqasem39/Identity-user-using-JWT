using JWT.Models;

namespace JWT.Services
{
    public interface IUserService
    {
        Task<RegisterModel> Register(RegisterModel resource, CancellationToken cancellationToken);
    }
}
