using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace DotnetVue.ElementAdmin.Authentication
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly ILogger<PermissionHandler> _logger;
        public PermissionHandler(ILogger<PermissionHandler> logger) 
        {
            this._logger = logger;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Claims.All(x => x.Type == "permission" && x.Value != requirement.Permission))
            {
                context.Fail();
                return;
            }
            
            context.Succeed(requirement);
            await Task.CompletedTask;
        }
    }
}