using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;
using static Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal.ExternalLoginModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SafeCon.Areas.Identity.Pages.Transporter
{
    public class RequestListModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AddressService _addressService;
        private readonly RequestService _requestService;
        private UserService _userService;
        private OfferService _offerService;

        public RequestListModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _addressService = new AddressService();
            _requestService = new RequestService();
            _userService = new UserService();
            _offerService = new OfferService();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string RequestId { get; set; }

        [BindProperty]
        public string Request { get; set; }

        [BindProperty]
        public string VehicleId { get; set; }
        [BindProperty]
        public string Vehicle { get; set; }

        [BindProperty]
        public string OfferPrice { get; set; }

        public List<Models.Request> RequestList { get; set; }

        public class InputModel
        {

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Price")]
            public double OfferPrice { get; set; }

        }

        public void OnGet()
        {
            var todaysDate = DateTime.Now;
            var data = _requestService.GetAll(request => DateTime.Compare(request.Start, todaysDate) > 0);

            RequestList = data;
        }


        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // return page
            }
            

            var offer = new Models.Offer();
            {
                OfferPrice = Input.OfferPrice.ToString();

            };

            _offerService.Add(offer);
            _offerService.Save();
            return RedirectToPage("MyOffers");
        }
    }
}
