using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Address
{
    public class AddressModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private AddressService _addressService;
        private UserService _userService;

        public AddressModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _addressService = new AddressService();
            _userService = new UserService();
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public Models.Address Address { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // return page
            }
            var address = Address;
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));
            address.UserId = customUser.Id;

            var userAddresses = customUser.Addresses;

            if (userAddresses == null)
            {
                userAddresses =  new List<Models.Address>();
                userAddresses.Add(address);
                customUser.Addresses = userAddresses;
            }
            else
            { 
                userAddresses.Add(address);
            }

            _addressService.Add(address);
            _addressService.Save();

            return RedirectToPage("MyAddresses");
        }
    }
}
