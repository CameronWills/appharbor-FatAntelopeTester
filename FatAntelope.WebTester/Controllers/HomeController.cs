using System;
using System.Reflection;
using System.Web.Mvc;

namespace FatAntelope.WebTester.Controllers
{

	[RequireHttps]
	public class HomeController : Controller
	{
        private static Lazy<string> version = new Lazy<string>(() =>
        {
            try
            {
                return "v" + typeof(XTree).Assembly.GetName().Version.ToString(3);
            }
            catch (Exception)
            { }

            return string.Empty;
        });

        public ActionResult Index()
		{
            ViewBag.DefaultSourceConfig = Resources.DefaultSourceConfig;
            ViewBag.DefaultTargetConfig = Resources.DefaultTargetConfig;
            ViewBag.Version = version.Value;
			return View();
		}
	}
}
