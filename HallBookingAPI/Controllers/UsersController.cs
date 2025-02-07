<<<<<<< HEAD
﻿using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
=======
﻿
using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UsersRepository _usersRepository;
<<<<<<< HEAD
        public UsersController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
=======
        private readonly IConfiguration _configuration;
        public UsersController(UsersRepository usersRepository,IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _usersRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("UserDropDown")]
        public IActionResult GetUserDropDown()
        {
            var users = _usersRepository.UserDropDown();
            return Ok(users);
        }

<<<<<<< HEAD
        [HttpGet("{UserID}")]
=======
        [HttpGet("GetBy/{UserID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult GetUserByID(int UserID)
        {
            UsersModel user = _usersRepository.SelectUserByPK(UserID);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

<<<<<<< HEAD
        [HttpDelete("{UserID}")]
=======
        [HttpDelete("Delete/{UserID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult DeleteUser(int UserID)
        {
            var isDeleted = _usersRepository.DeleteUser(UserID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

<<<<<<< HEAD
        [HttpPost]
=======
        [HttpPost("Register")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult UserInsert([FromBody] UsersModel user)
        {
            var isInserted = _usersRepository.InsertUser(user);
            if (user == null)
                return BadRequest();
            if (isInserted)
<<<<<<< HEAD
                return Ok(new { Message = "User Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the User");
        }

        [HttpPut("{UserID}")]
=======
                return Ok(new { Message = "Registered  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the User");
        }

        [HttpPut("Update/{UserID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult UserUpdate(int UserID, [FromBody] UsersModel user)
        {
            var isUpdated = _usersRepository.UpdateUser(user);
            if (user == null || UserID != user.UserID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "User Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the User");
        }

<<<<<<< HEAD
=======

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel) {

            var userData = _usersRepository.AuthenticateUser(userLoginModel);
            if (userLoginModel == null )
            {
                return BadRequest();
            }
            if (userData==null)
            {
                return StatusCode(500, "Please enter valid Email and password and Role");
            }
            if (userData != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ),
                    new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
                    new Claim("UserID", userData.UserID.ToString()),
                    new Claim("Email", userData.Email.ToString()),
                    new Claim("FullName", userData.FullName.ToString()),
                    new Claim("isAdmin", userData.IsAdmin.ToString()),
                    new Claim("Role", userData.Role.ToString()),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signIn
                    );

                string tockenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tockenValue, User = userData, Message = "User Login Successfully" });
            }
            return Unauthorized("Invalid username or password or Role.");
        }

        //public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        //{
        //    if (userLoginModel == null || string.IsNullOrEmpty(userLoginModel.Email) || string.IsNullOrEmpty(userLoginModel.Password))
        //    {
        //        return BadRequest("Email or Password cannot be empty.");
        //    }

        //    // Authenticate the user
        //    var (isSuccess, message) = _usersRepository.AuthenticateUser(userLoginModel.Email, userLoginModel.Password);

        //    if (!isSuccess)
        //    {
        //        return Unauthorized(message); // If authentication fails
        //    }

        //    // If authentication is successful, generate JWT token
        //    //var token = GenerateJwtToken(userLogin.Email);

        //    return Ok(new { messge="Login successful"});
        //}
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
    }
}
