﻿// Author:     Alejandro Rubio
// Partner:    None
// Date:       1/31/2020
// Course:     CS 3500, University of Utah, School of Computing
// Assignment: Assignment 3 - Formula
// Copyright:  CS 3500 and Alejandro Rubio - This work may not be copied for use in Academic Coursework.
// This file is part of a Library that tests the Formula
// I pledge that I did this work myself
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        // Create variables to use later
        private string[] tokens;
        private List<string> variables;
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            // Check if the formula is empty or null
            if (isStringNullOrEmpty(formula))
            {
                throw new FormulaFormatException("Invalid formula expression");
            }
            // Get the tokens from the formula
            tokens = GetTokens(formula).ToArray();
            // List for variables
            variables = new List<string>();
            // Counters for parenthesis
            int p1Counter = 0;
            int p2Counter = 0;
            // Go trough all the tokens
            for (int i = 0; i < tokens.Length; i++)
            {
                // Try to erase white spaces
                tokens[i] = tokens[i].Trim();
                // If the token its a variable we try to normalize it and check if its valid or not
                if (isVariable(i))
                {
                    tokens[i] = normalize(tokens[i]);
                    if (!isValid(tokens[i]))

                    {
                        throw new FormulaFormatException("Invalid formula expression");
                    }
                    // Finally we add it to our variables list if its not there
                    if (!variables.Contains(tokens[i]))
                    {
                        variables.Add(tokens[i]);
                    }
                }
                // Case where the token is a (
                if (tokens[i].Equals("("))
                {
                    // Increase the parenthesis counter
                    p1Counter++;
                    // Make sure we dont go out of bounds
                    if (i < tokens.Length - 1)
                    {
                        // Check if the next token is correct or not
                        if (!(isVariable(i + 1) || isNumber(i + 1) || tokens[i + 1].Equals("(")))
                        {
                            throw new FormulaFormatException("Invalid formula expression. Please check the expression after the (");
                        }
                    }
                }
                // Case where the token is a )
                else
                if (tokens[i].Equals(")"))
                {
                    // Increase the parenthesis counter
                    p2Counter++;
                    // Make sure we dont go out of bounds
                    if (i < tokens.Length - 1)
                    {
                        // Check if the next token is correct or not
                        if (!(isOperator(i + 1) || tokens[i + 1].Equals(")")))
                        {
                            throw new FormulaFormatException("Invalid formula expression. Please check the expression after the )");
                        }
                        // The number of ) parenthesis should not exceed the ( parenthesis because we are going from left to right
                        if (p2Counter > p1Counter)
                        {
                            throw new FormulaFormatException("Invalid formula expression. The number of open and closing parenthesis does not match");
                        }
                    }
                }
                // Case where the token is an operator
                else
                    if (isOperator(i))
                {
                    // Make sure we dont go out of bounds
                    if (i < tokens.Length - 1)
                    {
                        // Check if the next token is correct
                        if (!(isVariable(i + 1) || isNumber(i + 1) || tokens[i + 1].Equals("(")))
                        {
                            throw new FormulaFormatException("Invalid formula expression. Please check the expression after the operator");
                        }
                    }
                }
                // Case where the token is a number
                else if (isNumber(i))
                {
                    // Make sure we dont go out of bounds
                    if (i < tokens.Length - 1)
                    {
                        // Check if the next token is correct
                        if (!(isOperator(i + 1) || tokens[i + 1].Equals(")")))
                        {
                            throw new FormulaFormatException("Invalid formula expression. Please check the expression after the number");
                        }
                    }
                }
                // Case where the token is a number
                else if (isVariable(i))
                {
                    // Make sure we dont go out of bounds
                    if (i < tokens.Length - 1)
                    {
                        // Check if the next token is correct
                        if (!(isOperator(i + 1) || tokens[i + 1].Equals(")")))
                        {
                            throw new FormulaFormatException("Invalid formula expression. Please check the expression after the variable");
                        }
                    }
                }
            }
            // If the number of parenthesis is not equal the formula is wrong
            if (p1Counter != p2Counter)
            {
                throw new FormulaFormatException("Invalid formula expression. The number of open and closing parenthesis does not match");
            }
            // If the expression starts with an operator the formula is wrong
            if (isOperator(0))
            {
                throw new FormulaFormatException("Invalid formula expression. The expression cannot start with an operator");
            }
            // Check so we dont get index out of bounds
            if (tokens.Length != 0)
            {
                // If the last token is an operator the formula is wrong
                if (isOperator(tokens.Length - 1))
                {
                    throw new FormulaFormatException("Invalid formula expression. The last element cannot be an operator");
                }
            }
        }
        /// <summary>
        /// Checks if the token at index i is a variable.
        /// </summary>
        /// <param name="i">The index</param>
        /// <returns>true if its a variable and false otherwise</returns>
        private Boolean isVariable(int i)
        {
            // If its not an operator, parenthesis or a number it is a variable
            if (!isOperator(i) && !tokens[i].Equals("(") && !tokens[i].Equals(")") && !isNumber(i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if the token at index i is an operator
        /// </summary>
        /// <param name="i">The index</param>
        /// <returns>true if its an operator and false otherwise</returns>
        private Boolean isOperator(int i)
        {
            // We check the 4 operators that our application allows
            if (tokens[i].Equals("*") || tokens[i].Equals("/") || tokens[i].Equals("-") || tokens[i].Equals("+"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if the token at index i is a number
        /// </summary>
        /// <param name="i">The index</param>
        /// <returns>true if its a number and false otherwise</returns>
        private Boolean isNumber(int i)
        {
            // We try to parse the string to a number to check if its a number
            if (Double.TryParse(tokens[i], out double f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            /*
             * This code was copied and modified from the Assignment 1 - Formula Evaluator created by me
             */
            // Create variables to use later
            double result;
            Stack<string> opStack = new Stack<string>();
            Stack<double> valStack = new Stack<double>();
            double tryDouble;
            String actualString;
            // Go trough each of the elements (tokens) from the expression
            for (int i = 0; i < tokens.Length; i++)
            {
                actualString = tokens[i];
                // Check if its an integer and assign the value to tryInt
                if (double.TryParse(actualString, out tryDouble))
                {
                    // Case where we need to divide
                    if (hasOnTop(opStack, "/"))
                    {
                        // We cant divide by 0
                        if (tryDouble == 0)
                        {
                            return new FormulaError("Cannot divide by 0");
                        }
                        // Perform division
                        double poppedValue = valStack.Pop();
                        opStack.Pop();
                        double val = poppedValue / tryDouble;
                        valStack.Push(val);
                    }
                    // Case where we need to multiply
                    else if (hasOnTop(opStack, "*"))
                    {
                        // Perform multiplication
                        double poppedValue = valStack.Pop();
                        opStack.Pop();
                        double val = poppedValue * tryDouble;
                        valStack.Push(val);
                    }
                    else
                    {
                        valStack.Push(tryDouble);
                    }
                }
                // Case of addition and substraction
                else if (actualString.Equals("+") || actualString.Equals("-"))
                {
                    // Case of substraction
                    if (hasOnTop(opStack, "-"))
                    {
                        // Perform substraction
                        double val1 = valStack.Pop();
                        double val2 = valStack.Pop();
                        opStack.Pop();
                        double val = val2 - val1;
                        valStack.Push(val);
                    }
                    // Case of addition
                    else if (hasOnTop(opStack, "+"))
                    {
                        // Perform addition
                        double val1 = valStack.Pop();
                        double val2 = valStack.Pop();
                        opStack.Pop();
                        double val = val2 + val1;
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
                        // Perform addition
                        double val1 = valStack.Pop();
                        double val2 = valStack.Pop();
                        double val = val2 + val1;
                        opStack.Pop();
                        valStack.Push(val);
                    }
                    // Case of substraction with parenthesis
                    else if (hasOnTop(opStack, "-"))
                    {
                        // Perform substraction
                        double val1 = valStack.Pop();
                        double val2 = valStack.Pop();
                        double val = val2 - val1;
                        opStack.Pop();
                        valStack.Push(val);
                    }
                    if (!hasOnTop(opStack, "("))
                    {
                        throw new FormulaFormatException("Invalid formula expression");
                    }
                    // Take out the ( sign
                    opStack.Pop();
                    // Case of multiplication with parenthesis
                    if (hasOnTop(opStack, "*"))
                    {
                        // Perform multiplication
                        double val1 = valStack.Pop();
                        double val2 = valStack.Pop();
                        double val = val2 * val1;
                        opStack.Pop();
                        valStack.Push(val);
                    }
                    // Case of division with parenthesis
                    else if (hasOnTop(opStack, "/"))
                    {
                        // Get the 2 values
                        double val1 = valStack.Pop();
                        double val2 = valStack.Pop();
                        // Check if the divisor is going to be 0 because we cant do that
                        if (val1 == 0)
                        {
                            return new FormulaError("Cannot divide by 0");
                        }
                        // Perform division
                        opStack.Pop();
                        double val = val2 / val1;
                        valStack.Push(val);
                    }
                }
                // Case where its a variable
                else
                {
                    // Check if the variable exists if not throw an exception
                    try
                    {
                        tryDouble = lookup(actualString);
                    }
                    catch (Exception)
                    {
                        return new FormulaError("Variable not found");
                    }
                    // Case where we have to divide with the variable
                    if (hasOnTop(opStack, "/"))
                    {
                        // Check if we are going to divide by 0
                        if (tryDouble == 0)
                        {
                            return new FormulaError("Cannot divide by 0");
                        }
                        // Perform division
                        double poppedValue = valStack.Pop();
                        opStack.Pop();
                        double val = poppedValue / tryDouble;
                        valStack.Push(val);
                    }
                    else
                        // Case where we have to multiply with the variable
                        if (hasOnTop(opStack, "*"))
                    {
                        // Perform multiplication
                        double poppedValue = valStack.Pop();
                        opStack.Pop();
                        double val = poppedValue * tryDouble;
                        valStack.Push(val);
                    }
                    else
                    {
                        valStack.Push(tryDouble);
                    }
                }
            }
            // Now when the last token has been processed
            // If the operator stack is empty
            if (opStack.Count == 0)
            {
                result = valStack.Pop();
            }
            else
            // If the operator stack is not empty
            {
                double val1 = valStack.Pop();
                double val2 = valStack.Pop();
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
        private Boolean hasOnTop<T>(Stack<T> stack, T value)
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
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            return variables;
        }
        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            String formula = "";
            // Go trough every token and add it to our final result
            for (int i = 0; i < tokens.Length; i++)
            {
                if (Double.TryParse(tokens[i], out double n))
                {
                    formula = formula + n;
                }
                else
                {
                    formula = formula + tokens[i];
                }
            }
            return formula;
        }
        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            // Check if object is null or if its not Formula
            if (obj is null || !(obj is Formula))
            {
                return false;
            }
            // Convert the obj to type Formula to be able to compare the lengths
            Formula formula = (Formula)obj;
            // Check lengths
            if (this.tokens.Length != formula.tokens.Length)
            {
                return false;
            }
            double number1;
            double number2;
            // Go trough every token to check if they are equal
            for (int i = 0; i < this.tokens.Length; i++)
            {
                // If its and number we have to convert it first to a double and then to a string
                if (Double.TryParse(this.tokens[i], out number1) && Double.TryParse(formula.tokens[i], out number2))
                {
                    // Check if they are equal or not
                    if (!number1.ToString().Equals(number2.ToString()))
                    {
                        return false; ;
                    }
                }
                // We just compare the strings
                else
                {
                    // Check if the strings are equal
                    if (!this.tokens[i].Equals(formula.tokens[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            if ((f1 is null) && !(f2 is null))
            {
                return false;
            }
            if (!(f1 is null) && (f2 is null))
            {
                return false;
            }
            if ((f1 is null) && (f2 is null))
            {
                return true;
            }
            return f1.Equals(f2);
        }
        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            if ((f1 is null) && !(f2 is null))
            {
                return true;
            }
            if (!(f1 is null) && (f2 is null))
            {
                return true;
            }
            if ((f1 is null) && (f2 is null))
            {
                return false;
            }
            return !f1.Equals(f2);
        }
        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            // Use the ToString HashCode
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// Checks if the string is null, empty or a white space
        /// </summary>
        /// <param name="expression">The string to check</param>
        /// <returns>true if the string is null, empty or white space and false otherwise</returns>
        public static Boolean isStringNullOrEmpty(String expression)
        {
            // We cant check null with .Equals because throws an error so we check it first
            if (expression == null)
            {
                return true;
            }
            // Now we are safe so we can check with the .Equals function
            if (expression.Equals(" ") || expression.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";
            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})", lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);
            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }
    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }
    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }
        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}