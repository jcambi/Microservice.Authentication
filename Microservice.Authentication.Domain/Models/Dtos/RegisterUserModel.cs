using System;
using System.Collections.Generic;
using System.Text;

namespace FCClone.Microservice.Authentication.Domain.Models.Dtos
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
