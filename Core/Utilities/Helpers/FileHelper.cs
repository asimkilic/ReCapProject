using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var result = newPath(file);
            try
            {
                var sourcePath = Path.GetTempFileName();
                if (file.Length>0)
                {
                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    File.Move(sourcePath, result.newPath);
                }
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
            return result.path2;
        }
        public static string Update(string sourcePath,IFormFile file)
        {
            var result = newPath(file);
            try
            {
                if (sourcePath.Length>0)
                {
                    using (var stream = new FileStream(result.newPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                }
                File.Delete(sourcePath);
            }
            catch (Exception exception)
            {


                return exception.Message;
            }
            return result.path2;

        }
        public static IResult Delete(string path)
        {
            try
            {
                var currentDirectoryPath = Environment.CurrentDirectory + @"\wwwroot";
                string fullPath = $@"{currentDirectoryPath}{path}";
                File.Delete(fullPath);

            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();

        }
        public static (string newPath, string path2) newPath(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;
            var newPath = Guid.NewGuid() + fileExtension;
            string path = Environment.CurrentDirectory + @"\wwwroot\Uploads";
            string result = $@"{path}\{newPath}";
            return (result, $"\\Uploads\\{newPath}");
        }
    }
}
