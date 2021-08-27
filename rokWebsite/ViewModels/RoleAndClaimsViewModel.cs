using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.ViewModels
{
    public class RoleAndClaims
    {
        [Required]
        public string RoleName { get; set; }

        [Required]
        public string DeleteRoleName { get; set; }

        public List<IdentityRole> Roles { get; set; }
        public List<IdentityRoleClaim<string>> Claims { get ; set; }
    }
}
