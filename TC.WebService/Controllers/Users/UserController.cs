﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Extensions;
using TC.WebService.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository _userRepository;
        private ILogger<UserController> _logger;
        private IUnitOfWork _unitOfWork;
        private IUserHelper _userHelper;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IConfiguration config, IUnitOfWork unitOfWork, IUserHelper userHelper)
        {
            _config = config;
            _userRepository = userRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userHelper = userHelper;
        }
        [AllowAnonymous]
        [HttpPost()]
        public IActionResult CreateToken([FromBody] LoginModelViewModel login)
        {
            IActionResult response = Unauthorized();
            _logger.LogInformation($"Login {login.Email}");
            if (String.IsNullOrWhiteSpace(login.Password))
            {
                return response;
            }

            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new
                {
                    Name = user.Name,
                    Token = tokenString
                });
            }

            return response;
        }
        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Registration([FromBody] RegistrationViewModel viewModel)
        {
            if (_userRepository.FindByCondition(x => x.Email == viewModel.Email).Any())
            {
                return Ok(new
                {
                    message = "Email exists"
                });
            }


            if (String.IsNullOrWhiteSpace(viewModel.Password))
            {
                return Ok(new
                {
                    message = "Password cannot be empty"
                });
            }
            _userRepository.Create(new UserModel
            {
                Email = viewModel.Email,
                Password = _userHelper.PasswordHash(viewModel.Password),
                Guid = System.Guid.NewGuid(),
                Name = viewModel.Name,
            });
            _unitOfWork.SaveChanges();
            return Ok();
        }
        private string BuildToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claimsForAccessToken = new List<Claim>();
            if (string.IsNullOrEmpty(user.Guid.ToString()))
            {
                throw new ArgumentNullException(nameof(user.Guid));               
            }
            claimsForAccessToken.Add(new Claim("Guid", user.Guid.ToString()));
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new Exception("User Name cannot be empty");
            }
            claimsForAccessToken.Add(new Claim("Name", user.Name));
            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],
               audience: _config["Jwt:Issuer"],
               claims: claimsForAccessToken.ToArray(),
              expires: DateTime.Now.AddMonths(1),
              signingCredentials: creds

             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModelViewModel login)
        {

            UserModel user = _userRepository.Login(login.Email, _userHelper.PasswordHash(login.Password));

            return user;
        }
    }
}