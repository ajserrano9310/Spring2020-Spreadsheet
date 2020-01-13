using FormulaEvaluator;
using System;

namespace Test_The_Evaluator_Console_App
{
    class Program
    {
        private static Evaluator.Lookup a; //borrar

        static void Main(string[] args)
        {
            if (Evaluator.Evaluate("5+5", a) != 10) Console.WriteLine("Error");
        }
    }
}
