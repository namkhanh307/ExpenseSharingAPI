using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public class FileUploadHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly static string _customDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");

        public FileUploadHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            if (!Directory.Exists(_customDirectory))
            {
                Directory.CreateDirectory(_customDirectory);
            }
        }

        public static async Task<string> UploadFile(IFormFile? file, string id)
        {
            if (file != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(file.FileName); // Extract the file extension
                    string fileName = $"{id}{fileExtension}"; // Use the provided Id with the extracted extension
                    string filePath = Path.Combine(_customDirectory, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return fileName;
                }
                catch (Exception ex)
                {
                    return ex.Message; // Return the error message if an exception occurs
                }
            }
            else
            {
                return "string";
            }
        }

        public static async Task DeleteFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
