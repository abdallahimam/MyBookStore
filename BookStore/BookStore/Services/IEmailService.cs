using BookStore.Models;

namespace BookStore.Services
{
    public interface IEmailService
    {
        Task SendEmailWelcome(UserEmailOptions userEmailOptions);
        Task SendMailConfirmationEmail(UserEmailOptions userEmailOptions);
        Task SendPasswordResetEmail(UserEmailOptions userEmailOptions);
    }
}