using System;
using System.Collections.Generic;
using System.Text;

namespace FCClone.Microservice.Authentication.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string password, string hash);
    }
}
