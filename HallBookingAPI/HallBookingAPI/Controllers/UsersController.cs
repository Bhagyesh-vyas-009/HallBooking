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
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _usersRepository.GetAllUsers();
            return Ok(users);
        }
        #endregion

        #region GetUserDropDown
        [HttpGet("UserDropDown/{OwnerID}")]
        public IActionResult GetUserDropDown(int OwnerID)
        {
            var users = _usersRepository.UserDropDown(OwnerID);
            return Ok(users);
        }
        #endregion

        #region GetUserByID
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
        #endregion

    }
}
