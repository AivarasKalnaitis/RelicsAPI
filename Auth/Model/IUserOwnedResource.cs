using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Auth.Model
{
    public interface IUserOwnedResource
    {
        string UserId { get; }
    }
}
