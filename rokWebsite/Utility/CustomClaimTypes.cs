using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Utility
{
    public class CustomClaimTypes
    {
        public const string Permission = "Application.Permission";
    }
    public static class UserPermissions
    {
        public const string Add = "users.add";
        public const string Edit = "users.edit";
        public const string EditRole = "users.edit.role";
    }
}
