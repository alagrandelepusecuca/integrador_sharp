using System.Collections.Generic;

namespace DDS.Models {
    public class Indicador {
        static Dictionary<string, Indicador> indicadores = new Dictionary<string, Indicador>();
        internal static SortedSet<string> nombres = new SortedSet<string>();
        Parser parser;

        internal readonly string nombre;
        internal readonly string formula;

        internal static Indicador Get(string nombre) {
            return indicadores.ContainsKey(nombre) ? indicadores[nombre] : null;
        }

        internal Indicador(string nombre, string formula) {
            this.nombre = nombre;
            this.formula = formula;
            parser = new Parser(formula);
            nombres.Add(nombre);
            indicadores.Add(nombre, this);
        }

        internal double CalcularValor(Dictionary<string, Cuenta> cuentas) {
            return parser.CalcularValor(cuentas);
        }
    }
}