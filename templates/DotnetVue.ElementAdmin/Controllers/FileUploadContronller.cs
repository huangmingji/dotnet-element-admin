using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotnetVue.ElementAdmin.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotnetVue.ElementAdmin.Controllers
{
    [ApiController]
    [Route("api/file-upload")]
    public class FileUploadContronller : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileUploadContronller> _logger;
        public FileUploadContronller(IConfiguration configuration, ILogger<FileUploadContronller> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        [HttpPost("")]
        [Authorize]
        public async Task<UploadImgResponseDto> Upload()
        {
            var extensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (!Request.Form.Files.Any())
            {
                return new UploadImgResponseDto("请选择合法文件");
            }
            
            List<string> urls = new List<string>();
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName);
                if (!extensions.Contains(extension))
                {
                    return new UploadImgResponseDto("请选择合法文件");
                }

                if (file.Length > 1048576)
                {
                    return new UploadImgResponseDto("文件太大，请压缩后再上传");
                }

                string filesDirectory = _configuration.GetSection("UploadFileDirectory").Value;
                if (string.IsNullOrWhiteSpace(filesDirectory))
                {
                    filesDirectory = Directory.GetCurrentDirectory();
                }
                
                string directory = $"files/images/{DateTime.Now:yyyyMM}";
                string path = $"{directory}/{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N}{extension}";
                string savePath = Path.Combine(filesDirectory, path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                try
                {
                    await using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }                
                    urls.Add(path);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return new UploadImgResponseDto("文件保存失败");
                }
            }

            return new UploadImgResponseDto(urls);
        }
    }
}