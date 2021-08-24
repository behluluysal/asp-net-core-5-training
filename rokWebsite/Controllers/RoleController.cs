using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rokWebsite.Data;
using rokWebsite.Models;
using rokWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly KingdomDbContext _db;


        public RoleController(RoleManager<IdentityRole> roleManager, KingdomDbContext db, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            CreateRoleViewModel model = new CreateRoleViewModel();
            model.Roles = _db.Roles.ToList();
            return View(model);
        }

       
        public IActionResult AssignClaims()
        {
            CreateRoleViewModel model = new CreateRoleViewModel();
            model.Roles = _db.Roles.ToList();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };
            IdentityResult result = await roleManager.CreateAsync(identityRole);
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
            model.Users = userManager.Users.ToList();
            model.Roles = roleManager.Roles.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };
            IdentityResult result = await roleManager.CreateAsync(identityRole);
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
            var user = await userManager.FindByIdAsync(userId);
            await userManager.AddToRoleAsync(user, "test2");
            var roles = await userManager.GetRolesAsync(user);
            List<string> RoleIds = new List<string>();
            foreach (var item in roles)
            {
                var roleId = await roleManager.FindByNameAsync(item);
                RoleIds.Add(roleId.Id);
            }
            return Json(RoleIds);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(CreateRoleViewModel model)
        {
            List<string> errorList = new List<string>();

            IdentityRole identityRole = await roleManager.FindByNameAsync(model.DeleteRoleName);

            if (identityRole != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(identityRole);
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
