using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UsersRepository _usersRepository;
private readonly IConfiguration _configuration;
        public UsersController(UsersRepository usersRepository,IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
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

[HttpGet("GetBy/{UserID}")]
        public IActionResult GetUserByID(int UserID)
        {
            UsersModel user = _usersRepository.SelectUserByPK(UserID);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

[HttpDelete("Delete/{UserID}")]
        public IActionResult DeleteUser(int UserID)
        {
            var isDeleted = _usersRepository.DeleteUser(UserID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

[HttpPost("Register")]
        public IActionResult UserInsert([FromBody] UsersModel user)
        {
            var isInserted = _usersRepository.InsertUser(user);
            if (user == null)
                return BadRequest();
            if (isInserted)
        return Ok(new { Message = "Registered  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the User");
        }

        [HttpPut("Update/{UserID}")]
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


        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel) {

            if (userLoginModel == null)
            {
                return BadRequest(new { message = "Invalid request data" });
            }
            //if (userLoginModel == null )
            //{
            //    return BadRequest();
            //}
            var userData = _usersRepository.AuthenticateUser(userLoginModel);
            if (userData==null)
            {
                return Unauthorized(new { message = "Please enter valid Email and password and Role" });
            }

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
    }
}
