using System.IO;
using System.Threading.Tasks;

using CarStore.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace CarStore.Services
{
    public class FileConverter : IFileConverter
    {
        public async Task<byte[]> PostedToByteArray(IFormFile postedFile)
        {
            byte[] imageData = null;

            using (var memoryStream = new MemoryStream())
            {
                await postedFile.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            return imageData;
        }
    }
}
