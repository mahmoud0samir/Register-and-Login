using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VendingmachineCore.Model;
using VendingmachineInfastruction.Extend;

namespace VendingmachinePresention.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userMAnager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userMAnager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IMapper mapper)
        {
            this.userMAnager = userMAnager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }




        #region Registration

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterVm model)
        {

            //var user = new ApplicationUser()
            //{
            //    UserName = model.UserName,
            //    Email = model.Email,
            //    IsAgree = model.IsAgree
            //};

            var user = mapper.Map<ApplicationUser>(model);

            var result = await userMAnager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }

        #endregion


        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            var user = await userMAnager.FindByEmailAsync(model.Email);


            if (user == null)
            {
                TempData["error"] = "user not found";
            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "account not found");
                }

            }

            return View(model);
        }

        #endregion


        #region Sign Out


        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion


        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVm model)
        {

            var user = await userMAnager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                // Generate Token
                var token = await userMAnager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);


                EventLog log = new EventLog();
                log.Source = "Hr System";
                log.WriteEntry(passwordResetLink, EventLogEntryType.Information);

            }

            return RedirectToAction("ConfirmationForgetPassword");
        }


        public IActionResult ConfirmationForgetPassword()
        {
            return View();
        }

        #endregion


        #region Reset Password

        public IActionResult ResetPassword(string Email, string Token)
        {

            if (Email == null || Token == null)
                TempData["error"] = "account not registered";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {

            var user = await userMAnager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userMAnager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmationResetPassword");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);

        }


        public IActionResult ConfirmationResetPassword()
        {
            return View();
        }


        #endregion
    }
}
