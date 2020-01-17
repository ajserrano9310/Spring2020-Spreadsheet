using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace FormulaEvaluator
{
    /// <summary>
    /// Library that evaluates formula expressions using infix expression evaluation
    /// </summary>
    public static class Evaluator
    {
        /// <summary>
        /// Looks for the value of the given variable
        /// </summary>
        /// <param name="variable_name">The name of the variable to get the value</param>
        /// <returns>The integer value of the variable or an exception if it does not exist</returns>
        public delegate int Lookup(String variable_name);
        /// <summary>
        /// This method calculates the value of expressions using infix expression evaluation
        /// </summary>
        /// <param name="expression">The expression that the user wants the value of</param>
        /// <param name="variableEvaluator">Function that acts like a dictionary to search for the value of a variable</param>
        /// <returns></returns>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            // Check if the string is empty or null and if it is throw and exception
            if (isStringNullOrEmpty(expression))
            {
                throw new ArgumentException();
            }
            // Create variables to use later
            int result;
            Stack<string> opStack = new Stack<string>();
            Stack<int> valStack = new Stack<int>();
            int tryInt;
            String actualString;
            // Convert expression into tokens
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            // Go trough each of the elements (tokens) from the expression
            for (int i = 0; i < substrings.Length; i++)
            {
                // Try to erase blank spaces
                substrings[i] = substrings[i].Trim();
                actualString = substrings[i];
                // If it is a blank space we dont process it
                if (!isStringNullOrEmpty(actualString))
                {
                    // Check if its an integer and assign the value to tryInt
                    if (int.TryParse(actualString, out tryInt))
                    {
                        // Case where we need to divide
                        if (hasOnTop(opStack, "/"))
                        {
                            // If the stack of values has 0 elements we dont have the correct number of elements to perform a division
                            if (valStack.Count == 0)
                            {
                                throw new ArgumentException();
                            }
                            // We cant divide by 0
                            if (tryInt == 0)
                            {
                                throw new ArgumentException();
                            }
                            // Perform division
                            int val1 = valStack.Pop();
                            opStack.Pop();
                            int val = val1 / tryInt;
                            valStack.Push(val);
                        }
                        // Case where we need to multiply
                        else if (hasOnTop(opStack, "*"))
                        {
                            // If the stack of values has 0 elements we dont have the correct number of elements to perform a multiplication
                            if (valStack.Count == 0)
                            {
                                throw new ArgumentException();
                            }
                            // Perform multiplication
                            int val1 = valStack.Pop();
                            opStack.Pop();
                            int val = val1 * tryInt;
                            valStack.Push(val);
                        }
                        else
                        {
                            valStack.Push(tryInt);
                        }
                    }
                    // Case of addition and substraction
                    else if (actualString.Equals("+") || actualString.Equals("-"))
                    {
                        // Case of substraction
                        if (hasOnTop(opStack, "-"))
                        {
                            // We need 2 values for substraction
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException();
                            }
                            // Perform substraction
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            opStack.Pop();
                            int val = val2 - val1;
                            valStack.Push(val);
                        }
                        // Case of addition
                        else if (hasOnTop(opStack, "+"))
                        {
                            // We need 2 values for addition
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException();
                            }
                            // Perform addition
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            opStack.Pop();
                            int val = val2 + val1;
                            valStack.Push(val);
                        }
                        opStack.Push(actualString);
                    }
                    // Case where the token is a *
                    else if (actualString.Equals("*"))
                    {
                        opStack.Push("*");
                    }
                    // Case where the token is a /
                    else if (actualString.Equals("/"))
                    {
                        opStack.Push("/");
                    }
                    // Case where the token is a (
                    else if (actualString.Equals("("))
                    {
                        opStack.Push("(");
                    }
                    // Case where the token is a )
                    else if (actualString.Equals(")"))
                    {
                        // Case of addition with parenthesis
                        if (hasOnTop(opStack, "+"))
                        {
                            // We need 2 values for addition
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException();
                            }
                            // Perform addition
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val2 + val1;
                            opStack.Pop();
                            valStack.Push(val);
                        }
                        // Case of substraction with parenthesis
                        else if (hasOnTop(opStack, "-"))
                        {
                            // We need 2 values for substraction
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException();
                            }
                            // Perform substraction
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val2 - val1;
                            opStack.Pop();
                            valStack.Push(val);
                        }
                        // Case of multiplication with parenthesis
                        else if (hasOnTop(opStack, "*"))
                        {
                            // We need 2 values for multiplication
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException();
                            }
                            // Perform multiplication
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            int val = val2 * val1;
                            opStack.Pop();
                            valStack.Push(val);
                        }
                        // Case of division with parenthesis
                        else if (hasOnTop(opStack, "/"))
                        {
                            // We need 2 values for division
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException();
                            }
                            // Get the 2 values
                            int val1 = valStack.Pop();
                            int val2 = valStack.Pop();
                            // Check if the divisor is going to be 0 because we cant do that
                            if (val2 == 0)
                            {
                                throw new ArgumentException();
                            }
                            // Perform division
                            int val = val1 / val2;
                            opStack.Pop();
                            valStack.Push(val);
                        }
                        // Take out the ( sign
                        opStack.Pop();
                    }
                    // Case where its a variable
                    else
                    {
                        // Check if the variable exists if not throw an exception
                        try
                        {
                            tryInt = variableEvaluator(actualString);
                        }
                        catch (ArgumentException)
                        {
                            throw new ArgumentException();
                        }
                        // Case where we have to divide with the variable
                        if (hasOnTop(opStack, "/"))
                        {
                            // We need another value to perform the division
                            if (valStack.Count == 0)
                            {
                                throw new ArgumentException();
                            }
                            // Check if we are going to divide by 0
                            if (tryInt == 0)
                            {
                                throw new ArgumentException();
                            }
                            // Perform division
                            int val1 = valStack.Pop();
                            opStack.Pop();
                            int val = val1 / tryInt;
                            valStack.Push(val);
                        }
                        else
                            // Case where we have to multiply with the variable
                            if (hasOnTop(opStack, "*"))
                        {
                            // We need another value to multiply
                            if (valStack.Count == 0)
                            {
                                throw new ArgumentException();
                            }
                            // Perform multiplication
                            int val1 = valStack.Pop();
                            opStack.Pop();
                            int val = val1 * tryInt;
                            valStack.Push(val);
                        }
                        else
                        {
                            valStack.Push(tryInt);
                        }
                    }
                }
            }
            // Now when the last token has been processed
            // If the operator stack is empty
            if (opStack.Count == 0)
            {
                if (valStack.Count != 1)
                {
                    throw new ArgumentException();
                }
                result = valStack.Pop();
            }
            else
            // If the operator stack is not empty
            {
                // We need 1 operator and two values to obtain the last value
                if (opStack.Count != 1 || valStack.Count != 2)
                {
                    throw new ArgumentException();
                }
                int val1 = valStack.Pop();
                int val2 = valStack.Pop();
                // Check for the sign to see if its an addition or a substraction
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
        /// <summary>
        /// Check if the element you are looking for is at the top of the stack
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="stack">The stack to check</param>
        /// <param name="value">The value to search</param>
        /// <returns>true if the value is on top and false otherwise</returns>
        public static Boolean hasOnTop<T>(this Stack<T> stack, T value)
        {
            // If the size of the stack is 0 we know is not there and we cant peek because is going to throw an error
            if (stack.Count == 0)
            {
                return false;
            }
            else
            {
                // Now we can peek and see if the value matches the value on the top of the stack or not
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
        /// <summary>
        /// Checks if the string is null, empty or a white space
        /// </summary>
        /// <param name="exp">The string to check</param>
        /// <returns>true if the string is null, empty or white space and false otherwise</returns>
        public static Boolean isStringNullOrEmpty(String exp)
        {
            // We cant check null with .Equals because throws an error so we check it first
            if (exp == null)
            {
                return true;
            }
            // Now we are safe so we can check with .Equals
            if (exp.Equals(" ") || exp.Equals(""))
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