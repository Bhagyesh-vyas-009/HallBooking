using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        #region Configuration
        private readonly UsersRepository _usersRepository;
        private readonly IConfiguration _configuration;
        public UsersController(UsersRepository usersRepository,IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
        }
        #endregion

        #region GetAllUser
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _usersRepository.GetAllUsers();
            return Ok(users);
        }
        #endregion

        #region GetUserDropDown
        [Authorize(Roles = "Owner")]
        [HttpGet("UserDropDown/{OwnerID}")]
        public IActionResult GetUserDropDown(int OwnerID)
        {
            var users = _usersRepository.UserDropDown(OwnerID);
            return Ok(users);
        }
        #endregion

        #region GetUserByID
        [Authorize]
        [HttpGet("GetBy/{UserID}")]
        public IActionResult GetUserByID(int UserID)
        {
            UsersModel user = _usersRepository.SelectUserByPK(UserID);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        #endregion

        #region DeleteUser
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{UserID}")]
        public IActionResult DeleteUser(int UserID)
        {
            var isDeleted = _usersRepository.DeleteUser(UserID);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
        #endregion

        #region UserInsert
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
        #endregion

        #region UserUpdate
        [Authorize]
        [HttpPut("Update/{UserID}")]
        public IActionResult UserUpdate(int UserID, [FromBody] UserUpdateModel user)
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
        #endregion

        #region Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {

            if (userLoginModel == null)
            {
                return BadRequest(new { message = "Invalid request data" });
            }
            var userData = _usersRepository.AuthenticateUser(userLoginModel);
            if (userData == null)
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
                    new Claim(ClaimTypes.Role, userData.Role.ToString()),

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
        #endregion

        #region ChangePassword
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.NewPassword))
                return BadRequest("Invalid input.");

            bool isUpdated = _usersRepository.ChangePassword(model);

            if (isUpdated)
                return Ok(new { message = "Password updated successfully." });
            else
                return BadRequest("Failed to update password.");
        }
        #endregion
    }
}
