using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookStore.Controllers
{
	public class HomeController : Controller
	{
        public readonly ContactInfoConfig _contactInfoConfig1;
        public readonly ContactInfoConfig _contactInfoConfig2;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IOptionsMonitor<ContactInfoConfig> contactInfoConfig, IUserService userService, IEmailService emailService)
        {
            _contactInfoConfig1 = contactInfoConfig.Get("ContactInfoConfig1");
            _contactInfoConfig2 = contactInfoConfig.Get("ContactInfoConfig2");
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<ViewResult> Index()
		{
            ViewBag.UserId = "";
            if (_userService.IsLoggedIn())
            {
                ViewBag.UserId = _userService.GetUserId();

            }
            //var userEmailOptions = new UserEmailOptions()
            //{
            //    To = new List<string>() { "email@email.com" },
            //    Placeholders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{USERNAME}}", "Abdullah")
            //    }
            //};

            //await _emailService.SendEmailWelcome(userEmailOptions);
            return View();
		}

        public ViewResult Contact()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }
    }
}
