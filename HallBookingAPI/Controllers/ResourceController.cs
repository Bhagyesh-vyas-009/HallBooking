<<<<<<< HEAD
﻿using HallBookingAPI.Data;
using HallBookingAPI.Models;
=======
﻿using FluentValidation;
using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< HEAD
    public class ResourceController : ControllerBase
    {
        private readonly ResourceRepository _resourceRepository;
        public ResourceController(ResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
=======
    [Authorize]
    public class ResourceController : ControllerBase
    {

        private readonly ResourceRepository _resourceRepository;
        private readonly IValidator<ResourceUploadModel> _validator;

        public ResourceController(ResourceRepository resourceRepository, IValidator<ResourceUploadModel> validator)
        {
            _resourceRepository = resourceRepository;
            _validator = validator;
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        }

        [HttpGet]
        public IActionResult GetAllResources()
        {
            var resources = _resourceRepository.GetAllResources();
            return Ok(resources);
        }

        

<<<<<<< HEAD
        [HttpGet("{ResourceID}")]
        public IActionResult GetResourceByID(int ResourceID)
        {
            ResourceModel resourse = _resourceRepository.SelectResourceByPK(ResourceID);
=======
        [HttpGet("GetBy/{ResourceID}")]
        public IActionResult GetResourceByID(int ResourceID)
        {
            ResourceUploadModel resource = _resourceRepository.SelectResourceByPK(ResourceID);
            if (resource == null)
                return NotFound();
            return Ok(resource);
        }

        [HttpGet("GetBy/User/{UserID}")]
        public IActionResult GetResourceByUserID(int UserID)
        {
            var resourse = _resourceRepository.GetResourcesByUserID(UserID);
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
            if (resourse == null)
                return NotFound();
            return Ok(resourse);
        }

<<<<<<< HEAD
        [HttpDelete("{ResourceID}")]
=======
        [HttpDelete("Delete/{ResourceID}")]
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public IActionResult DeleteResource(int ResourceID)
        {
            var isDeleted = _resourceRepository.DeleteResource(ResourceID);
            if (!isDeleted)
                return NotFound();
<<<<<<< HEAD
            return NoContent();
        }

        [HttpPost]
        public IActionResult ResourceInsert([FromBody] ResourceModel resource)
=======
            //return Ok(new { message = "Deleted" });
            return StatusCode(200, new { message = "Deleted" });
        }

        [HttpPost("Add")]
        //public IActionResult ResourceInsert([FromBody] ResourceModel resource)
        //{
        //    // Validate the model
        //    var validationResult = _validator.Validate(resource);
        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(validationResult.Errors);  // Return validation errors
        //    }

        //    // Proceed with business logic (e.g., save the user)
        //    return Ok(new { Message = "Resource created successfully!" });
        //}
        public IActionResult ResourceInsert([FromBody] ResourceUploadModel resource)
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        {
            var isInserted = _resourceRepository.InsertResource(resource);
            if (resource == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Resource Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Resource");
        }

<<<<<<< HEAD
        [HttpPut("{ResourceID}")]
        public IActionResult ResourceUpdate(int ResourceID, [FromBody] ResourceModel resource)
=======
        [HttpPut("Update/{ResourceID}")]
        public IActionResult ResourceUpdate(int ResourceID, [FromBody] ResourceUploadModel resource)
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        {
            var isUpdated = _resourceRepository.UpdateResource(resource);
            if (resource == null || ResourceID != resource.ResourceID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "Resource Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the Resource");
        }
<<<<<<< HEAD
=======

        [HttpPost("UploadPropertyWithImages")]
        public async Task<IActionResult> UploadProperty([FromForm] ResourceUploadModel resourceUpload)
        {
            if (resourceUpload.Images == null || resourceUpload.Images.Count == 0)
            {
                return BadRequest("No images provided.");
            }
            _resourceRepository.UploadPropertyWithImage(resourceUpload);
            return Ok("inserted");
        }
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
    }
}
