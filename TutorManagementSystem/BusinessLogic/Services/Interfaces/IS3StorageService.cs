using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.Interfaces
{
    public interface IS3StorageService
    {
        Task<string> UploadFileToS3(IFormFile file);

    }
}
