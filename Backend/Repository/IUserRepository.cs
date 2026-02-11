using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repository
{
    public interface IUserRepository
    {
        public Task Add(User user);
    }
}