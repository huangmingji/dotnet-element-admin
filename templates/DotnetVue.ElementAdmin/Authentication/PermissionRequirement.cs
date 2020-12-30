using Microsoft.AspNetCore.Authorization;

namespace DotnetVue.ElementAdmin.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
        
    }
}