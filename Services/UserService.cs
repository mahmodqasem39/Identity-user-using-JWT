using JWT.EF;
using JWT.Models;
using JWT.Utilities;

namespace JWT.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        //private readonly AppSettings _appSettings;
        private readonly int _iteration = 3;

        public UserService(AppDbContext appDbContext/*, AppSettings appSettings*/)
        {
            _context = appDbContext;
            //_appSettings = appSettings;
        }

        public async Task<RegisterModel> Register(RegisterModel resource, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = resource.Username,
                Email = resource.Email,
                FullName= resource.FullName,
                PasswordSalt = PasswordHasher.GenerateSalt()
            };

            user.HashedPassword = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, "Pas5pr@se", _iteration);
            await _context.Users.AddAsync(user,cancellationToken);
            await _context.SaveChangesAsync();

            return null;//new UserResource(user.Id, user.Username, user.Email);
        }
    }
}
