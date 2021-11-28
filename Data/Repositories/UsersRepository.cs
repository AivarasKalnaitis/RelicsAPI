using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAll();
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly RelicsContext _relicsContext;

        public UsersRepository(RelicsContext relicsContext)
        {
            _relicsContext = relicsContext;
        }

        public async Task<List<User>> GetAll()
        {
            return await _relicsContext.Users.ToListAsync();
        }
    }
}
