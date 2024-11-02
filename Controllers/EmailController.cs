using Microsoft.AspNetCore.Mvc;
using SupportCenter.Models;
using SupportCenter.Services;


namespace SupportCenter.Controllers
{
	public class EmailController : Controller
	{
		private readonly IEmailSender _emailSender;

		public EmailController(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		[HttpPost]
		public IActionResult SendEmail(EmailModel model)
		{
			if (ModelState.IsValid)
			{
				_emailSender.SendEmailAsync(model.Email, model.Subject, model.Message).Wait();
				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}
	}
}
