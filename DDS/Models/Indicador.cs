using System.Collections.Generic;

namespace DDS.Models {
    public class Indicador {
        static Dictionary<string, Indicador> indicadores = new Dictionary<string, Indicador>();
        internal static SortedSet<string> nombres = new SortedSet<string>();

        internal string nombre;
        internal string formula;

        static Indicador Get(string nombre) {
            return indicadores.ContainsKey(nombre) ? indicadores[nombre] : null;
        }

        internal Indicador(string nombre, string formula) {
            this.nombre = nombre;
            this.formula = formula;
            nombres.Add(nombre);
            indicadores.Add(formula, this);
        }

        void Calcular() {
            
        }
    }
}