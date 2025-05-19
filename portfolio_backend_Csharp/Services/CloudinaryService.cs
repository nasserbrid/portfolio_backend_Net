using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using portfolio_backend_Csharp.Configuration;

namespace portfolio_backend_Csharp.Services
{
    public class CloudinaryService: ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var settings = new CloudinarySettings();
            config.GetSection("CloudinarySettings").Bind(settings);

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
                Transformation = new Transformation().Crop("limit").Width(800).Height(600)
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
