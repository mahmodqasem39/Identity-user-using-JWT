using JWT.Models;
using Microsoft.AspNetCore.Authorization;

namespace JWT.Utilities
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) 
            : base(policy: permission.ToString())
        {

        }
    }
}
