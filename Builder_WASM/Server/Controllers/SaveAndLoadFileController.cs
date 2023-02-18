using Builder_WASM.Server.Services;
using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.Net.Http.Headers;
using System.Reflection;

namespace Builder_WASM.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaveAndLoadFileController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SaveAndLoadFileController(IUnitOfWork context, IWebHostEnvironment webHostEnvironment)
        {
            _context= context;
            _webHostEnvironment= webHostEnvironment;
        }

        // GET: api/SaveAndLoadFile/load/{fileName}
        [HttpGet("load/{fileName}")]
        public async Task<ActionResult<FileData>> Get(string fileName) 
        {
            string folderName = Path.Combine("Resources", "userFiles");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string fullPath = Path.Combine(pathToSave, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                return BadRequest(new { message = "File not found!"});
            }

            FileData file = new FileData();
            file.Data = await System.IO.File.ReadAllBytesAsync(fullPath);            
            file.FileType = System.Net.Mime.MediaTypeNames.Application.Pdf;            

            return Ok(file);
        }



        // POST: api/SaveAndLoadFile/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]        
        public async Task<ActionResult> Post([FromBody] FileData file)
        {           
            string path = await GetFullPath(file.Extension, file.Name);
            string fullPath = await SaveFile(file, path);                       

            return Ok(new { message = fullPath });
        }






        private async Task<int> GetCompanyId()
        {
            var userName = User?.Identity?.Name;
            var id = (await _context.UserRegisteredRepository.GetAsync(x => x.Name == userName))?.FirstOrDefault()?.CompanyId ?? 0;

            return id;
        }
        private async Task<string> GetFullPath(string fileExtension, string fileName = "")
        {
            string companyId = (await GetCompanyId()).ToString();

            string folderName = Path.Combine("Resources", "userFiles");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            
            string name = (string.IsNullOrWhiteSpace(fileName)) ? $"{companyId}logo.{fileExtension}" : $"{fileName}.{fileExtension}";
            string fullPath = Path.Combine(pathToSave, name);            
            string dbPath = Path.Combine(folderName, name);

            return dbPath;
        }
        private async Task<string> SaveFile(FileData file, string fullPath)
        {
            using (var fileStream = System.IO.File.Create(fullPath))
            {
                await fileStream.WriteAsync(file.Data);
            }
            return fullPath;
        }
    }
}
