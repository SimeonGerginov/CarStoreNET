using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarStore.Services.Contracts
{
    public interface IFileConverter
    {
        Task<byte[]> PostedToByteArray(IFormFile postedFile);
    }
}
