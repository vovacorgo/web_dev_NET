using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using SupportCenter.Models;

namespace SupportCenter.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // POST: /file/upload
        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadModel model)
        {
            if (model.File != null && model.File.Length > 0) // Check for valid file
            {
                try
                {
                    var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Files");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, Path.GetFileName(model.File.FileName)); // Use FileName
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream); // Copy the file to the server
                    }
                    return RedirectToAction("ImagesView");
                }
                catch (IOException ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while uploading the file: " + ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please select a file to upload.");
            }

            return View(model);
        }




        public IActionResult ImagesView()
        {
            // Get the path to the directory containing the images
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");

            // Get the list of image files in the directory
            var imageFiles = Directory.GetFiles(directoryPath)
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".gif"))
                .Select(file => Path.GetFileName(file))
                .ToArray();

            // Pass the file names to the view
            return View(imageFiles);
        }
    }
}