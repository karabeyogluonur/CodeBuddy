using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Controllers
{
    public class DashboardController : BasePublicController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
