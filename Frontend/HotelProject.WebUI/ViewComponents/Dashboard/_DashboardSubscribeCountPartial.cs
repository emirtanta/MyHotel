using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelProject.WebUI.Dtos.FollowersDto;
using Newtonsoft.Json;
using System.Linq;

namespace HotelProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardSubscribeCountPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://instagram-profile1.p.rapidapi.com/getprofileinfo/emrtnt"),
                Headers =
                {
                    { "X-RapidAPI-Key", "554e8ab730msh5251e42ae8164d2p1cbd2ajsnf40e449d2957" },
                    { "X-RapidAPI-Host", "instagram-profile1.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                //tek veri döndüğünde kullanılır
                ResultInstagramFollowersDto resultInstagramFollowersDtos = JsonConvert.DeserializeObject<ResultInstagramFollowersDto>(body);

                //return View(resultInstagramFollowersDtos);

                //viewbag ile dönüş yapma durumu da olabilir
                ViewBag.followers = resultInstagramFollowersDtos.followers;
                ViewBag.following = resultInstagramFollowersDtos.following;

                
            }

            #region rapid api içerisinden twitter api ile takipçi sayısı çekme bölümü

            
            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twitter32.p.rapidapi.com/getProfile?username=nike"),
                Headers =
                {
                    { "X-RapidAPI-Key", "554e8ab730msh5251e42ae8164d2p1cbd2ajsnf40e449d2957" },
                    { "X-RapidAPI-Host", "twitter32.p.rapidapi.com" },
                },
            };
            using (var response2 = await client.SendAsync(request))
            {
                response2.EnsureSuccessStatusCode();
                var body2 = await response2.Content.ReadAsStringAsync();

                ResultTwitterFollowersDto resultTwitterFollowersDto=JsonConvert.DeserializeObject<ResultTwitterFollowersDto>(body2);

                ViewBag.followersCount = resultTwitterFollowersDto.data.user_info.followers_count;
                ViewBag.friendsCount = resultTwitterFollowersDto.data.user_info.friends_count;
                
            }



            #endregion


            #region rapid api üzerinden linkedin takipçi sayısını çekme


            var client3 = new HttpClient();
            var request3 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://fresh-linkedin-profile-data.p.rapidapi.com/get-linkedin-profile?linkedin_url=https%3A%2F%2Fwww.linkedin.com%2Fin%2Femir-tanta-0b1589b0%2F"),
                Headers =
                {
                    { "X-RapidAPI-Key", "554e8ab730msh5251e42ae8164d2p1cbd2ajsnf40e449d2957" },
                    { "X-RapidAPI-Host", "fresh-linkedin-profile-data.p.rapidapi.com" },
                },
            };
            using (var response3 = await client.SendAsync(request))
            {
                response3.EnsureSuccessStatusCode();
                var body3 = await response3.Content.ReadAsStringAsync();

                ResultLinkedInFollowersDto resultLinkedInFollowersDto = JsonConvert.DeserializeObject<ResultLinkedInFollowersDto>(body3);

                ViewBag.followersCount = resultLinkedInFollowersDto.data.followers_count;
            }

            #endregion

            return View();
        }
    }
}
