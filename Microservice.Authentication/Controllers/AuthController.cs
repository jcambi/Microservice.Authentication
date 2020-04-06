using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCClone.Microservice.Authentication.Domain.Interfaces;
using FCClone.Microservice.Authentication.Domain.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCClone.Microservice.Authentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationModel>> Authenticate(AuthenticateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var authModel = await _authRepo.Authenticate(model);
            if (authModel == null)
            {
                return Unauthorized();
            }
            return Ok(authModel);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newUserId = await _authRepo.Register(model);

            return Ok(newUserId);
        }
    }
}