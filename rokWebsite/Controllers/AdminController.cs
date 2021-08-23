using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rokWebsite.Data;
using rokWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;



namespace rokWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly KingdomDbContext _db;


        public AdminController(RoleManager<IdentityRole> roleManager, KingdomDbContext db)
        {
            this.roleManager = roleManager;
            _db = db;
        }


        public IActionResult Dashboard()
        {
            ViewBag.test = "Alit";
            return View();
        }

        public IActionResult CreateRole()
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
                TempData["success"] = "Role "+ model.RoleName + " saved successfully";
                return RedirectToAction("CreateRole","Admin");
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (IdentityError error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
                TempData["error"] = errorList;
                return RedirectToAction("CreateRole", "Admin");
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(CreateRoleViewModel model)
        {
            List<string> errorList = new List<string>();

            IdentityRole identityRole = await roleManager.FindByNameAsync(model.DeleteRoleName);

            if(identityRole != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["success"] = "Role " + model.RoleName + " removed successfully";
                    return RedirectToAction("CreateRole", "Admin");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }
                    TempData["error"] = errorList;
                    return RedirectToAction("CreateRole", "Admin");
                }
            }
            else
            {
                errorList.Add("Role named "+model.DeleteRoleName+" could not found");
                TempData["error"] = errorList;
                return RedirectToAction("CreateRole", "Admin");
            }
           
        }
    }
}
