using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using rokWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using rokWebsite.Helpers;
using System.Globalization;

namespace rokWebsite.Utility
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IMemoryCache _cache;
        private IHttpContextAccessor _httpContextAccessor = null;

        public PermissionAuthorizationHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _cache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity.Name == null)
            {
                return;
            }

            HttpContext httpContext = _httpContextAccessor.HttpContext;
            DateTime SessionUpdateTime = Convert.ToDateTime(httpContext.Session.GetString(context.User.Identity.Name));
            DateTime LastPermissionUpdate = Convert.ToDateTime(_cache.Get("LastPermissionUpdate"));

            if (SessionUpdateTime < LastPermissionUpdate)
            {
                await UserSessionHelper.SetSessions(context.User.Identity.Name, httpContext, _userManager, _roleManager);
            }

            string UserName = context.User.Identity.Name;
            string s2 = UserName + "Roles";
            string s3 = UserName  + "Claims";

            List<string> data = await UserSessionHelper.GetSessionData(s2, httpContext, UserName, _userManager,_roleManager);
            List<string> UserClaims = await UserSessionHelper.GetSessionData(s3, httpContext, UserName, _userManager, _roleManager);
           
            if(UserClaims.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
                return;
            }
           
        }
    }
}
