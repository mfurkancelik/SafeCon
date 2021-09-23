using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Request
{
    public class EditRequestModel : PageModel
    {
        private readonly RequestService _requestService;

        public EditRequestModel()
        {
            _requestService = new RequestService();
        }

        [BindProperty]
        public Models.Request Request { get; set; }

        public void OnGet(Guid id)
        {
            if (id != null)
            {
                var data = _requestService.GetById(id);
                Request = data;
            }
        }

        public ActionResult OnPost(Guid id)
        {
            var request = Request;
            var data = _requestService.GetById(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            data.Start = request.Start;
            data.End = request.End;
            data.FreightAmount = request.FreightAmount;
            data.FreightType = request.FreightType;


            _requestService.Update(data);
            _requestService.Save();

            return RedirectToPage("MyRequests");
        }
    }
}
