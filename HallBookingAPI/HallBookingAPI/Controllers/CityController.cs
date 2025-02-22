using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPiDemo.Data;
using WebAPiDemo.Models;

namespace WebAPiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        #region Configutaion
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        #endregion

        #region GetAllCities
        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities = _cityRepository.GetAllCities();
            return Ok(cities);
        }
        #endregion

        #region GetCityDropDown
        [HttpGet("cities")]
        public IActionResult GetCityDropDown()
        {
            var cities = _cityRepository.GetCityDropDown();
            if (!cities.Any())
            {
                return NotFound("No City found");
            }
            return Ok(cities);
        }
        #endregion

        #region GetCityByStateID
        [HttpGet("CityBy/{StateID}")]
        public IActionResult GetCityByStateID(int StateID)
        {
            var cities = _cityRepository.GetCityByStateID(StateID);
            if (!cities.Any())
            {
                return NotFound("No City found");
            }
            return Ok(cities);
        }
        #endregion

        #region GetCityByID
        [HttpGet("GetBy/{CityID}")]
        public IActionResult GetCityByID(int CityID)
        {
            var city = _cityRepository.SelectCityByPK(CityID);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
        #endregion

        #region CityInsert
        [Authorize(Roles = "Admin")]
        [HttpPost("Add")]
        public IActionResult CityInsert([FromBody] CityModel city)
        {
            var isInserted = _cityRepository.InsertCity(city);
            if (city == null)
            {
                return BadRequest();
            }
            if (isInserted)
            {
                return Ok(new { Message = "City Inserted Succesfully" });
            }
            return StatusCode(500, "An error ocurred while inserting the city");
        }
        #endregion

        #region CityUpdate
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{CityID}")]
        public IActionResult CityUpdate(int CityID, [FromBody] CityModel city)
        {
            var isUpdated = _cityRepository.UpdateCity(city);
            if (city == null || CityID != city.CityID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "City Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the city");
        }
        #endregion

        #region CityDelete
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{CityID}")]
        public IActionResult CityDelete(int CityID)
        {
            var isDeleted = _cityRepository.DeleteCity(CityID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

    }
}
