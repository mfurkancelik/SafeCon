using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Request
{
    public class MyRequestsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private RequestService _requestService;
        private UserService _userService;
        AddressService _addressService;


        public MyRequestsModel(UserManager<ApplicationUser> userManager)
        {
            _requestService = new RequestService();
            _userService = new UserService();
            _userManager = userManager;
            _addressService = new AddressService();
        }

        public List<Models.Request> RequestList { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));

            var todaysDate = DateTime.Now;
            var data = _requestService.GetAll(request => DateTime.Compare(request.Start, todaysDate) > 0 && request.UserId.Equals(customUser.Id));

            
            RequestList = data;
        }

        public ActionResult OnGetDelete(Guid id)
        {
            if (id != null)
            {
                _requestService.Delete(id);
                _requestService.Save();
            }
            return RedirectToPage("MyRequests");
        }
    }
}
