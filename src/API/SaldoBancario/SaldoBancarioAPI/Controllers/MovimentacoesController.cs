using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaldoBancarioAPI.Data.Interfaces;
using SaldoBancarioAPI.Model;
using SaldoBancarioAPI.Services;
using System;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacoesController : ControllerBase
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly IImportacaoRepository _importacaoRepository;

        public MovimentacoesController(IMovimentacaoRepository movimentacaoRepository, IImportacaoRepository importacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
            _importacaoRepository = importacaoRepository;
        }

        // GET: api/Movimentacoes
        [HttpGet]
        public async Task<IActionResult> GetMovimentacoes()
        {
            try
            {
                return Ok(await _movimentacaoRepository.Get());

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        // POST: api/Movimentacoes
        [HttpPost]
        public async Task<IActionResult> PostMovimentacao(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    var filePath = UploadFiles.Upload(file);

                    Importacao importacao = new Importacao();
                    importacao.Id = Guid.NewGuid();
                    importacao.DataImportacao = DateTime.Now;
                    importacao.NomeArquivo = filePath;
                    importacao.Movimentacoes = Extrato.MontaExtrato(await _movimentacaoRepository.GetSaldo(), importacao);

                    _importacaoRepository.Add(importacao);

                    if (await _importacaoRepository.SaveChangesAsync())
                    {
                        return Ok(await _movimentacaoRepository.Get());
                    }
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no upload do arquivo.");
            }
        }
    }
}
