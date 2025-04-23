
namespace GameZone2.PL.Models.Helpers
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile, string folder = "images")
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("No file uploaded.");

            // استخراج الامتداد
            var extension = Path.GetExtension(imageFile.FileName).ToLower();

            // قائمة بالامتدادات المسموح بها
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Only image files are allowed (jpg, jpeg, png, gif, bmp, webp).");

            string wwwRootPath = _env.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + extension;
            string folderPath = Path.Combine(wwwRootPath, folder);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/{folder}/{fileName}";
        }
        public void DeleteImage(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return;

            string fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

    }
}
