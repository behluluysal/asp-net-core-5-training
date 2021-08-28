using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using rokWebsite.Models;

namespace rokWebsite.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IMemoryCache _memoryCache;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, IMemoryCache memoryCache)
        {
            _signInManager = signInManager;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            string RolesOfUser = _signInManager.Context.User.Identity.Name + "Roles";
            string ClaimsOfUserRole = _signInManager.Context.User.Identity.Name + "Claims";
            _memoryCache.Remove(RolesOfUser);
            _memoryCache.Remove(ClaimsOfUserRole);

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
