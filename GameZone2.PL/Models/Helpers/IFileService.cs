namespace GameZone2.PL.Models.Helpers
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string folder = "images");
        void DeleteImage(string relativePath);
    }
}
