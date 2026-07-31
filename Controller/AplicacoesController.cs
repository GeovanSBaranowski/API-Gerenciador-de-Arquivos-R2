using Microsoft.AspNetCore.Mvc;
using UploadImagemR2.DTOs.Aplicacoes;
using UploadImagemR2.Services.Interfaces;

namespace UploadImagemR2.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AplicacoesController : ControllerBase
    {
        private readonly IAplicacaoService _service;

        public AplicacoesController(IAplicacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateAplicacaoRequest aplicacaoRequest)
        {
            var response = await _service.CreateAsync(aplicacaoRequest);

            return Ok(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _service.GetByIdAsync(id);

            return Ok(response);
        }
    }
}