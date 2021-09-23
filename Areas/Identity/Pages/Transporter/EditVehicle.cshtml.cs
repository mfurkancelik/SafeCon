using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Models;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Transporter
{
    public class EditVehicleModel : PageModel
    {
        private VehicleService _vehicleService;

        public EditVehicleModel()
        {
            _vehicleService = new VehicleService();
        }

        [BindProperty]
        public Models.Vehicle Vehicle { get; set; }

        public void OnGet(Guid id)
        {
            if (id != null)
            {
                var data = _vehicleService.GetById(id);
                Vehicle = data;
            }
        }

        public ActionResult OnPost(Guid id)
        {
            var vehicle = Vehicle;
            var data = _vehicleService.GetById(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            data.VehicleName = vehicle.VehicleName;
            data.InsurancePolicyNumber = vehicle.InsurancePolicyNumber;
            data.TrailerType = vehicle.TrailerType;
            data.VehicleCapacity = vehicle.VehicleCapacity;


            _vehicleService.Update(data);
            _vehicleService.Save();

            return RedirectToPage("MyAddresses");
        }
    }
}

