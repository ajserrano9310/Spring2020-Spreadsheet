using FormulaEvaluator;
using System;

namespace Test_The_Evaluator_Console_App
{
    class Tester
    {
        private static Evaluator.Lookup a; //borrar

        static void Main(string[] args)
        {
            if (Evaluator.Evaluate("1+2", a) != 3) Console.WriteLine("Error");
        }
    }
}
