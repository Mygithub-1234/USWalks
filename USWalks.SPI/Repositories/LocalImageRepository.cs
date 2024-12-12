using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using USWalks.SPI.Data;
using USWalks.SPI.Models.Domain;

namespace USWalks.SPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly USWalksDbContext uSWalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor, USWalksDbContext uSWalksDbContext)
        {
            this.webHost = webHost;
            this.httpContextAccessor = httpContextAccessor;
            this.uSWalksDbContext = uSWalksDbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHost.ContentRootPath, "Images", 
                $"{image.FileName}");
            //Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
                
            //https://localhost:1234/images/image.jpg
            
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{ image.FileName}";
            image.FilePath = urlFilePath;

            await uSWalksDbContext.Images.AddAsync(image);
            await uSWalksDbContext.SaveChangesAsync();
            return image;


        }
    }
}
