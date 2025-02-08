﻿using FluentValidation;
using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResourceController : ControllerBase
    {

        private readonly ResourceRepository _resourceRepository;
        private readonly IValidator<ResourceUploadModel> _validator;

        public ResourceController(ResourceRepository resourceRepository, IValidator<ResourceUploadModel> validator)
        {
            _resourceRepository = resourceRepository;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAllResources()
        {
            var resources = _resourceRepository.GetAllResources();
            return Ok(resources);
        }

        

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
            if (resourse == null)
                return NotFound();
            return Ok(resourse);
        }

        [HttpDelete("Delete/{ResourceID}")]
        public IActionResult DeleteResource(int ResourceID)
        {
            var isDeleted = _resourceRepository.DeleteResource(ResourceID);
            if (!isDeleted)
                return NotFound();
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
        {
            var isInserted = _resourceRepository.InsertResource(resource);
            if (resource == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Resource Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Resource");
        }

        [HttpPut("Update/{ResourceID}")]
        public IActionResult ResourceUpdate(int ResourceID, [FromBody] ResourceUploadModel resource)
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
    }
}
