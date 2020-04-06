using FCClone.Microservice.Authentication.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCClone.Microservice.Authentication.Domain.Interfaces
{
    public interface ITokenBuilder
    {
        string GenerateToken(User user);
    }
}
