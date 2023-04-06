using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
