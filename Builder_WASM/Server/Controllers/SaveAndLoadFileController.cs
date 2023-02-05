using Builder_WASM.Server.Services;
using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // POST: api/SaveAndLoadFile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("path")]
        public async Task<ActionResult> Post([FromBody]FileData file)
        {   
            var folderName = Path.Combine("Resources", "userFiles");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = $"{Guid.NewGuid()}.{file.Extension}";
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);
                        
            using (var fileStream = System.IO.File.Create(fullPath))
            {
                await fileStream.WriteAsync(file.Data);
            }
            
            return Ok(new { message = fullPath });
        }
    }
}
