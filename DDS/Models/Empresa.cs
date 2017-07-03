using System;
using System.Collections.Generic;

namespace DDS.Models {
    public class Empresa {
        static Dictionary<string, Empresa> empresas = new Dictionary<string, Empresa>();
        internal static SortedSet<int> períodos = new SortedSet<int>();
        internal static SortedSet<string> nombres = new SortedSet<string>();

        internal readonly String nombre;
        List<Cuenta> cuentas = new List<Cuenta>();

        internal Empresa(String nombre) {
            this.nombre = nombre;
            nombres.Add(nombre);
            empresas.Add(nombre, this);
        }

        internal void AgregarCuenta(Cuenta c) {
            períodos.Add(c.período);
            cuentas.Add(c);
        }

        internal static Empresa Get(string nombre) {
            return empresas.ContainsKey(nombre) ? empresas[nombre] : null;
        }

        internal List<Cuenta> CuentasDelPeríodo(int period) {
            List<Cuenta> listaCuentas = new List<Cuenta>();
            foreach (Cuenta c in cuentas)
                if (c.período == period)
                    listaCuentas.Add(c);
            return listaCuentas;
        }

        internal Cuenta Cuenta(string nombre, int period) {
            foreach (Cuenta c in cuentas)
                if (c.nombre == nombre && c.período == period)
                    return c;
            return null;
        }
    }
}