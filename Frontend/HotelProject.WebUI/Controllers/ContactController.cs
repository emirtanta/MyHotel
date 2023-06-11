using HotelProject.WebUI.Dtos.BookingDto;
using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Dtos.MessageCategoryDto;
using HotelProject.WebUI.Dtos.RoomDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class ContactController : Controller
    {
        //veritabanından gelen verileri dinamik olarak göstermek için tanımlandı
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //api consume

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("http://localhost:1179/api/MessageCategory"); //apinin çalışma adresi


            var jsonData = await responseMessage.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<List<ResultMessageCategoryDto>>(jsonData);

            #region message kategorilerini dropdown içerisinde listeleme
            
            List<SelectListItem> values2=(from x in values
                                          select new SelectListItem 
                                          {
                                             Text=x.MessageCategoryName,
                                             Value=x.MessageCategoryID.ToString(),
                                          }).ToList();

            ViewBag.v = values2;

            #endregion

            return View();

        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateContactDto createContactDto)
        {
            createContactDto.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PostAsync("http://localhost:1179/api/Contact", stringContent);

            return RedirectToAction("Index", "Default");
        }
    }
}
