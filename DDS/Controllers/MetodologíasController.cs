using DDS.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DDS.Controllers {
    public class MetodologíasController : Controller {
        public ActionResult Agregar() {
            return View();
        }

        [HttpPost]
        public ActionResult Procesar(string nombre, string fórmula, string esHistórico, string esComparativo) {
            bool esTaxativo = fórmula.Contains("<") || fórmula.Contains(">") || fórmula.Contains("=");
            new Metodología(nombre, fórmula, esTaxativo, esHistórico == "on", esComparativo == "on");
            return RedirectToAction("Index", "Home");
        }

        private void AgregarTuplas(Metodología m, Empresa e, int período) {
            if (m.esHistórico) {
                foreach (int p in Empresa.períodos) {
                    double val = m.CalcularValor(e.DiccionarioCuentasDelPeríodo(p));
                    if (m.parser.EsVálido()) {
                        string valor = m.esTaxativo ? (val == 1 ? "Cumple" : "No Cumple") : val.ToString();
                        filas.Add(new Tuple<string, int, string>(e.nombre, p, valor));
                    }
                }
            } else if (período != 0) {
                double val = m.CalcularValor(e.DiccionarioCuentasDelPeríodo(período));
                if (m.parser.EsVálido()) {
                    string valor = m.esTaxativo ? (val == 1 ? "Cumple" : "No Cumple") : val.ToString();
                    filas.Add(new Tuple<string, int, string>(e.nombre, período, valor));
                }
            }
        }

        private List<Tuple<string, int, string>> filas = new List<Tuple<string, int, string>>();

        public ActionResult Visualizar(string nombreMetodología = null, string nombreEmpresa = null, int período = 0) {
            ViewBag.NombreMetodología = nombreMetodología;
            ViewBag.NombresMetodologías = Metodología.nombres;
            ViewBag.NombreEmpresa = nombreEmpresa;
            ViewBag.NombresEmpresas = Empresa.nombres;
            ViewBag.Período = período;
            ViewBag.Períodos = Empresa.períodos;

            filas = new List<Tuple<string, int, string>>();

            if (nombreMetodología != null) {
                Metodología m = Metodología.Get(nombreMetodología);
                ViewBag.EsComparativo = m.esComparativo;
                ViewBag.EsHistórico = m.esHistórico;

                if (m.esComparativo) {
                    foreach (Empresa e in Empresa.empresas.Values)
                        AgregarTuplas(m, e, período);
                } else if (nombreEmpresa != null) {
                    Empresa e = Empresa.Get(nombreEmpresa);
                    if (e != null) AgregarTuplas(m, e, período);
                }
                ViewBag.Filas = filas;
            }

            return View();
        }
    }
}