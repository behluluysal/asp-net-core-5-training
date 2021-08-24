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

      
    }
}
