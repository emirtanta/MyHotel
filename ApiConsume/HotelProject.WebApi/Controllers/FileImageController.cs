using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //dosya yüklemek için tanımlandı
    public class FileImageController : ControllerBase
    {
        //frontend tarafında AdminImageFileController içersinde işlemler yapıldı
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm]IFormFile file)
        {
            var fileName=Guid.NewGuid()+Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "images/" + fileName);

            var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            return Created("", file);
        }
    }
}
