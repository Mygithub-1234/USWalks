using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USWalks.SPI.Models.Domain;
using USWalks.SPI.Models.DTO;
using USWalks.SPI.Repositories;

namespace USWalks.SPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)
        {
            validateFileUpload(imageUploadRequestDTO);
            if (ModelState.IsValid)
            {//convert DTO to domain model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileDescription = imageUploadRequestDTO.FileDescription,
                    FileSizeBytes = imageUploadRequestDTO.File.Length,
                    FileName = imageUploadRequestDTO.FileName,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.FileName)

                };
                //User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);

        }

        private void validateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload again");
            }
        }
    }
}
