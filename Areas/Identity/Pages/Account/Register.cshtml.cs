// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Works_Life_Cycle.Data;
using Works_Life_Cycle.Models;
namespace Works_Life_Cycle.Areas.Identity.Pages.Account {
    //lets users who have not been authenticated access the action or controller
    [AllowAnonymous]
    public class RegisterModel : PageModel {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ILogger<LoginModel> _logger1;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Possui a referência à base de dados
        /// </summary>
        private readonly WorksLifeCycleDB _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            ILogger<LoginModel> logger1,
            IEmailSender emailSender,
            WorksLifeCycleDB context) {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _logger1 = logger1;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null) {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            if (!(Input.Email.Contains("aluno") || Input.Email.Contains("professor") || Input.Email.Contains("secretaria") || Input.Email.Contains("admin"))) {
                ModelState.AddModelError(string.Empty, "Email must contain aluno/professor/secretary");
                return Page();
            }

            if (ModelState.IsValid) {
                var user = new IdentityUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    EmailConfirmed = false,
                    LockoutEnabled = true
                };

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded) {
                    _logger.LogInformation("User created a new account with password.");


                    Person person = new Person {
                        Email = Input.Email,
                        UserNameID = user.Id
                    };
                    if (Input.Email.Contains("@ipt.pt"))
                    {
                        //incase the user inserted into the email box a string that contains "aluno"
                        if (Input.Email.Contains("aluno"))
                        {

                            person.Role = "Aluno";
                            await _userManager.AddToRoleAsync(user, "Student");
                        }
                        //incase the user inserted into the email box a string that contains "professor"
                        else if (Input.Email.Contains("professor"))
                        {
                            person.Role = "Professor";
                            await _userManager.AddToRoleAsync(user, "Teacher");
                        }
                        //incase the user inserted into the email box a string that contains "secretaria"
                        else if (Input.Email.Contains("secretaria"))
                        {
                            person.Role = "Secretary";
                            await _userManager.AddToRoleAsync(user, "Secretary");
                        }
                        //incase the user inserted into the email box a string that contains "admin"
                        else if (Input.Email.Contains("admin"))
                        {
                            person.Role = "Admin";
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Please enter a valid email");
                        return Page();
                    }
                    

                    try {
                        //save the data in the database
                        await _context.AddAsync(person);

                        //consolidate the operation to save inside the database
                        await _context.SaveChangesAsync();
                        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                        if (ModelState.IsValid) {
                            // This doesn't count login failures towards account lockout
                            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                            var result1 = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
                            if (result1.Succeeded) {
                                _logger1.LogInformation("User logged in.");

                                if (Input.Email.Contains("aluno")) {
                                    return RedirectToAction("Create", "Students");
                                }
                                //incase the user inserted into the email box a string that contains "professor"
                                else if (Input.Email.Contains("professor")) {
                                    return RedirectToAction("Create", "Teachers");
                                }
                                else if (Input.Email.Contains("admin"))
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                //incase the user inserted into the email box a string that contains "juror"
                                else {
                                    return RedirectToAction("Create", "People");
                                }
                            }
                        }
                        // redirect to the Create View of the Person Model

                    }
                    catch (Exception) {
                        // apresenta uma mensagem de erro
                        ModelState.AddModelError("", "Something wrong happened...");

                        //  apaga o user que foi recentemente criado
                        await _userManager.DeleteAsync(user);

                        // devolver os dados à página
                        return Page();
                    }
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityUser CreateUser() {
            try {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore() {
            if (!_userManager.SupportsUserEmail) {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}