using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using rokWebsite.Data;
using rokWebsite.Models;
using rokWebsite.Utility;
using rokWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace rokWebsite.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly KingdomDbContext _db;
        private readonly IMemoryCache _memoryCache;

        public RoleController(RoleManager<IdentityRole> roleManager, KingdomDbContext db, UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            RoleAndClaims model = new RoleAndClaims();
            model.Roles = _db.Roles.ToList();
            model.Claims = new List<string>();
            model.Claims = model.Claims.Concat(Permissions.Dashboards._metrics).ToList();
            model.Claims = model.Claims.Concat(Permissions.Users._metrics).ToList();
            return View(model);
        }

        
        public IActionResult AssignClaims()
        {
            RoleAndClaims model = new RoleAndClaims();
            model.Roles = _db.Roles.ToList();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleAndClaims model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                TempData["success"] = "Role " + model.RoleName + " saved successfully";
                return RedirectToAction("Index", "Role");
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (IdentityError error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
                TempData["error"] = errorList;
                return RedirectToAction("Index", "Role");
            }
        }

        public IActionResult AssignRole()
        {
            AssignRoleViewModel model = new AssignRoleViewModel();
            model.Users = _userManager.Users.ToList();
            model.Roles = _roleManager.Roles.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleAndClaims model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                TempData["success"] = "Role " + model.RoleName + " saved successfully";
                return RedirectToAction("Index", "Role");
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (IdentityError error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
                TempData["error"] = errorList;
                return RedirectToAction("Index", "Role");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            List<string> RoleIds = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            return Json(RoleIds);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetRoleClaims(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            List<string> ClaimIds = _db.RoleClaims.Where(x => x.RoleId == role.Id).Select(x=>x.ClaimValue).ToList();
            return Json(ClaimIds);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SwitchUserRole(string UserId, string RoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var role = await _roleManager.FindByIdAsync(RoleId);

            //try to add the role
            IdentityResult result = await _userManager.AddToRoleAsync(user,role.Name);
            if(result.Succeeded)
            {
                return Json(new
                {
                    Data = "Role " + role.Name + " successfully added to user " + user.UserName,
                    ContentType = "success",
                });
            }
            else
            {
                //if failed, try to remove the role
                IdentityResult resultDelete = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if(resultDelete.Succeeded)
                {
                    return Json(new
                    {
                        Data = "Role " + role.Name + " successfully removed from user " + user.UserName,
                        ContentType = "success",
                    });
                }
            }
            return Json(new
            {
                Data = "Something went wrong",
                ContentType = "error",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SwitchRoleClaim(string RoleId, string ClaimValue)
        {
            string rr = RoleId;
            string tt = ClaimValue;
            long NowTicks; // for changing cache
            //Check if Claim exists in role
            var claim = _db.RoleClaims.Where(x => x.ClaimValue == ClaimValue && x.RoleId == RoleId).FirstOrDefault();

            if (claim == null)
            {
                var role = await _roleManager.FindByIdAsync(RoleId);
                IdentityResult result= await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, ClaimValue));
                if(result.Succeeded)
                {
                    NowTicks = DateTime.Now.Ticks;
                    _memoryCache.Set("LastPermissionUpdate", new DateTime(NowTicks));

                    return Json(new
                    {
                        Data = "Claim "+ClaimValue+" successfully added to " + role.Name,
                        ContentType = "success",
                    });
                }
            }
            else
            {
                var role = await _roleManager.FindByIdAsync(RoleId);
                IdentityResult result = await _roleManager.RemoveClaimAsync(role, new Claim(CustomClaimTypes.Permission, ClaimValue));
                string t = ClaimValue;
                if(result.Succeeded)
                {
                    NowTicks = DateTime.Now.Ticks;
                    _memoryCache.Set("LastPermissionUpdate", new DateTime(NowTicks));

                    return Json(new
                    {
                        Data = "Claim " + claim.ClaimValue + " successfully removed from " + role.Name,
                        ContentType = "success",
                    });
                }
            }
            //If both results go into else statement, it will end up here.
            return Json(new
            {
                Data = "There was an unknown error.",
                ContentType = "error",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(RoleAndClaims model)
        {
            List<string> errorList = new List<string>();

            IdentityRole identityRole = await _roleManager.FindByNameAsync(model.DeleteRoleName);

            if (identityRole != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["success"] = "Role " + model.RoleName + " removed successfully";
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }
                    TempData["error"] = errorList;
                    return RedirectToAction("Index", "Role");
                }
            }
            else
            {
                errorList.Add("Role named " + model.DeleteRoleName + " could not found");
                TempData["error"] = errorList;
                return RedirectToAction("Index", "Role");
            }
        }

    }
}
