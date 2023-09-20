using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JWT.Models
{

    public sealed record RegisterModel([Required] string Username, string FullName,
                                       [Required, EmailAddress] string Email,
                                       [Required,PasswordPropertyText] string Password);
    public sealed record UserModel(int Id,string Username, string Email, string FullName);
    public sealed record LoginModel([Required]string Username,[Required]string Password);
    public sealed record LoginResponse(string token);





}
