using System;
using System.Collections.Generic;
using System.Linq;

namespace Reto3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var letras = new Dictionary<string, int>()
            {
                { "uno", 1},
                { "dos", 2},
                { "tres", 3},
                { "cuatro", 4},
                { "cinco", 5},
                { "seis", 6},
                { "siete", 7},
                { "ocho", 8},
                { "nueve", 9},
                { "dies", 10},
                { "once", 11},
                { "doce", 12},
                { "veinte", 20},
                { "treinta", 30},
                { "cien", 100},
            };

            //dividir numero en base 10

            //100 132 - (132%100=32) = 100
            // 30  32 - (132%10 = 2)) = 30
            //  2   2 - (132%1 = 0)  = 2
 
            var input = 132;
            var temporal = input;

            var cadena = string.Empty;
            for (int i = input.ToString().Length; i > 0 ; i--)
            {
                int realRestante = (int)(input % Math.Pow(10, i - 1));
                int numeroDecimal = temporal - realRestante;

                //TODO: if es 11 o 12 o 13 pintar y hacer break
                cadena = cadena + " y " + letras.First(x => x.Value == numeroDecimal).Key;

                temporal = realRestante;
            }
           
            Console.WriteLine(cadena);
            Console.Read();

        }
    }
}
