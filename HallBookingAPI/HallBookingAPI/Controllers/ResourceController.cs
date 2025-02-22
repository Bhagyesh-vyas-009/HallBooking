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
        [HttpGet("GetBy/User/{UserID}")]
        public IActionResult GetResourceByUserID(int UserID)
        {
            var resourse = _resourceRepository.GetResourcesByUserID(UserID);
            if (resourse == null)
                return NotFound();
            return Ok(resourse);
        }
        #endregion

        #region DeleteResource
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
