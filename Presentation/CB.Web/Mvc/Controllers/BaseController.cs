using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Mvc.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}
