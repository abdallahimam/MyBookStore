using BookList.Models;
using BookStore.Controllers;
using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly SignInManager<ApplicationUser> _signManager = null;
        private readonly IUserService _userService = null;
        private readonly IEmailService _emailService = null;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, IUserService userService, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signManager = signManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel model)
        {
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }

        public async Task<SignInResult> PasswordSigninUserAsync(SignInUserModel model)
        {
            var result =  await _signManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
            if (result.IsNotAllowed)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser? user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendMailConfirmationEmail(user, token);
            }
        }

        public async Task GeneratePasswordResetTokenAsync(ApplicationUser? user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendPasswordResetEmail(user, token);
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(_userService.GetUserId());
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

		public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
		{
			var user = await _userManager.FindByIdAsync(model.UserId);
			return await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
		}

		public async Task<IdentityResult> UpdateUserNameAsync(UpdateUserNameModel model)
        {
            var user = await _userManager.FindByIdAsync(_userService.GetUserId());
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<UpdateUserNameModel> GetCurrentUserNameData()
        {
            var user = await _userManager.FindByIdAsync(_userService.GetUserId());
            var model = new UpdateUserNameModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return model;
        }

        public async Task Logout()
        {
            await _signManager.SignOutAsync();
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task SendMailConfirmationEmail(ApplicationUser user, string token)
        {
            var appDomain = _configuration["Application:AppDomain"];
            var emailConfirmation = _configuration["Application:EmailConfirmation"];

            var userEmailOptions = new UserEmailOptions()
            {
                To = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{USERNAME}}", user.FirstName),
                    new KeyValuePair<string, string>("{{LINK}}", string.Format(appDomain + emailConfirmation, user.Id, token))
                }
            };

            await _emailService.SendMailConfirmationEmail(userEmailOptions);
        }

        private async Task SendPasswordResetEmail(ApplicationUser user, string token)
        {
            var appDomain = _configuration["Application:AppDomain"];
            var emailConfirmation = _configuration["Application:ForgotPassword"];

            var userEmailOptions = new UserEmailOptions()
            {
                To = new List<string>() { user.Email },
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{USERNAME}}", user.FirstName),
                    new KeyValuePair<string, string>("{{LINK}}", string.Format(appDomain + emailConfirmation, user.Id, token))
                }
            };

            await _emailService.SendPasswordResetEmail(userEmailOptions);
        }
    }
}
