using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using Feedback_System.Services.Interfaces;

namespace Feedback_System.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private IHostingEnvironment _hostingEnvironment;
        private IExcelUploadService _excelUploadService;

        public FileUploadController(IHostingEnvironment hostingEnvironment, IExcelUploadService excelUploadService)
        {
            _hostingEnvironment = hostingEnvironment;
            _excelUploadService = excelUploadService;
        }
        [HttpGet]
        //[Route("/getUserData")]
        public ActionResult<int> Get()
        {
            return 123;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("test")]
        public ActionResult<bool> Upload()
        {
            string fullPath = "";
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                if (string.IsNullOrWhiteSpace(webRootPath))
                {
                    webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                _excelUploadService.ReadExcel(fullPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
