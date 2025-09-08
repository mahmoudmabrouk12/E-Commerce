using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace E_Commerce.InfraStructure.Repository.Service
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection Files, string Source)
        {
            var SaveImageSource = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot" ,"Images", Source);
            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);  
            }
            foreach (var item in Files)
            {
                if (item.Length > 0)
                {
                    var ImageName = item.FileName;
                    var ImageSource = $"/Images/{Source}/{ImageName}";
                    var root = Path.Combine (ImageDirectory, ImageName) ;
                    using (var Stream = new FileStream(root , FileMode.Create))
                    {
                        await item .CopyToAsync(Stream) ;
                    }
                    SaveImageSource.Add(ImageSource);

                }
            }
            return SaveImageSource;
        }

        public void DeleteImageAsync(string Source)
        {
            var Info = _fileProvider.GetFileInfo(Source);
            var Root = Info.PhysicalPath;
            File.Delete(path: Root);
        }
    }
}
