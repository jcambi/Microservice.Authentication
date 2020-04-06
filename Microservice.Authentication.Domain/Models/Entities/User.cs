using System;
using System.Collections.Generic;
using System.Text;

namespace FCClone.Microservice.Authentication.Domain.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAtTimestamp { get; set; }
        public DateTime PasswordChangedTimestamp { get; set; }
    }
}
