using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.AppUserDto;
using HotelProject.WebUI.Dtos.RoomDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //api consume

            //var client = _httpClientFactory.CreateClient();

            //var responseMessage = await client.GetAsync("http://localhost:1179/api/AppUser"); //apinin çalışma adresi

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();

            //    var values = JsonConvert.DeserializeObject<List<ResultAppUserDto>>(jsonData);

            //    return View(values);
            //}

            //api consume bitti

            return View();
        }

        public async Task<IActionResult> UserList()
        {
            //api consume

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("http://localhost:1179/api/AppUser"); //apinin çalışma adresi

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultAppUserListDto>>(jsonData);

                return View(values);
            }

            //api consume bitti

            return View();
        }
    }
}
