using Microsoft.AspNetCore.Http;

namespace GameStore.BL.Services.Abstractions;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile imageFile, string envPath, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension, string envPath);
}