using System.Web.Mvc;

namespace FatAntelope.WebTester.Controllers
{
	[RequireHttps]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            ViewBag.DefaultSourceConfig = Resources.DefaultSourceConfig;
            ViewBag.DefaultTargetConfig = Resources.DefaultTargetConfig;
			return View();
		}
	}
}
