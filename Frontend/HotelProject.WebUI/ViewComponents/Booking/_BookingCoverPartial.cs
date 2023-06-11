using HotelProject.WebUI.Dtos.AboutDto;
using HotelProject.WebUI.Dtos.ServiceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelProject.WebUI.ViewComponents.Default
{
    public class BookingCoverPartial:ViewComponent
    {

        //veritabanından gelen verileri dinamik olarak göstermek için tanımlandı
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingCoverPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //veritabanından gelen verileri dinamik olarak göstermek için tanımlandı
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //api consume

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("http://localhost:1179/api/About"); //apinin çalışma adresi

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);

                return View(values);
            }

            //api consume bitti

            return View();
        }
    }
}
