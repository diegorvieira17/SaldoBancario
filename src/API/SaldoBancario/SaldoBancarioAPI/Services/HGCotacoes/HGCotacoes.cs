using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Services.HGCotacoes
{
    public static class HGCotacoes
    {
        public async static Task<Currencies> GetCotacoes()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = "https://api.hgbrasil.com/finance";

                var response = await client.GetStringAsync(url);
                //var cotacoes = JsonConvert.DeserializeObject<Currencies>(response);
                var cotacoes = ConvertJson.FromJson(response);


                return cotacoes.Results.Currencies;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
