using Microsoft.AspNetCore.Mvc;

namespace RealNest.Web.Controllers
{
    public class AdminController:Controller
    {
        public IActionResult MainAdmin()
        {
            return View();
        }
    }
}
