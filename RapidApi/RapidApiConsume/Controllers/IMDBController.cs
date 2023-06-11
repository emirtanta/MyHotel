using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RapidApiConsume.Models;
using Newtonsoft.Json;

namespace RapidApiConsume.Controllers
{
    public class IMDBController : Controller
    {
        //rapid api üzerinden film verisi çekme
        public async Task<IActionResult> Index()
        {
            List<ApiMovieVM> apiMovieVMs=new List<ApiMovieVM>();
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                {
                    { "X-RapidAPI-Key", "554e8ab730msh5251e42ae8164d2p1cbd2ajsnf40e449d2957" },
                    { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                apiMovieVMs = JsonConvert.DeserializeObject<List<ApiMovieVM>>(body);

                return View(apiMovieVMs);
               
            }

        }
    }
}
