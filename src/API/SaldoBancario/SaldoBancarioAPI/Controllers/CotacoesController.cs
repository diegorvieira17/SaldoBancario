using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaldoBancarioAPI.Data.Interfaces;
using SaldoBancarioAPI.Model;
using SaldoBancarioAPI.Services.HGCotacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacoesController : ControllerBase
    {
        private readonly IMoedaRepository _moedaRepository;

        public CotacoesController(IMoedaRepository moedaRepository)
        {
            _moedaRepository = moedaRepository;
        }

        // GET: api/Cotacoes
        [HttpGet]
        public async Task<IActionResult> GetCotacoes()

        {
            try
            {
                // TODO - Corrigir retorno. Caindo na exception qdo vem nulo
                List<Moeda> lastMoedas = new List<Moeda>();
                if (await _moedaRepository.GetByName("Dollar") != null)
                    lastMoedas.Add(await _moedaRepository.GetByName("Dollar"));
                if (await _moedaRepository.GetByName("Euro") != null)
                    lastMoedas.Add(await _moedaRepository.GetByName("Euro"));
                if (await _moedaRepository.GetByName("Bitcoin") != null)
                    lastMoedas.Add(await _moedaRepository.GetByName("Bitcoin"));

                var response = await HGCotacoes.GetCotacoes();
                if (response != null)
                {
                    List<Moeda> moedas = new List<Moeda>();
                    moedas.Add(new Moeda() {
                        Id = Guid.NewGuid(),
                        Nome = response.Usd.Name,
                        Compra = Convert.ToDecimal(response.Usd.Buy),
                        Venda = Convert.ToDecimal(response.Usd.Sell),
                        Variacao = Convert.ToDecimal(response.Usd.Variation),
                        DataCotacao = DateTime.Now
                    });

                    moedas.Add(new Moeda()
                    {
                        Id = Guid.NewGuid(),
                        Nome = response.Eur.Name,
                        Compra = Convert.ToDecimal(response.Eur.Buy),
                        Venda = Convert.ToDecimal(response.Eur.Sell),
                        Variacao = Convert.ToDecimal(response.Eur.Variation),
                        DataCotacao = DateTime.Now
                    });
                    moedas.Add(new Moeda()
                    {
                        Id = Guid.NewGuid(),
                        Nome = response.Btc.Name,
                        Compra = Convert.ToDecimal(response.Btc.Buy),
                        Venda = Convert.ToDecimal(response.Btc.Sell),
                        Variacao = Convert.ToDecimal(response.Btc.Variation),
                        DataCotacao = DateTime.Now
                    });

                    foreach(Moeda m in lastMoedas)
                    {
                        Moeda mo = moedas.Where(x => x.Nome == m.Nome).FirstOrDefault();

                        if (m.Equals(mo))
                            continue;

                        _moedaRepository.Add(mo);
                        await _moedaRepository.SaveChangesAsync();
                    }

                    return Ok(moedas);
                }
                else
                {
                    if (lastMoedas.Count > 0)
                    {
                        return Ok(lastMoedas);
                    }
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }

            return BadRequest();
        }
    }
}
