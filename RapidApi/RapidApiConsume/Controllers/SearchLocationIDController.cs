using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RapidApiConsume.Models;
using Newtonsoft.Json;
using System.Linq;

namespace RapidApiConsume.Controllers
{
    public class SearchLocationIDController : Controller
    {
        public async Task<IActionResult> Index(string cityName)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                List<BookingApiLocationSearchVM> model = new List<BookingApiLocationSearchVM>();

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={cityName}&locale=en-gb"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "554e8ab730msh5251e42ae8164d2p1cbd2ajsnf40e449d2957" },
                    { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    model = JsonConvert.DeserializeObject<List<BookingApiLocationSearchVM>>(body);

                    return View(model.Take(1).ToList());
                }
            }

            else
            {
                List<BookingApiLocationSearchVM> model = new List<BookingApiLocationSearchVM>();

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/hotels/locations?name=paris&locale=en-gb"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "554e8ab730msh5251e42ae8164d2p1cbd2ajsnf40e449d2957" },
                    { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
                },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    model = JsonConvert.DeserializeObject<List<BookingApiLocationSearchVM>>(body);

                    return View(model.Take(1).ToList());
                }
            }

            
        }
    }
}
