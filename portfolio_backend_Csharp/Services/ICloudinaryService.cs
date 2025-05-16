using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using global::portfolio_backend_Csharp.Configuration;
using Microsoft.AspNetCore.Http;
namespace portfolio_backend_Csharp.Services
{

    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }

}
