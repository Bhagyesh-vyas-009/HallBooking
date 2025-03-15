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
    public class ResourceImageController : ControllerBase
    {
        #region Configutaion
        private readonly ResourceImageRepository _resourceImageRepository;
        public ResourceImageController(ResourceImageRepository resourceImageRepository)
        {
            _resourceImageRepository = resourceImageRepository;   
        }
        #endregion

        #region GetAllImageByResourceID
        [HttpGet("ImagesBy/{ResourceID}")]
        public IActionResult GetAllImageByResourceID(int ResourceID)
        {
            var images = _resourceImageRepository.GetAllImagesByResourceID(ResourceID);
            return Ok(images);
        }
        #endregion

        #region GetAllImageByOwnerID
        [HttpGet("ImagesByOwner/{OwnerID}")]
        public IActionResult GetAllImageByOwnerID(int OwnerID)
        {
            var images = _resourceImageRepository.GetAllImagesByOwnerID(OwnerID);
            return Ok(images);
        }
        #endregion

        #region GetImageByID
        [HttpGet("GetBy/{ImageID}")]
        public IActionResult GetImageByID(int ImageID)
        {
            ResourceImageModel image= _resourceImageRepository.SelectImageByPK(ImageID);
            if (image == null)
                return NotFound();
            return Ok(image);
        }
        #endregion

        #region DeleteImage
        [Authorize(Roles = "Owner")]
        [HttpDelete("Delete/{ImageID}")]
        public IActionResult DeleteImage(int ImageID,string imagePath)
        {
            var isDeleted = _resourceImageRepository.DeleteImage(ImageID,imagePath);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
        #endregion

        #region ImageInsert
        [Authorize(Roles = "Owner")]
        [HttpPost("Add")]
        public IActionResult ImageInsert([FromBody] ResourceImageModel resourceImage)
        {
            var isInserted = _resourceImageRepository.InsertImage(resourceImage);
            if (resourceImage == null)
                return BadRequest();
            if (isInserted)
                return Ok(new { Message = "Image Inserted  Successfully" });
            return StatusCode(500, "An error ocurred while inserting the Image");
        }
        #endregion

        #region ImageUpdate
        [Authorize(Roles = "Owner")]
        [HttpPut("Update/{ImageID}")]
        public IActionResult ImageUpdate(int ImageID, [FromBody] ResourceImageModel resourceImage)
        {
            var isUpdated = _resourceImageRepository.UpdateImage(resourceImage);
            if (resourceImage == null || ImageID != resourceImage.ImageID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "Image Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the Image");
        }
        #endregion

    }
}
