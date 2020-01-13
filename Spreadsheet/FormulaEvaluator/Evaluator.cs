using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    public static class Evaluator
    {
        public delegate int Lookup(String variable_name);

        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            int result = 0;
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            for(int i = 0; i < substrings.Length; i++)
            {
                substrings[i]=substrings[i].Trim();
                Console.WriteLine(substrings[i]);
            }

            Stack opStack = new Stack();
            Stack valStack = new Stack();

            for (int j = 0; j < substrings.Length; j++)
            {

            }
            return result;
        }
    }
}
