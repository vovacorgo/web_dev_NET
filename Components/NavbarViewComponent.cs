using Microsoft.AspNetCore.Mvc;

namespace SupportCenter.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
