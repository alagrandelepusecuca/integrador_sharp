using DDS.Models;
using System.Web.Mvc;

namespace DDS.Controllers {
    public class MetodologíasController : Controller {
        public ActionResult Agregar() {
            return View();
        }

        [HttpPost]
        public ActionResult Procesar(string nombre, string formula) {
            new Metodología(nombre, formula);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Visualizar(string nombreMetodología = null, string nombreEmpresa = null, int período = 0) {
            ViewBag.NombreMetodología = nombreMetodología;
            ViewBag.NombresMetodologías = Metodología.nombres;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.NombresEmpresas = Empresa.nombres;
            ViewBag.Período = período;
            ViewBag.Períodos = Empresa.períodos;
            if (nombreMetodología != null && nombreEmpresa != null && período != 0)
                ViewBag.Valor = Metodología.Get(nombreMetodología).CalcularValor(Empresa.Get(nombreEmpresa).DiccionarioCuentasDelPeríodo(período));
            return View();
        }
    }
}