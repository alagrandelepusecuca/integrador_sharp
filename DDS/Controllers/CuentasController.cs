using System.Web.Mvc;
using DDS.Models;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using System;

namespace DDS.Controllers {
    public class CuentasController : Controller {
        public ActionResult Importar() {
            return View();
        }

        [HttpPost]
        public ActionResult Procesar(HttpPostedFileBase file) {
            if (file != null && file.ContentLength > 0) {
                using (TextFieldParser parser = new TextFieldParser(file.InputStream)) {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    while (!parser.EndOfData) {
                        string[] fields = parser.ReadFields();
                        string nombreEmpresa = fields[0];
                        string nombreCuenta = fields[1];
                        int período = Convert.ToInt32(fields[2]);
                        double valor = Convert.ToDouble(fields[3]);
                        Empresa e = Empresa.Get(nombreEmpresa);
                        if (e == null) e = new Empresa(nombreEmpresa);
                        Cuenta c = new Cuenta(período, nombreCuenta, valor);
                        e.AgregarCuenta(c);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Visualizar(string nombre = null, int período = 0) {
            ViewBag.Nombre = nombre;
            ViewBag.Período = período;
            ViewBag.Nombres = Empresa.nombres;
            ViewBag.Períodos = Empresa.períodos;
            if (nombre != null && período != 0)
                ViewBag.Cuentas = Empresa.Get(nombre).CuentasDelPeríodo(período);
            return View();
        }
    }
}