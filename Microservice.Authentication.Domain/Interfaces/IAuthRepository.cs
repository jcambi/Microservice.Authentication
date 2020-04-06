using FCClone.Microservice.Authentication.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FCClone.Microservice.Authentication.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthenticationModel> Authenticate(AuthenticateModel model);
        Task<Guid> Register(RegisterUserModel model);
    }
}
