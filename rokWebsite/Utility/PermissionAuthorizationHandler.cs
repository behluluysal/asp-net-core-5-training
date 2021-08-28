using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using rokWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace rokWebsite.Utility
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private IMemoryCache _cache;

        public PermissionAuthorizationHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _cache = memoryCache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity.Name == null)
            {
                return;
            }
            var user = await _userManager.GetUserAsync(context.User);
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));
            string s2 = context.User.Identity.Name + "Roles";
            string s3 = context.User.Identity.Name + "Claims";
            var o = _cache.Get<IList<string>>(s2);
            var o2 = _cache.Get<List<Claim>>(s3);
            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                        x.Value == requirement.Permission)
                                            .Select(x => x.Value);

                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}
