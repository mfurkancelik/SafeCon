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
    public class PaymentMethodModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private UserService _userService;
        private PaymentService _paymentService;

        public PaymentMethodModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userService = new UserService();
            _paymentService = new PaymentService();
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public Models.Payment Payment { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // return page
            }
            var payment = Payment;
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));
            payment.UserId = customUser.Id;

            var userPayments = customUser.Payments;

            if (userPayments == null)
            {
                userPayments = new List<Models.Payment>();
                userPayments.Add(payment);
                customUser.Payments = userPayments;
            }
            else
            {
                userPayments.Add(payment);
            }

            _paymentService.Add(payment);
            _paymentService.Save();

            return RedirectToPage("MyPaymentMethods");
        }
    }
}
