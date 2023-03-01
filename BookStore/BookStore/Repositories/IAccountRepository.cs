using BookList.Models;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel model);
        Task<SignInResult> PasswordSigninUserAsync(SignInUserModel model);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> UpdateUserNameAsync(UpdateUserNameModel model);
        Task<UpdateUserNameModel> GetCurrentUserNameData();
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser? user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task GeneratePasswordResetTokenAsync(ApplicationUser? user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);

		Task Logout();
    }
}