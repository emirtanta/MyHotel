using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();

            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleVM addRoleVM)
        {
            AppRole appRole = new AppRole()
            {
                Name = addRoleVM.RoleName
            };

            var result = await _roleManager.CreateAsync(appRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            UpdateRoleVM updateRoleVM = new UpdateRoleVM()
            {
                RoleID = value.Id,
                RoleName = value.Name
            };

            return View(updateRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleVM updateRoleVM)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == updateRoleVM.RoleID);

            value.Name = updateRoleVM.RoleName;

            await _roleManager.UpdateAsync(value);

            return RedirectToAction("Index");
        }

        public async  Task<IActionResult> DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            await _roleManager.DeleteAsync(value);

            return RedirectToAction("Index");
        }

        
    }
}
