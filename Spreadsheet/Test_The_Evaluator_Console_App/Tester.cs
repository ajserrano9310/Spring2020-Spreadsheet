using FormulaEvaluator;
using System;

namespace Test_The_Evaluator_Console_App
{
    class Tester
    {
        private static Evaluator.Lookup a; //borrar

        static void Main(string[] args)
        {
            //if (Evaluator.Evaluate("(2 + 3) * 5 + 2", a) != 27) Console.WriteLine("Error");
            //Console.WriteLine(Evaluator.Evaluate("(2 + 3) * 5 + 2", a));
            Console.WriteLine(Evaluator.Evaluate("10/5",null));

        }
    }
}
