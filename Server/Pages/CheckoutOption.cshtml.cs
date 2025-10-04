using Application.AccountApp;
using Application.UserApp;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels.Pages.Account;

namespace Server.Pages
{
    public class CheckoutOptionModel : BasePageModel
    {
        private readonly IUserApplication _application;

        private readonly DatabaseContext _context;

        public CheckoutOptionModel(IUserApplication application, DatabaseContext context)
        {
            _application = application;
            _context = context;
        }

        [BindProperty]
        public bool GuestCheckout { get; set; }

        public string Status { get; set; }

        [BindProperty]
        public UserRegistrationViewModel ViewModel { get; set; } = new();

        [BindProperty]
        public string? ReturnUrl { get; set; }

        public async Task<IActionResult> OnGet(string? returnUrl)
        {
            ReturnUrl = returnUrl;

            Status = "formquestion";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string status)
        {
            if (GuestCheckout == true && status == "formquestion")
            {
                return RedirectToPage("/Account/Login");
            }
            else if (GuestCheckout == false && status == "formquestion")
            {
                Status = "formregister";

                AddPageWarning(message: "برای ادامه روند خرید اطلاعات خود را وارد کنید");

                return Page();
            }

            if (status == "formregister")
            {
                var user = await _application.AddUserAndRetuenId(ViewModel);


                var claims = new List<Claim>();

                Claim claim;

                claim = GetUserClaims(user, claims);

                var claimsIdentity =
                    new ClaimsIdentity(claims: claims,
                    authenticationType: Infrastructure.Security.Utility.AuthenticationScheme);

                var claimsPrincipal =
                    new ClaimsPrincipal(identity: claimsIdentity);

                var authenticationProperties =
                    new AuthenticationProperties();

                await HttpContext.SignInAsync
                    (scheme: Infrastructure.Security.Utility.AuthenticationScheme,
                    principal: claimsPrincipal, properties: authenticationProperties);


                if (string.IsNullOrWhiteSpace(ReturnUrl))
                {
                    return RedirectToPage(pageName: "/Index");
                }
                else
                {
                    return Redirect(url: ReturnUrl);
                }
            }

            return Page();
        }

        private static Claim GetUserClaims(Domain.Users.User user, List<Claim> claims)
        {
            // **************************************************
            Claim claim = new Claim
                (type: "FullName", value: string.Concat(user.FullName));
            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            claim =
                new Claim
                (type: "Id", value: user.Id.ToString());

            claims.Add(item: claim);
            // **************************************************

            // **************************************************
            claim =
                new Claim
                (type: ClaimTypes.Role, value: user.Role ?? "User");

            claims.Add(item: claim);
            // **************************************************

            return claim;
        }
    }
}
