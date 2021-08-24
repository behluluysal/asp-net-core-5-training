using Microsoft.AspNetCore.Identity;
using rokWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.ViewModels
{
    public class AssignRoleViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public List<User> Users { get; set; }
    }
}
