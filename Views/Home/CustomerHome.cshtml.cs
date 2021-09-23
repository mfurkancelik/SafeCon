using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SafeCon.Areas.Identity.Data;

namespace SafeCon.Views.Home
{
    
        public class CostumerHomeModel : PageModel
        {
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger<CostumerHomeModel> _logger;
            private readonly IEmailSender _emailSender;

            public CostumerHomeModel(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILogger<CostumerHomeModel> logger,
                IEmailSender emailSender)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
                _emailSender = emailSender;
            }

            [BindProperty]
            public InputModel Input { get; set; }

            public string ReturnUrl { get; set; }

            public IList<AuthenticationScheme> ExternalLogins { get; set; }

            public class InputModel
            {
                [Required]
                [Display(Name = "Type of the cargo")]
                public string TypeofCargo { get; set; }

                [Required]
                [DataType(DataType.Text)]
                [Display(Name = "How many tons of cargo?")]
                public string CargoTons { get; set; }

                [Required]
                [Display(Name = "Starting Point")]
                public string StartPoint { get; set; }

                [Required]
                [DataType(DataType.Text)]
                [Display(Name = "Point of Arrival")]
                public string PointArrival { get; set; }

                [Required]
                [Display(Name = "In how many days do you want to send your cargo?")]
                public string WantCargo { get; set; }

                [Required]
                [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
                [DataType(DataType.Text)]
                [Display(Name = "Tax Address")]
                public string TaxAddress { get; set; }
            }

            public async Task OnGetAsync(string returnUrl = null)
            {
                ReturnUrl = returnUrl;
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            }

            
        }
    }
