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
    public class MyAddressesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private AddressService _addressService;
        private UserService _userService;

        public MyAddressesModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _addressService = new AddressService();
            _userService = new UserService();
        }

        public List<Models.Address> AddressList { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));

            var data = _addressService.GetAll(address => address.UserId.Equals(customUser.Id));

            AddressList = data;
        }

        public ActionResult OnGetDelete(Guid id)
        {
            if (id != null)
            {
                _addressService.Delete(id);
                _addressService.Save();
            }
            return RedirectToPage("MyAddresses");
        }
    }
}
