using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Interfaces;

public interface IFileService
{
    string AddImage(string imageFile, string path, string imageName);
    public string UploadFile(IFormFile? file, string directory);
    void RemoveImage(string imageName, string path);
    Task<string> GetImageAsBase64Async(string path);
}