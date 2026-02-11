using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Models
{
    public class User
    {
        public User(Guid id, string login, string passwordHash)
        {
            Id = id;
            Login = login;
            PasswordHash = passwordHash;
        }
        public Guid Id { get; set; }
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public static User Create(Guid Id, string login, string passwordHash)
        {
            return new User(Id, login, passwordHash);
        }
    }
}