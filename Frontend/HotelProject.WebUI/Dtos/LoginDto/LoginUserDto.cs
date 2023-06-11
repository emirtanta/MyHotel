using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.WebUI.Dtos.LoginDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage ="Kullanıcı adı zorunludur")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Şifre zorunludur")]
        public string Password { get; set; }
    }
}
