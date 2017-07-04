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

        public ActionResult Visualizar(string nombreIndicador = null, string nombreEmpresa = null, int período = 0) {
            ViewBag.NombreIndicador = nombreIndicador;
            ViewBag.NombresIndicadores = Indicador.nombres;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.NombresEmpresas = Empresa.nombres;
            ViewBag.Período = período;
            ViewBag.Períodos = Empresa.períodos;

            if (nombreIndicador != null && nombreEmpresa != null && período != 0) {
                Indicador i = Indicador.Get(nombreIndicador);
                Empresa e = Empresa.Get(nombreEmpresa);
                if (i != null && e != null) {
                    double valor = i.CalcularValor(e.DiccionarioCuentasDelPeríodo(período));
                    if (i.parser.EsVálido()) ViewBag.Valor = valor;
                }
            }
            return View();
        }
    }
}