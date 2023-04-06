using CB.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Controllers
{
	public class HomeController : BasePublicController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}