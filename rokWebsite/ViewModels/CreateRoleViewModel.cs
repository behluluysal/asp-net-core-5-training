﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.ViewModels
{
    public class CreateRoleViewModel
    {
        public IdentityRole Role { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
