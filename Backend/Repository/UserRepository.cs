using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class UserRepository : DbContext, IUserRepository
    {
        private DbSet<User> _users => Set<User>();
        public UserRepository()
        {
            Database.EnsureCreated();
        }
        public async Task Add(User user)
        {
            await _users.AddAsync(user);

            return;
        }
    }
}