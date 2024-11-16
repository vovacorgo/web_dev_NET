using Microsoft.AspNetCore.Mvc;
using SupportCenter.Models;
using SupportCenter.Validators;

namespace SupportCenter.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SupportRequest model)
        {
            // Validate the input model
            var validator = new SupportRequestValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                // Add validation errors to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model); 
            }


            return RedirectToAction("Index","Home");
        }
    }
}
