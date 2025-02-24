﻿using FluentValidation;
using HallBookingAPI.Data;
using HallBookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace HallBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ResourceController : ControllerBase
    {
        #region Configuration
        private readonly ResourceRepository _resourceRepository;
        private readonly IValidator<ResourceUploadModel> _validator;

        public ResourceController(ResourceRepository resourceRepository, IValidator<ResourceUploadModel> validator)
        {
            _resourceRepository = resourceRepository;
            _validator = validator;
        }
        #endregion

        #region GetAllResources
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllResources()
        {
            var resources = _resourceRepository.GetAllResources();
            return Ok(resources);
        }
        #endregion

        #region GetTopResources
        [HttpGet("Top")]
        public IActionResult GetTopResources()
        {
            var resources = _resourceRepository.GetTop10Resources();
            return Ok(resources);
        }
        #endregion

        #region GetResourceByID
        [HttpGet("GetBy/{ResourceID}")]
        public IActionResult GetResourceByID(int ResourceID)
        {
            ResourceUploadModel resource = _resourceRepository.SelectResourceByPK(ResourceID);
            if (resource == null)
                return NotFound();
            return Ok(resource);
        }
        #endregion

        #region GetResourceByUserID
        [Authorize(Roles = "Owner")]
        [HttpGet("GetBy/User/{UserID}")]
        public IActionResult GetResourceByUserID(int UserID)
        {
            var resourse = _resourceRepository.GetResourcesByUserID(UserID);
            if (resourse == null)
                return NotFound();
            return Ok(resourse);
        }
        #endregion

        #region UploadPropertyWithImages
        [Authorize(Roles = "Owner")]
        [HttpPost("UploadPropertyWithImages")]
        public async Task<IActionResult> UploadPropertyWithImages([FromForm] ResourceUploadModel resourceUpload)
        {
            if (resourceUpload.Images == null || resourceUpload.Images.Count == 0)
            {
                return BadRequest("No images provided.");
            }
            _resourceRepository.UploadResourceWithImage(resourceUpload);
            return Ok(new { message = "Resource uploaded successfully." });
            //return Ok("inserted");
        }
        #endregion


        #region UpdatePropertyWithImages
        [Authorize(Roles = "Owner")]
        [HttpPut("UpdatePropertyWithImages/{ResourceID}")]
        public async Task<IActionResult> UpdatePropertyWithImages(int ResourceID,[FromForm] ResourceUploadModel resourceUpload)
        {
            if (resourceUpload == null || ResourceID != resourceUpload.ResourceID)
            {
                return BadRequest();
            }
            _resourceRepository.UploadResourceWithImage(resourceUpload);
            return Ok(new { message = "Resource updated successfully." });
            //return Ok("inserted");
        }
        #endregion

        #region DeleteResource
        [Authorize(Roles = "Admin,Owner")]
        [HttpDelete("Delete/{ResourceID}")]
        public IActionResult DeleteResource(int ResourceID)
        {
            var isDeleted = _resourceRepository.DeleteResource(ResourceID);
            if (!isDeleted)
                return NotFound();
            //return Ok(new { message = "Deleted" });
            return StatusCode(200, new { message = "Deleted" });
        }
        #endregion


        
    }
}
