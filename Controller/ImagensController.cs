
using Microsoft.AspNetCore.Mvc;
using UploadImagemR2.Models;
using UploadImagemR2.Services;

namespace UploadImagemR2.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagensController : ControllerBase
    {
        private readonly IR2StorageService _r2Storage;

        public ImagensController(IR2StorageService r2Storage)
        {
            _r2Storage = r2Storage;
        }

        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest("Nenhum arquivo enviado");
            }
            var upload = await _r2Storage.UploadAsync(arquivo);
            return Ok(upload);
        }

        [HttpGet("{arquivo}")]
        public async Task<IActionResult> DownloadAsync(string arquivo)
        {
            var resultado = await _r2Storage.DownloadAsync(arquivo);

            return File(
                resultado.Bytes,
                resultado.ContentType,
                resultado.FileName
            );
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var resultado = await _r2Storage.ListAsync();

            return Ok(resultado);
        }

        [HttpDelete("{arquivo}")]
        public async Task<IActionResult> DeletarAsync(string arquivo)
        {
            try
            {
                await _r2Storage.DeleteAsync(arquivo);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}