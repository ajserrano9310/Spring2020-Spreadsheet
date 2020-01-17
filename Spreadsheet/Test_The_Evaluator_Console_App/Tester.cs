using FormulaEvaluator;
using System;

namespace Test_The_Evaluator_Console_App
{
    class Tester
    {
        static void Main(string[] args)
        {
            //if (Evaluator.Evaluate("(2 + 3) * 5 + 2", a) != 27) Console.WriteLine("Error");
            //Console.WriteLine(Evaluator.Evaluate("(2 + 3) * 5 + 2", a));
            //Console.WriteLine(Evaluator.Evaluate("10/5",null));

            Console.WriteLine(Evaluator.Evaluate("jfg34^wuf$u+10", SimpleLookup));




        }




        public static int SimpleLookup(string v)
        {
            // Do anything here. Decide whether or not this delegate 
            // has a value for v, and return its value, or throw if it doesn't.

            if (v == "Z6")
                return 20;
            else if (v == "jfg34^wuf$u")
                return 10;
            else
                throw new ArgumentException();
        }
    }
}
