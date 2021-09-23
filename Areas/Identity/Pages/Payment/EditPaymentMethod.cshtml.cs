using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Payment
{
    public class EditPaymentMethodModel : PageModel
    {

        private PaymentService _paymentService;

        public EditPaymentMethodModel()
        {
            _paymentService = new PaymentService();
        }

        [BindProperty]
        public Models.Payment Payment { get; set; }

        public void OnGet(Guid id)
        {
            if (id != null)
            {
                var data = _paymentService.GetById(id);
                Payment = data;
            }
        }

        public ActionResult OnPost(Guid id)
        {
            var payment = Payment;
            var data = _paymentService.GetById(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            data.CardName = payment.CardName;
            data.CardNumber = payment.CardNumber;
            data.Skt = payment.Skt;
            data.Cvv = payment.Cvv;


            _paymentService.Update(data);
            _paymentService.Save();

            return RedirectToPage("MyPaymentMethods");
        }
    }
}
