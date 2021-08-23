﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rokWebsite.Data;
using rokWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                
                if (result.Succeeded)
                {
                    //add roles back to model for refresh
                    model.Roles = _db.Roles.ToList();
                    return View(model);
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            ModelState["DeleteRoleName"].Errors.Clear();
            //add roles back to model for refresh
            model.Roles = _db.Roles.ToList();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteRole(CreateRoleViewModel model)
        {
                IdentityRole identityRole = await roleManager.FindByNameAsync(model.DeleteRoleName);
                IdentityResult result = await roleManager.DeleteAsync(identityRole);
                //add roles back to model for refresh
                model.Roles = _db.Roles.ToList();
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            ModelState["RoleName"].Errors.Clear();
            return RedirectToAction("CreateRole","Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteRolet(string DeleteRoleName)
        {
            string s;
            s = "ali";
            return Json(new {error="",baz="test" });
        }
    }
}
