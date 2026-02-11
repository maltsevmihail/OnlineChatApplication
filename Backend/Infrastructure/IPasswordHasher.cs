using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure
{
    public interface IPasswordHasher
    {
        public string Generate(string password);
        public bool Verify(string password, string passwrodHash);
    }
}