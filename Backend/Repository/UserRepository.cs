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
        public UserRepository(DbContextOptions<UserRepository> options) : base(options)
        {
            
            Database.EnsureCreated();
        }
        public async Task Add(User user)
        {
            await _users.AddAsync(user);
            
            await SaveChangesAsync();

            return;
        }
        public async Task<User> GetByLogin(string login)
        {
            User? user = await _users.FirstOrDefaultAsync(x => x.Login == login);

            return user;
        }
    }
}