using System;
using System.Collections.Generic;

namespace DDS.Models {
    public class Cuenta {
        internal static SortedSet<string> nombres = new SortedSet<string>();

        public readonly int período;
        public readonly String nombre;
        public readonly double valor;

        internal Cuenta(int período, String nombre, double valor) {
            this.período = período;
            this.nombre = nombre;
            this.valor = valor;
            nombres.Add(nombre);
        }
    }
}