using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using rokWebsite.Models;
using rokWebsite.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Helpers
{
    internal class UserSessionHelper
    {
        private static UserManager<User> _userManager;
        private static  RoleManager<IdentityRole> _roleManager;
        private static IHttpContextAccessor _httpContextAccessor = null;

        public UserSessionHelper(SignInManager<User> signInManager,
           UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public static async Task SetSessions(string Username, HttpContext tt, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            HttpContext httpContext = tt;
            _userManager = userManager;
            _roleManager = roleManager;
            var user = await _userManager.FindByNameAsync(Username);
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));
            List<string> roleClaims = new List<string>();
            foreach (var role in userRoles)
            {
                var temp = await _roleManager.GetClaimsAsync(role);
                foreach (var item in temp)
                {
                    roleClaims.Add(item.Value);
                }
            }
            string RolesOfUser = Username + "Roles";
            string ClaimsOfUserRole = Username + "Claims";
            httpContext.Session.SetComplexData(RolesOfUser, userRoleNames);
            httpContext.Session.SetComplexData(ClaimsOfUserRole, roleClaims);
        }

        public static void EndSessions(string Username)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            string RolesOfUser = Username + "Roles";
            string ClaimsOfUserRole = Username + "Claims";
            httpContext.Session.Remove(RolesOfUser);
            httpContext.Session.Remove(ClaimsOfUserRole);
        }

        public static async Task<List<string>> GetSessionData(string key, HttpContext http, string Username, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            HttpContext httpContext = http;
            List<string> returnValue = httpContext.Session.GetComplexData<List<string>>(key);
            if (returnValue == null)
                await SetSessions(Username,http,userManager,roleManager);

            return httpContext.Session.GetComplexData<List<string>>(key);
        }
    }
}
