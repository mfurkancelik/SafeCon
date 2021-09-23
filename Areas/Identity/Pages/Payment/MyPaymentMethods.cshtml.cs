using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Payment
{
    public class MyPaymentMethodsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private PaymentService _paymentService;
        private UserService _userService;

        public MyPaymentMethodsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _paymentService = new PaymentService();
            _userService = new UserService();
        }

        public List<Models.Payment> PaymentList { get; set; }
        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));

            var data = _paymentService.GetAll(address => address.UserId.Equals(customUser.Id));

            PaymentList = data;
        }

        public ActionResult OnGetDelete(Guid id)
        {
            if (id != null)
            {
                _paymentService.Delete(id);
                _paymentService.Save();
            }
            return RedirectToPage("MyPaymentMethods");
        }
    }
}
