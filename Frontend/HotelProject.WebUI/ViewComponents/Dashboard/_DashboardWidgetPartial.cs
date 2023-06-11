using HotelProject.WebUI.Dtos.AppUserDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardWidgetPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardWidgetPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //api consume

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("http://localhost:1179/api/DashboardWidgets/StaffCount"); //apinin çalışma adresi

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                ViewBag.staffCount = jsonData;

                
            }

            #region rezervasyon sayısını api'den çeker

            var client2 = _httpClientFactory.CreateClient();

            var responseMessage2 = await client2.GetAsync("http://localhost:1179/api/DashboardWidgets/BookingCount"); //apinin çalışma adresi

            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                ViewBag.bookingCount = jsonData2;
            }


            #endregion

            #region kullanıcı sayısını api'den çeker

            var client3 = _httpClientFactory.CreateClient();

            var responseMessage3 = await client3.GetAsync("http://localhost:1179/api/DashboardWidgets/AppUserCount"); //apinin çalışma adresi

            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();

                ViewBag.appUserCount = jsonData3;
            }


            #endregion

            #region oda sayısını api'den çeker

            var client4 = _httpClientFactory.CreateClient();

            var responseMessage4 = await client4.GetAsync("http://localhost:1179/api/DashboardWidgets/RoomCount"); //apinin çalışma adresi

            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();

                ViewBag.roomCount = jsonData4;
            }


            #endregion

            //api consume bitti

            return View();
        }
    }
}
