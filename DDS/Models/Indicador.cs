using System.Collections.Generic;

namespace DDS.Models {
    public class Indicador {
        static Dictionary<string, Indicador> indicadores = new Dictionary<string, Indicador>();
        internal static SortedSet<string> nombres = new SortedSet<string>();
        internal Parser parser;

        internal readonly string nombre;
        internal readonly string fórmula;

        internal static Indicador Get(string nombre) {
            return indicadores.ContainsKey(nombre) ? indicadores[nombre] : null;
        }

        internal Indicador(string nombre, string fórmula) {
            this.nombre = nombre;
            this.fórmula = fórmula;
            parser = new Parser(fórmula);
            nombres.Add(nombre);
            indicadores.Add(nombre, this);
        }

        internal double CalcularValor(Dictionary<string, Cuenta> cuentas) {
            return parser.CalcularValor(cuentas);
        }
    }
}