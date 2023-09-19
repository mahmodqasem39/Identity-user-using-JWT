namespace JWT.Models
{

    public sealed record RegisterModel(string Username, string Email,string FullName, string Password);
}
