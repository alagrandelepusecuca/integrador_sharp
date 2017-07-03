using System.Web.Mvc;

namespace DDS.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
    }
}