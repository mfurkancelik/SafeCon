using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Address
{
    public class EditAddressModel : PageModel
    {
        private AddressService _addressService;

        public EditAddressModel()
        {
            _addressService = new AddressService();
        }

        [BindProperty]
        public Models.Address Address { get; set; }

        public void OnGet(Guid id)
        {
            if (id != null)
            {
                var data = _addressService.GetById(id);
                Address = data;
            }
        }

        public ActionResult OnPost(Guid id)
        {
            var address = Address;
            var data = _addressService.GetById(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            data.Line = address.Line;
            data.City = address.City;
            data.Country = address.Country;
            data.ZipCode = address.ZipCode;


            _addressService.Update(data);
            _addressService.Save();

            return RedirectToPage("MyAddresses");
        }
    }
}
