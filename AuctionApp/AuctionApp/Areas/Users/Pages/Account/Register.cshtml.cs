// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Areas.Users.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AuctionUser> _signInManager;
        private readonly UserManager<AuctionUser> _userManager;
        private readonly IUserStore<AuctionUser> _userStore;

        public RegisterModel(
            UserManager<AuctionUser> userManager,
            IUserStore<AuctionUser> userStore,
            SignInManager<AuctionUser> signInManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                // Custom properties
                user.Email = Input.Email;
                user.RecievedMessages = new List<AuctionPrivateMessage>();
                user.SentMessages = new List<AuctionPrivateMessage>();

                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);

                    if (await _userManager.Users.CountAsync() == 3)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private AuctionUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AuctionUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AuctionUser)}'. " +
                    $"Ensure that '{nameof(AuctionUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
