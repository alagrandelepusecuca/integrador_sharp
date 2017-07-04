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

        public ActionResult Visualizar(string nombreIndicador, string nombreEmpresa = null, int período = 0) {
            ViewBag.NombreIndicador = nombreIndicador;
            ViewBag.Nombre = nombreEmpresa;
            ViewBag.Período = período;
            if (nombreEmpresa != null && período != 0)
                ViewBag.Valor = Indicador.Get(nombreIndicador).CalcularValor(Empresa.Get(nombreEmpresa).DiccionarioCuentasDelPeríodo(período));
            return View();
        }
    }
}