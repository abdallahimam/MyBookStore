using BookList.Models;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository = null;
        private readonly IEmailService _emailService = null;
        public AccountController(IAccountRepository accountRepository, IEmailService emailService) 
        { 
            _accountRepository = accountRepository;
            _emailService = emailService;
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(model);
                if (!result.Succeeded) {
                    ModelState.Clear();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    ViewBag.isSuccess = false;
                    return View(model);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new {email = model.Email});
            }
            ViewBag.isSuccess = false;
            return View(model);
        }

        [Route("signin")]
        public IActionResult Signin(bool IsPasswordReset = false)
        {
            ViewBag.IsPasswordReset = IsPasswordReset;
			return View();
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> Signin(SignInUserModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSigninUserAsync(model);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
					ModelState.AddModelError("", "Account was locked out! try agai after 3 minutes");
				}
                else if (result.IsNotAllowed)
                {
                    return RedirectToAction("ConfirmEmail", new { email = model.Email});
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credintials!");
                }
            }
            return View(model);
        }

        [Route("change-password")]
        [Authorize]
        public IActionResult ChangePassword(bool IsSuccess = false)
        {
            ViewBag.IsSuccess = IsSuccess;
            return View();
        }

        [Route("change-password")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return RedirectToAction("ChangePassword", "Account", new { IsSuccess = true} );
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [Route("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(bool IsSuccess = false)
        {
            var model = new ForgotPasswordModel { EmailSent = IsSuccess };
            return View(model);
        }

        [Route("forgot-password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GeneratePasswordResetTokenAsync(user);
                    ModelState.Clear();
                    model.EmailSent = true;
                }
            }
            return RedirectToAction("ForgotPassword", new { IsSuccess = true });
        }

		[Route("reset-password")]
		[AllowAnonymous]
		public IActionResult ResetPassword(string uid = null, string token = null , bool IsPasswordReset = false)
		{
            var model = new ResetPasswordModel { UserId = uid, Token = token, IsSuccess = IsPasswordReset };
			return View(model);
		}

        [Route("reset-password")]
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
		{
			if (ModelState.IsValid)
			{
                model.Token = model.Token.Replace(' ', '+');
				var result = await _accountRepository.ResetPasswordAsync(model);
				if (result.Succeeded)
				{
					ModelState.Clear();
					model.IsSuccess = true;
                    return RedirectToAction("ResetPassword", new { IsPasswordReset = true });
				}

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
			}
			return View(model);
		}

		[Route("update-user-name")]
        [Authorize]
        public async Task<IActionResult> UpdateUserName(bool IsSuccess = false)
        {
            var model = await _accountRepository.GetCurrentUserNameData();
            ViewBag.IsSuccess = IsSuccess;
            return View(model);
        }

        [Route("update-user-name")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUserName(UpdateUserNameModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.UpdateUserNameAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return RedirectToAction("UpdateUserName", "Account", new { IsSuccess = true });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            var model = new EmailConfirmationModel
            {
                Email = email,
                EmailSent = true
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                var result = await _accountRepository.ConfirmEmailAsync(uid, token.Replace(" ", "+"));
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmationModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong!");
            }
            return View(model);
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
