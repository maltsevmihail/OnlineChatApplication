using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Infrastructure
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}