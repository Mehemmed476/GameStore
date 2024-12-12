using GameStore.BL.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace GameStore.BL.Services.Concretes
{
    public class FileService : IFileService
    {
        
        public async Task<string> SaveFileAsync(IFormFile imageFile, string rootPath, string[] allowedFileExtensions)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            var contentPath = Path.Combine(rootPath, "Uploads");
            if (!Directory.Exists(contentPath))
            {
                Directory.CreateDirectory(contentPath);
            }

            var ext = Path.GetExtension(imageFile.FileName).ToLower();
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(", ", allowedFileExtensions)} files are allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(contentPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return fileName;
        }


        public void DeleteFile(string fileNameWithExtension, string envPath)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }
            
            var path = Path.Combine(envPath, "Uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Invalid file path");
            }

            File.Delete(path);
        }
    }
}
