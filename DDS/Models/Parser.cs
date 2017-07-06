using System;
using System.Collections.Generic;
using System.IO;

namespace DDS.Models {
    public class Parser {
        enum Símbolos {
            MAS = '+',
            MENOS = '-',
            MULT = '*',
            DIVI = '/',
            ABRE = '(',
            CIERRA = ')',
            MENOR = '<',
            MAYOR = '>',
            IGUAL = '=',
            FIN = StreamTokenizer.TT_EOF,
            NOMBRE = StreamTokenizer.TT_WORD
        }

        private string formula = null;
        private StreamTokenizer tokens;
        private int token;
        private double valor = 0;
        private string error = null;
        private Dictionary<string, Cuenta> cuentas = new Dictionary<string, Cuenta>();

        internal Parser(string formula) {
            this.formula = formula;
        }

        internal double CalcularValor(Dictionary<string, Cuenta> ccs) {
            cuentas = ccs;
            return CalcularValor();
        }

        private Stream GenerateStreamFromString(string s) {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private double CalcularValor() {
            valor = 0;
            error = null;
            StreamReader sr = new StreamReader(GenerateStreamFromString(formula));
            tokens = new StreamTokenizer(sr);

            foreach (Símbolos s in Enum.GetValues(typeof(Símbolos)))
                tokens.OrdinaryChar((int) s);

            GetToken();
            valor = Expr();
            if (!EsToken(Símbolos.FIN))
                GenError("error de sintaxis");
            return valor;
        }

        internal bool EsVálido() { return error == null; }

        private void GetToken() {
            try {
                token = tokens.NextToken();
            } catch (IOException e) {
                GenError("error de e/s " + e.ToString());
            }
        }

        // Devuelve verdadero si el token actual coincide con el recibido
        private bool EsToken(Símbolos ss) {
            return (Símbolos) token == ss;
        }

        // expr = [addop] term {(addop) term} FIN
        private double Expr() {
            int sign = 1;
            Aceptar(Símbolos.MAS);
            if (Aceptar(Símbolos.MENOS)) sign = -1;
            double valor = sign * Term();
            while (EsAdic(token)) {
                if (Aceptar(Símbolos.MAS)) valor += Term();
                if (Aceptar(Símbolos.MENOS)) valor -= Term();
            }
            if (Aceptar(Símbolos.MENOR)) valor = valor < Term() ? 1 : 0;
            if (Aceptar(Símbolos.MAYOR)) valor = valor > Term() ? 1 : 0;
            if (Aceptar(Símbolos.IGUAL)) valor = valor == Term() ? 1 : 0;
            return valor;
        }

        // term = factor {(multOp) factor} FIN
        private double Term() {
            double valor = Factor();
            while (EsMult(token)) {
                if (Aceptar(Símbolos.MULT)) valor *= Factor();
                if (Aceptar(Símbolos.DIVI)) valor /= Factor();
            }
            return valor;
        }

        // factor = NOMBRE | "(" expr ")" FIN
        private double Factor() {
            double valor = 0;
            if (EsToken(Símbolos.NOMBRE)) {
                String id = tokens.StringValue;
                Metodología m = Metodología.Get(id);
                if (m != null) {
                    valor = m.CalcularValor(cuentas);
                } else {
                    Indicador i = Indicador.Get(id);
                    if (i != null) {
                        valor = i.CalcularValor(cuentas);
                    } else {
                        bool cuentaEstá = cuentas.ContainsKey(id);
                        if (cuentaEstá) valor = cuentas[id].valor;
                        else GenError("error por nombre no encontrado");
                    }
                }
                GetToken();
            } else if (Aceptar(Símbolos.ABRE)) {
                valor = Expr();
                Esperar(Símbolos.CIERRA);
            } else {
                GenError("error en parseo de factores");
                GetToken();
            }
            return valor;
        }


        // Requiere un símbolo particular; genera un error si no llega
        private void Esperar(Símbolos ss) {
            if (Aceptar(ss)) return;
            GenError("missing " + (char) ss);
        }

        // Avanza si el token actual coincide con el símbolo recibido
        private bool Aceptar(Símbolos ss) {
            if (EsToken(ss)) {
                GetToken();
                return true;
            }
            return false;
        }

        private void GenError(String ss) {
            if (error == null)
                error = ss + " at " + tokens.ToString();
        }

        public static bool EsAdic(int token) {
            return (Símbolos) token == Símbolos.MAS || (Símbolos) token == Símbolos.MENOS;
        }

        public static bool EsMult(int token) {
            return (Símbolos) token == Símbolos.MULT || (Símbolos) token == Símbolos.DIVI;
        }
    }
}