using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Auth.Model
{
    public class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Customer = nameof(Customer);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, Customer };
    }
}
