using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace rokWebsite.Utility
{
    public static class Permissions
    {
        public static class Dashboards
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
            public static readonly List<string> _metrics =
                new List<string>(new[]
                {
                    View,Create,Edit,Delete
                });
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public static readonly List<string> _metrics =
               new List<string>(new[]
               {
                    View,Create,Edit,Delete
               });
        }
    }
}
