using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.IO.Compression;

namespace TauriServer_netdore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UpdateServerController : ControllerBase
    {
      
    
        private static readonly Update update = new(){
          version="0.2.0",
          pub_date= System.DateTime.Now,
            url = "https://github.com/aviconcentrix/TauriServer_netdore/blob/master/TauriServer_netdore/assets/EndPoint_0.2.0_x64_en-US.msi.zip",
          //url = "https://github.com/aviconcentrix/TauriServer_netdore/raw/master/TauriServer_netdore/assets/EndPoint_0.2.0_x64_en-US.msi.zip",
          signature= "dW50cnVzdGVkIGNvbW1lbnQ6IHNpZ25hdHVyZSBmcm9tIHRhdXJpIHNlY3JldCBrZXkKUlVRQ2ZmUmVmNVBnT1cwUldROTIvaytoc25Cby9KVWZPSy9NbUNick80azRTTkU2QTl2aGJaeWxpY0VrOWsrdFg4eDFFMHNLYVJGaE5SN0h3bUY3eEZKeTlFMFVzS0J1NmdRPQp0cnVzdGVkIGNvbW1lbnQ6IHRpbWVzdGFtcDoxNzAxMzQzMjI0CWZpbGU6RW5kUG9pbnRfMC4yLjBfeDY0X2VuLVVTLm1zaS56aXAKZi82aituY0RZQ1FYc1NPZ210QmNidkRERWJaUC9CT1d0ZzBIVWJ2RmdhejlLU1RPUGM3eEc4TFdPT05VUXljaE9kL1lMeStiVDhYcEI4TmU3Y2VVRHc9PQo=",
          notes="UI Changes!.."
        };

        private readonly ILogger<UpdateServerController> _logger;

        public UpdateServerController(ILogger<UpdateServerController> logger)
        {
            _logger = logger;
           // _contentTypeProvider = contentTypeProvider;
        }

        [HttpGet(Name = "GetUpdate")]
        [Route("{target}/{arch}/{current_version}")]
        public Update Get(string target, string arch, string current_version)
        {
            return update;
        }
        [HttpGet]
        [Route("download")]
        public IActionResult DownloadFiles()
        {
            try
            {
                var version = "EndPoint_0.2.0_x64_en-US.msi.zip";
                // var folderPath = "C:\\WorkingDir\\Rust\\POC\\Tauri\\withangular\\EndPoint\\src-tauri\\target\\release\\bundle\\msi\\";
               var folderPath =Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"assets");

                // Ensure the folder exists
                if (!Directory.Exists(folderPath))
                    return NotFound("Folder not found.");

                // Get a list of files in the folder
                var files = Directory.GetFiles(folderPath, version).FirstOrDefault();

                if (files.Length == 0)
                    return NotFound("No files found to download.");

                using (var memoryStream = new MemoryStream())
                {
                    using (var fileStream = new FileStream(files, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.CopyTo(memoryStream);
                    }
                    // Return the zip file as a byte array
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Return the zip file as a byte array
                    return File(memoryStream.ToArray(), "application/zip", version);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: {ex.Message}");
            }
        }
    }
}