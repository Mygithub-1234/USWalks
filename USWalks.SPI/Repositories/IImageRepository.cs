using USWalks.SPI.Models.Domain;

namespace USWalks.SPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
