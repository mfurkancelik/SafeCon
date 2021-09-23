using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Transporter
{
    public class MyVehiclesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private VehicleService _vehicleService;
        private UserService _userService;

        public MyVehiclesModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _vehicleService = new VehicleService();
            _userService = new UserService();
        }

        public List<Models.Vehicle> VehicleList { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));

            var data = _vehicleService.GetAll(vehicle => vehicle.UserId.Equals(customUser.Id));

            VehicleList = data;
        }

        public ActionResult OnGetDelete(Guid id)
        {
            if (id != null)
            {
                _vehicleService.Delete(id);
                _vehicleService.Save();
            }
            return RedirectToPage("MyVehicle");
        }
    }
}
