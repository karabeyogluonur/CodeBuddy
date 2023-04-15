using CB.Application.Abstractions.Services.Media;
using CB.Application.Utilities.Helpers;
using CB.Domain.Enums.Media;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CB.Services.Media
{
    public class FileService : IFileService
    {
        public readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete(Path.Combine(path, fileName));
        }

        public async Task<string> FileRenameAsync(string fileName, string path, bool overwrite = false)
        {
            return await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldFileName = Path.GetFileNameWithoutExtension(fileName);
                string regulatedFileName = CommonHelper.CharacterRegularity(oldFileName);

                var files = Directory.GetFiles(path, regulatedFileName + "*");

                if (files.Length == 0) return regulatedFileName + extension;

                if (files.Length == 1) return regulatedFileName + "-2" + extension;

                int[] fileNumbers = new int[files.Length];
                int lastHyphenIndex;
                for (int i = 0; i < files.Length; i++)
                {
                    lastHyphenIndex = files[i].LastIndexOf("-");
                    if (lastHyphenIndex == -1)
                        fileNumbers[i] = 1;
                    else
                        fileNumbers[i] = int.Parse(files[i].Substring(lastHyphenIndex + 1, files[i].Length - extension.Length - lastHyphenIndex - 1));
                }
                var biggestNumber = fileNumbers.Max();
                biggestNumber++;
                return regulatedFileName + "-" + biggestNumber + extension;
            });

        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new(path);
            return directoryInfo.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        {
            return File.Exists(Path.Combine(path, fileName));
        }

        public async Task<(string fileName, string path)> UploadAsync(IFormFile formFile)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileHelper.GetCommonPath());
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var newFileName = await FileRenameAsync(formFile.FileName, uploadPath);
            await CopyFileAsync(Path.Combine(uploadPath, newFileName), formFile);
            return (newFileName, uploadPath);
        }

        public async Task<(string fileName, string path)> UploadAsync(IFormFile formFile, string specialName)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileHelper.GetCommonPath());
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            if (String.IsNullOrEmpty(Path.GetExtension(specialName)))
                specialName = specialName + Path.GetExtension(formFile.FileName);

            var newFileName = await FileRenameAsync(specialName, uploadPath);
            await CopyFileAsync(Path.Combine(uploadPath, newFileName), formFile);
            return (newFileName, uploadPath);
        }

        public async Task<(string fileName, string path)> UploadAsync(IFormFile formFile, string specialName, RegisteredFileType registeredFileType)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileHelper.GetRegisteredFilePath(registeredFileType));
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            if(String.IsNullOrEmpty(Path.GetExtension(specialName)))
                specialName = specialName + Path.GetExtension(formFile.FileName);        

            var newFileName = await FileRenameAsync(specialName, uploadPath);
            await CopyFileAsync(Path.Combine(uploadPath, newFileName), formFile);
            return (newFileName, uploadPath);
        }

        public Task<List<(string fileName, string path)>> UploadRangeAsync(IFormFileCollection formFileColletion)
        {
            throw new NotImplementedException(); //todo: Upload Range Async
        }

        public Task<List<(string fileName, string path)>> UploadRangeAsync(IFormFileCollection formFileColletion, string specialName)
        {
            throw new NotImplementedException(); //todo: Upload Range Async
        }

        public Task<List<(string fileName, string path)>> UploadRangeAsync(IFormFileCollection formFileColletion, string specialName, RegisteredFileType registeredFileType)
        {
            throw new NotImplementedException(); //todo: Upload Range Async
        }
    }
}
