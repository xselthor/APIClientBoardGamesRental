using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APIClientBoardGamesRental.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData] public string StatusMessage { get; set; }

        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string ShortName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string Country { get; set; }
            public string NIP { get; set; }
            public string ContactPerson { get; set; }
            public string Comments { get; set; }
            public string ContactPhone { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ShortName = user.ShortName,
                Street = user.Street,
                City = user.City,
                PostCode = user.PostCode,
                Country = user.Country,
                NIP = user.NIP,
                ContactPerson = user.ContactPerson,
                Comments = user.Comments,
                ContactPhone = user.ContactPhone
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.ShortName != user.ShortName)
            {
                user.ShortName = Input.ShortName;
            }

            if (Input.Street != user.Street)
            {
                user.Street = Input.Street;
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
            }

            if (Input.PostCode != user.PostCode)
            {
                user.PostCode = Input.PostCode;
            }

            if (Input.Country != user.Country)
            {
                user.Country = Input.Country;
            }

            if (Input.NIP != user.NIP)
            {
                user.NIP = Input.NIP;
            }

            if (Input.ContactPerson != user.ContactPerson)
            {
                user.ContactPerson = Input.ContactPerson;
            }

            if (Input.Comments != user.Comments)
            {
                user.Comments = Input.Comments;
            }

            if (Input.ContactPhone != user.ContactPhone)
            {
                user.ContactPhone = Input.ContactPhone;
            }
            
            await _userManager.UpdateAsync(user);
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}