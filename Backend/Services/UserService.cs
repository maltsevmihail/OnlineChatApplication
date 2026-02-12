using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        public UserService(
            IJwtProvider jwtProvider,
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }
        public async Task Register(string login, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), login, hashedPassword);

            await _userRepository.Add(user);
        }
        public async Task<string> Login(string login, string password)
        {
            var user =  await _userRepository.GetByLogin(login);

            if (user is null)
                throw new Exception("User not found.");
            
            var reuslt = _passwordHasher.Verify(password, user.PasswordHash);
            if (!reuslt)
                throw new Exception("Wrong password");

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}