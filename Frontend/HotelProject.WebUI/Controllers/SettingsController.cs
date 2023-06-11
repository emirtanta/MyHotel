using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Models.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;


        public SettingsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        #region Kullanıcı Bilgileri ve Şifre Güncelleme Bölümü

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user=await _userManager.FindByNameAsync(User.Identity.Name);

            UserEditVM userEditVM = new UserEditVM();
            userEditVM.Name=user.Name;
            userEditVM.Surname=user.Surname;
            userEditVM.Username = user.UserName;
            userEditVM.Email = user.Email;

            return View(userEditVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditVM userEditVM)
        {
            if (userEditVM.Password==userEditVM.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.Name = userEditVM.Name;
                user.Surname = userEditVM.Surname;
                user.Email = userEditVM.Email;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditVM.Password);

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        #endregion
    }
}
