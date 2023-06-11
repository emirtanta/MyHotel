using HotelProject.WebUI.Dtos.ContactDto;
using HotelProject.WebUI.Dtos.RoomDto;
using HotelProject.WebUI.Dtos.SendMessageDto;
using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Inbox()
        {
            //api consume

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("http://localhost:1179/api/Contact"); //apinin çalışma adresi

            //gelen mesaj sayısını almak için tanımlandı
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client.GetAsync("http://localhost:1179/api/Contact/GetContactCount"); //apinin çalışma adresi

            //gönderilen mesaj sayısını almak için tanımlandı
            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client.GetAsync("http://localhost:1179/api/SendMessage/GetSendMessageCount"); //apinin çalışma adresi

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData);

                //gelen mesaj sayısını alma
                var jsonData2 = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.contactCount = jsonData2;

                //gönderilen mesaj sayısını alma
                var jsonData3 = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.sendMessageCount = jsonData3;

                return View(values);
            }

            //api consume bitti

            return View();
        }

        public async Task<IActionResult> Sendbox()
        {
            //api consume

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("http://localhost:1179/api/SendMessage"); //apinin çalışma adresi

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultSendboxDto>>(jsonData);

                return View(values);
            }

            //api consume bitti

            return View();
        }

        public PartialViewResult SideBarAdminContactParital()
        {
            return PartialView();
        }

        public PartialViewResult SideBarAdminCategoryParital()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult AddSendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSendMessage(CreateSendMessageDto createSendMessageDto)
        {
            createSendMessageDto.SenderMail = "admin@gmail.com";
            createSendMessageDto.SenderName = "admin";
            createSendMessageDto.Date = DateTime.Parse(DateTime.Now.ToShortDateString());

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSendMessageDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://localhost:1179/api/SendMessage", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SendBox");
            }

            return View();
        }

        public async Task<IActionResult> MessageDetailsBySendbox(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:1179/api/SendMessage/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //gelen kutusu detaylar
        public async Task<IActionResult> MessageDetailsByInbox(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:1179/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<InboxContactDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //public async Task<IActionResult> GetContactCount()
        //{
        //    //api consume

        //    var client = _httpClientFactory.CreateClient();

        //    var responseMessage = await client.GetAsync("http://localhost:1179/api/Contact/GetContactCount"); //apinin çalışma adresi

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var jsonData = await responseMessage.Content.ReadAsStringAsync();

        //        //var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData);

        //        ViewBag.data = jsonData;

        //        return View();
        //    }

        //    //api consume bitti

        //    return View();
        //}
    }
}
