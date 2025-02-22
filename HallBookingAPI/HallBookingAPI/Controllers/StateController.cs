using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPiDemo.Data;
using WebAPiDemo.Models;

namespace WebAPiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        #region Configuration
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }
        #endregion

        #region GetAllStates
        [HttpGet]
        public IActionResult GetAllStates()
        {
            var states = _stateRepository.GetAllStates();
            return Ok(states);
        }
        #endregion

        #region GetStateDropDown
        [HttpGet("states")]
        public IActionResult GetStateDropDown()
        {
            var states = _stateRepository.GetStateDropDown();
            if (!states.Any())
            {
                return NotFound("No State found");
            }
            return Ok(states);
        }
        #endregion

        #region GetStateByCountryID
        [HttpGet("StatesBy/{CountryID}")]
        public IActionResult GetStateByCountryID(int CountryID)
        {
            var states = _stateRepository.GetStateByCountryID(CountryID);
            if (!states.Any())
            {
                return NotFound("No State found");
            }
            return Ok(states);
        }
        #endregion

        #region SelectStateByID
        [HttpGet("GetBy/{StateID}")]
        public IActionResult SelectStateByID(int StateID)
        {
            StateModel state = _stateRepository.SelectStateByID(StateID);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
        #endregion

        #region DeleteState
        [HttpDelete("Delete/{StateID}")]
        public IActionResult DeleteState(int StateID)
        {
            var isDeleted = _stateRepository.DeleteState(StateID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region StateInsert
        [HttpPost("Add")]
        public IActionResult StateInsert([FromBody] StateModel state)
        {
            var isInserted = _stateRepository.InsertState(state);
            if (state == null)
            {
                return BadRequest();
            }
            if (isInserted)
            {
                return Ok(new { Message = "State Inserted Succesfully" });
            }
            return StatusCode(500, "An error ocurred while inserting the State");
        }
        #endregion

        #region StateUpdate
        [HttpPut("Update/{StateID}")]
        public IActionResult StateUpdate(int StateID, [FromBody] StateModel state)
        {
            var isUpdated = _stateRepository.UpdateState(state);
            if (state == null || StateID != state.StateID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "State Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the State");
        }
        #endregion

    }
}
