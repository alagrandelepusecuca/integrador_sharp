using DDS.Models;
using System.Web.Mvc;

namespace DDS.Controllers {
    public class IndicadoresController : Controller {
        public ActionResult Agregar() {
            return View();
        }

        [HttpPost]
        public ActionResult Procesar(string nombre, string formula) {
            new Indicador(nombre, formula);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Visualizar() {
            return View();
        }
    }
}