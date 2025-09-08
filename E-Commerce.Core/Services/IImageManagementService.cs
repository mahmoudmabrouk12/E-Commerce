
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Core.Services
{
    public interface IImageManagementService
    {
        public Task<List<string>> AddImageAsync(IFormFileCollection Files , string Source);

        public void DeleteImageAsync(string Source);
    }
}
