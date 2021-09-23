using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Transporter
{
    public class VehicleInfoModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private VehicleService _vehicleService;
        private UserService _userService;


        public VehicleInfoModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _vehicleService = new VehicleService();
            _userService = new UserService();
        }


        public string ReturnUrl { get; set; }

        [BindProperty]
        public Models.Vehicle Vehicle { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // return page
            }
            var vehicle = Vehicle;
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));
            vehicle.UserId = customUser.Id;

            var userVehicles = customUser.Vehicles;

            if (userVehicles == null)
            {
                userVehicles = new List<Models.Vehicle>();
                userVehicles.Add(vehicle);
                customUser.Vehicles = userVehicles;
            }
            else
            {
                userVehicles.Add(vehicle);
            }


            _vehicleService.Add(vehicle);
            _vehicleService.Save();

            return RedirectToPage("MyVehicles");
        }

        
    }
}
