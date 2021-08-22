using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Models
{
    public class User : IdentityUser
    {
        public bool Activated { get; set; }
        public virtual Alliance Alliance { get; set; }
    }
}
