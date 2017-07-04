﻿using System.Collections.Generic;

namespace DDS.Models {
    public class Metodología {
        static Dictionary<string, Metodología> metodologías = new Dictionary<string, Metodología>();
        internal static SortedSet<string> nombres = new SortedSet<string>();
        Parser parser;

        internal string nombre;
        internal string formula;

        internal static Metodología Get(string nombre) {
            return metodologías.ContainsKey(nombre) ? metodologías[nombre] : null;
        }

        internal Metodología(string nombre, string formula) {
            this.nombre = nombre;
            this.formula = formula;
            parser = new Parser(formula);
            nombres.Add(nombre);
            metodologías.Add(formula, this);
        }

        internal double CalcularValor(Dictionary<string, Cuenta> cuentas) {
            return parser.CalcularValor(cuentas);
        }

        internal double CalcularValor(Empresa empresa, int período) {
            return parser.CalcularValor(empresa, período);
        }
    }
}