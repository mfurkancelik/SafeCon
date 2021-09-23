using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SafeCon.Areas.Identity.Data;
using SafeCon.Services;

namespace SafeCon.Areas.Identity.Pages.Request
{
    public class RequestModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AddressService _addressService;
        private readonly RequestService _requestService;
        private UserService _userService;


        public RequestModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _addressService = new AddressService();
            _requestService = new RequestService();
            _userService = new UserService();
        }

        public List<SelectListItem> PickUpAddresses { get; set; }

        public List<SelectListItem> DestinationAddresses { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string PickUpAddressId { get; set; }

        [BindProperty]
        public string PickUpAddress { get; set; }

        [BindProperty]
        public string DestinationAddressId { get; set; }
        [BindProperty]
        public string DestinationAddress { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "Transportation Start Time")]
            public DateTime Start { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "Transportation End Time")]
            public DateTime End { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Freight Amount")]
            public double FreightAmount { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Freight Type")]
            public string FreightType { get; set; }
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));

            PickUpAddresses = _addressService.GetAll(address => address.UserId.Equals(customUser.Id)).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Line + "-" + a.City + "-" + a.Country +"-" + a.ZipCode
                                  }).ToList();

            DestinationAddresses = _addressService.GetAll(address => address.UserId.Equals(customUser.Id)).Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Line + "-" + a.City + "-" + a.Country + "-" + a.ZipCode
                                  }).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // return page
            }

            var user = await _userManager.GetUserAsync(User);
            var customUser = _userService.GetByDefault(u => u.Email.Equals(user.Email));

            var request = new Models.Request
            {
                Start = Input.Start,
                End = Input.End,
                FreightAmount = Input.FreightAmount,
                FreightType = Input.FreightType.ToString()
            };

            request.UserId = customUser.Id;
            request.PickUpAddressId = new Guid(PickUpAddressId);
            request.DestinationAddressId = new Guid(DestinationAddressId);

            _requestService.Add(request);
            _requestService.Save();

            return RedirectToPage("MyRequests");
        }
    }
}
