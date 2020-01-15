using System;
using System.Collections.Generic;
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
                //Console.WriteLine(substrings[i]);
            }
            Stack<string> opStack = new Stack<string>();
            Stack<int> valStack = new Stack<int>();
            int temp = 0;
            int tryInt = 0;
            for (int j = 0; j < substrings.Length; j++)
            {
                if (int.TryParse(substrings[j], out tryInt))
                {
                    if (opStack.Count != 0 && valStack.Count != 0)
                    {
                        if (opStack.Peek().Equals("/"))
                        {
                            int val1 = valStack.Pop();
                            //Console.WriteLine(temp);
                            opStack.Pop();
                            int val = val1 / tryInt;
                            valStack.Push(val);
                        }
                        else
                        if (opStack.Peek().Equals("*"))
                        {
                            int val1 = valStack.Pop();
                            //Console.WriteLine(temp);
                            opStack.Pop();
                            int val = val1 * tryInt;
                            valStack.Push(val);
                        }
                        else
                        {
                            int.TryParse(substrings[j], out tryInt);
                            valStack.Push(tryInt);
                        }
                    }
                    else
                    {
                        int.TryParse(substrings[j], out tryInt);
                        valStack.Push(tryInt);
                    }



                }

                if (substrings[j].Equals("+") || substrings[j].Equals("-"))
                {
                    if (opStack.Count != 0 && valStack.Count > 1)
                    {
                        if (opStack.Peek().Equals("-"))
                        {
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            opStack.Pop();
                            int val = val1 - val2;

                            valStack.Push(val);
                            
                            opStack.Push(substrings[j]);
                        }
                            else if (opStack.Peek().Equals("+"))
                        {
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            opStack.Pop();
                            int val = val1 + val2;
                            valStack.Push(val);
                            
                        }

                        

                    }

                        opStack.Push(substrings[j]);
                    
                }


                if (substrings[j].Equals("*"))
                {
                    opStack.Push("*");
                }
                if (substrings[j].Equals("/"))
                {
                    opStack.Push("/");
                }

                if (substrings[j].Equals("("))
                {
                    opStack.Push("(");
                }

                if (substrings[j].Equals(")")){
                    if (opStack.Count != 0 && valStack.Count > 1)
                    {
                        if (opStack.Peek().Equals("+"))
                        {
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val1 + val2;
                            opStack.Pop();
                            valStack.Push(val);
                            opStack.Pop();
                        }
                        else if (opStack.Peek().Equals("-"))
                        {
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val1 - val2;
                            opStack.Pop();
                            valStack.Push(val);
                            opStack.Pop();
                        }
                        else if (opStack.Peek().Equals("*")){
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val1 * val2;
                            opStack.Pop();
                            valStack.Push(val);
                            opStack.Pop();
                        }

                        else if (opStack.Peek().Equals("/")){
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val1 / val2;
                            opStack.Pop();
                            valStack.Push(val);
                            opStack.Pop();
                        }
                    }
                }
            }


            
            result = valStack.Pop();
            //Console.WriteLine(result);
            return result;
        }
    }
}
