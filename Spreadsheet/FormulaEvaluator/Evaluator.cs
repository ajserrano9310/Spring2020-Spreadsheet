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
            if (isExpressionNullOrEmpty(expression))
            {
                throw new ArgumentException();
            }
            int result;
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            for(int i = 0; i < substrings.Length; i++)
            {
                substrings[i]=substrings[i].Trim();
            }
            Stack<string> opStack = new Stack<string>();
            Stack<int> valStack = new Stack<int>();
            int tryInt;
            String actualString;
            for (int j = 0; j < substrings.Length; j++)
            {
                actualString = substrings[j];
                if (!isExpressionNullOrEmpty(actualString))
                {
                    if (int.TryParse(actualString, out tryInt))
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
                                opStack.Pop();
                                int val = val1 * tryInt;
                                valStack.Push(val);
                            }
                            else
                            {
                                int.TryParse(actualString, out tryInt);
                                valStack.Push(tryInt);
                            }
                        }
                        else
                        {
                            int.TryParse(actualString, out tryInt);
                            valStack.Push(tryInt);
                        }
                    }
                    else if (actualString.Equals("+") || actualString.Equals("-"))
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
                                opStack.Push(actualString);
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
                        opStack.Push(actualString);
                    }
                    else if (actualString.Equals("*"))
                    {
                        opStack.Push("*");
                    }
                    else if (actualString.Equals("/"))
                    {
                        opStack.Push("/");
                    }

                    else if (actualString.Equals("("))
                    {
                        opStack.Push("(");
                    }
                    else if (actualString.Equals(")"))
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
                    else
                    {
                        int val123 = variableEvaluator(actualString);

                        if (opStack.Count > 0)
                        {
                            if (opStack.Peek().Equals("/"))
                            {
                                if (valStack.Count == 0)
                                {
                                    throw new ArgumentException();
                                }
                                if (val123 == 0)
                                {
                                    throw new ArgumentException();
                                }
                                int val1 = valStack.Pop();
                                opStack.Pop();
                                int val = val1 / val123;
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
                                opStack.Pop();
                                int val = val1 * val123;
                                valStack.Push(val);
                            }
                            else
                            {
                                valStack.Push(val123);
                            }
                        }
                        else
                        {
                            valStack.Push(val123);
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
        public static Boolean hasOnTop<T>(this Stack<T> stack, T value)
        {
            if (stack.Count == 0)
            {
                return false;
            }
            else
            {
                if (stack.Peek().Equals(value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static Boolean isExpressionNullOrEmpty(String exp)
        {
            if(exp.Equals(" "))
            {
                return true;
            }else if (exp.Equals(null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}