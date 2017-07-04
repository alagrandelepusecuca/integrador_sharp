﻿using System;
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
            FIN = StreamTokenizer.TT_EOF,
            NOMBRE = StreamTokenizer.TT_WORD
        }

        private String formula = null;
        private StreamTokenizer tokens;
        private char token;
        private double valor = 0;
        private String error = "";
        private Dictionary<string, Cuenta> cuentas = new Dictionary<string, Cuenta>();

        public Parser(string formula) {
            this.formula = formula;
        }

        internal double CalcularValor(Empresa emp, int per) {
            cuentas = emp.DiccionarioCuentasDelPeríodo(per);
            return CalcularValor();
        }

        internal double CalcularValor(Dictionary<string, Cuenta> ccs) {
            cuentas = ccs;
            return CalcularValor();
        }

        private double CalcularValor() {
            StreamReader reader = new StreamReader(formula);
            tokens = new StreamTokenizer(reader);

            foreach (Símbolos s in Enum.GetValues(typeof(Símbolos)))
                tokens.OrdinaryChar((char) s);

            GetToken();
            valor = Expr();
            if (!EsToken(Símbolos.FIN))
                GenError("error de sintaxis");
            return valor;
        }

        bool EsVálido() { return error == ""; }

        private void GetToken() {
            try {
                token = (char) tokens.NextToken();
            } catch (IOException e) {
                GenError("error de e/s " + e.ToString());
            }
        }

        // Devuelve verdadero si el token actual coincide con el recibido
        private bool EsToken(Símbolos ss) {
            return token == (char) ss;
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
                Indicador i = Indicador.Get(id);
                if (i != null) {
                    valor = i.CalcularValor(cuentas);
                } else {
                    bool cuentaEstá = cuentas.ContainsKey(id);
                    if (cuentaEstá) valor = cuentas[id].valor;
                    else GenError("error por cuenta no encontrada");
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
            return token == (char) Símbolos.MAS || token == (char) Símbolos.MENOS;
        }

        public static bool EsMult(int token) {
            return token == (char) Símbolos.MULT || token == (char) Símbolos.DIVI;
        }
    }
}