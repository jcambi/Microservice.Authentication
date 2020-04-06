using FCClone.Microservice.Authentication.Domain.Interfaces;
using FCClone.Microservice.Authentication.Domain.Models.Dtos;
using FCClone.Microservice.Authentication.Domain.Models.Entities;
using FCClone.Microservice.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCClone.Microservice.Authentication.DataAccess
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _authContext;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IPasswordHasher _passwordHasher;

        public AuthRepository(AuthDbContext authContext, ITokenBuilder tokenBuilder, IPasswordHasher passwordHasher)
        {
            _authContext = authContext;
            _tokenBuilder = tokenBuilder;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticationModel> Authenticate(AuthenticateModel model)
        {
            var user = await _authContext.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = _passwordHasher.VerifyHashedPassword(model.Password, user.Password);

            if (!isValidPassword)
            {
                return null;
            }
            
            var jwtToken = _tokenBuilder.GenerateToken(user);
            var authenticationModel = new AuthenticationModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = jwtToken
            };

            return authenticationModel;
        }

        public async Task<Guid> Register(RegisterUserModel model)
        {
            var userToRegister = new User
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                // encrypt password
                Password = _passwordHasher.HashPassword(model.Password),
                CreatedAtTimestamp = DateTime.UtcNow,
                PasswordChangedTimestamp = DateTime.UtcNow
            };

            await _authContext.Users.AddAsync(userToRegister);
            await _authContext.SaveChangesAsync();

            return userToRegister.Id;
        }
    }
}
