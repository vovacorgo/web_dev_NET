using Microsoft.AspNetCore.Http;

namespace SupportCenter.Models
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; } = null!;
    }
}