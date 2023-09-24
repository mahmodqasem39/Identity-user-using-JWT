using JWT.EF;
using JWT.Models;
using JWT.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        //private readonly AppSettings _appSettings;
        private readonly int _iteration = 3;
        private readonly string? _secret;

        public UserService(AppDbContext appDbContext/*, AppSettings appSettings*/, IConfiguration configuration)
        {
            _context = appDbContext;
            _configuration = configuration;
            _secret = _configuration.GetValue<string>("Secret");
        }


        public async Task<UserModel> Register(RegisterModel resource, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = resource.Username,
                Email = resource.Email,
                FullName= resource.FullName,
                PasswordSalt = PasswordHasher.GenerateSalt()
            };

            user.HashedPassword = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt,_iteration);
            await _context.Users.AddAsync(user,cancellationToken);
            await _context.SaveChangesAsync();

            return new UserModel(user.UserId, user.UserName, user.Email,user.FullName);
        }

        public async Task<LoginResponse> Login(LoginModel resource, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == resource.Username);
            if (user == null)
                throw new Exception("Invalid UserName Or Password");

            var password = PasswordHasher.ComputeHash(resource.Password, user?.PasswordSalt, _iteration);

            if (user.HashedPassword != password)
                throw new Exception("Invalid UserName Or Password");

            return new LoginResponse(generateJwtToken(user.UserId, out DateTime tokenExpiryDateTime));
        }

        private string generateJwtToken(int userId, out DateTime tokenExpiryDateTime)
        {
            tokenExpiryDateTime = DateTime.UtcNow.AddDays(7);
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = tokenExpiryDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
