using System.Collections.Generic;

namespace DDS.Models {
    public class Metodología {
        static Dictionary<string, Metodología> metodologías = new Dictionary<string, Metodología>();
        internal static SortedSet<string> nombres = new SortedSet<string>();
        internal Parser parser;

        internal readonly string nombre;
        internal readonly string fórmula;
        internal readonly bool esTaxativo;
        internal readonly bool esHistórico;
        internal readonly bool esComparativo;

        internal static Metodología Get(string nombre) {
            return metodologías.ContainsKey(nombre) ? metodologías[nombre] : null;
        }

        internal Metodología(string nombre, string fórmula, bool esTaxativo, bool esHistórico, bool esComparativo) {
            this.nombre = nombre;
            this.fórmula = fórmula;
            this.esTaxativo = esTaxativo;
            this.esHistórico = esHistórico;
            this.esComparativo = esComparativo;
            parser = new Parser(fórmula);
            nombres.Add(nombre);
            metodologías.Add(nombre, this);
        }

        internal double CalcularValor(Dictionary<string, Cuenta> cuentas) {
            return parser.CalcularValor(cuentas);
        }
    }
}