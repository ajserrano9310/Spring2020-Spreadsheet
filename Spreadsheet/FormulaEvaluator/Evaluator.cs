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
            if (expression.Equals(null))
            {
                throw new ArgumentException();
            }
            if (expression.Equals(" "))
            {
                throw new ArgumentException();
            }
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
                if (substrings[j].Equals(" "))
                {

                }
                else
                {


                    if (int.TryParse(substrings[j], out tryInt))
                    {
                        if (opStack.Count > 0)
                        {
                            if (opStack.Peek().Equals("/"))
                            {
                                if (valStack.Count == 0)
                                {
                                    throw new ArgumentException();
                                }
                                if (tryInt == 0)
                                {
                                    throw new ArgumentException();
                                }
                                int val1 = valStack.Pop();
                                //Console.WriteLine(temp);
                                opStack.Pop();
                                int val = val1 / tryInt;
                                valStack.Push(val);
                            }
                            else
                            if (opStack.Peek().Equals("*"))
                            {
                                if (valStack.Count == 0)
                                {
                                    throw new ArgumentException();
                                }
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





                    else if (substrings[j].Equals("+") || substrings[j].Equals("-"))
                    {
                        if (opStack.Count != 0)
                        {
                            if (opStack.Peek().Equals("-"))
                            {
                                if (valStack.Count < 2)
                                {
                                    throw new ArgumentException();
                                }
                                int val1 = valStack.Pop();
                                int val2 = valStack.Pop();
                                opStack.Pop();
                                int val = val2 - val1;

                                valStack.Push(val);

                                opStack.Push(substrings[j]);
                            }
                            else if (opStack.Peek().Equals("+"))
                            {
                                if (valStack.Count < 2)
                                {
                                    throw new ArgumentException();
                                }
                                int val1 = valStack.Pop();
                                int val2 = valStack.Pop();
                                opStack.Pop();
                                int val = val2 + val1;
                                valStack.Push(val);

                            }



                        }

                        opStack.Push(substrings[j]);

                    }


                    else if (substrings[j].Equals("*"))
                    {
                        opStack.Push("*");
                    }
                    else if (substrings[j].Equals("/"))
                    {
                        opStack.Push("/");
                    }

                    else if (substrings[j].Equals("("))
                    {
                        opStack.Push("(");
                    }

                    else if (substrings[j].Equals(")"))
                    {
                        if (opStack.Count != 0 && valStack.Count > 1)
                        {
                            if (opStack.Peek().Equals("+"))
                            {
                                int val1 = valStack.Pop();
                                int val2 = valStack.Pop();
                                int val = val2 + val1;
                                opStack.Pop();
                                valStack.Push(val);
                                opStack.Pop();
                            }
                            else if (opStack.Peek().Equals("-"))
                            {
                                int val1 = valStack.Pop();
                                int val2 = valStack.Pop();
                                int val = val2 - val1;
                                opStack.Pop();
                                valStack.Push(val);
                                opStack.Pop();
                            }
                            else if (opStack.Peek().Equals("*"))
                            {
                                int val1 = valStack.Pop();
                                int val2 = valStack.Pop();
                                int val = val2 * val1;
                                opStack.Pop();
                                valStack.Push(val);
                                opStack.Pop();
                            }

                            else if (opStack.Peek().Equals("/"))
                            {
                                int val1 = valStack.Pop();
                                int val2 = valStack.Pop();
                                int val = val2 / val1;
                                opStack.Pop();
                                valStack.Push(val);
                                opStack.Pop();
                            }
                        }
                    }
                }
            }

            if (opStack.Count == 0)
            {
                result = valStack.Pop();
            }
            else
            {
                int val1 = valStack.Pop();
                int val2 = valStack.Pop();
                String sign = opStack.Pop();

                if (sign.Equals("-"))
                {
                    result = val2 - val1;
                }
                else
                {
                    result = val2 + val1;
                }
            }
            return result;
        }
    }
}
