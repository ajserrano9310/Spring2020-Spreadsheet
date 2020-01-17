using FormulaEvaluator;
using System;
namespace Test_The_Evaluator_Console_App
{
    class EvaluatorTester
    {
        /// <summary>
        /// Tester for the FormulaEvaluator library. It includes tests that obtain correct answers and cases where an exception is thrown. 
        /// </summary>
        /// <param name="args">For testing purposes we are ignoring args</param>
        static void Main(string[] args)
        {
            // Call methods for testing purposes with output on the console
            Console.WriteLine(testOneNumber());
            Console.WriteLine(testSimpleAddition());
            Console.WriteLine(testSimpleSubstraction());
            Console.WriteLine(testSimpleMultiplication());
            Console.WriteLine(testSimpleDivision());
            Console.WriteLine(testOneNumberWithParenthesis());
            Console.WriteLine(testSimpleAdditionWithParenthesis());
            Console.WriteLine(testSimpleSubstractionWithParenthesis());
            Console.WriteLine(testSimpleMultiplicationWithParenthesis());
            Console.WriteLine(testSimpleDivisionWithParenthesis());
            Console.WriteLine(testSimpleExpression());
            Console.WriteLine(testNull());
            Console.WriteLine(testWrongVariable());
            Console.WriteLine(testWithVariables());
            Console.WriteLine(testDivideByZero());
            Console.WriteLine(testWeirdExpression());
            Console.WriteLine(testWeirdVariable());
            Console.WriteLine(testDivideByZeroWithParenthesis());
            Console.WriteLine(testJustSymbol());
            Console.WriteLine(testWeirdMultiplication());

        }
        /// <summary>
        /// A dictionary that contains variables and returns their value
        /// </summary>
        /// <param name="value">The variable to look up</param>
        /// <returns>The value of the variable</returns>
        public static int SimpleLookup(string value)
        {
            if (value == "A7")
                return 5;
            else if (value == "jfg34^wuf$u")
                return 10;
            else if (value == "A1")
                return 2;
            else
                throw new ArgumentException();
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with only one number
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testOneNumber()
        {
            int result = Evaluator.Evaluate("5", SimpleLookup);
            if (result == 5)
            {
                return "testOneNumber Passed! " + "Expected: 5 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 5 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a simple addition
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleAddition()
        {
            int result = Evaluator.Evaluate("5+5", SimpleLookup);
            if (result == 10)
            {
                return "testOneNumber Passed! " + "Expected: 10 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 10 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a simple substraction
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleSubstraction()
        {
            int result = Evaluator.Evaluate("5-3", SimpleLookup);
            if (result == 2)
            {
                return "testOneNumber Passed! " + "Expected: 2 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 2 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a simple multiplication
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleMultiplication()
        {
            int result = Evaluator.Evaluate("2*6", SimpleLookup);
            if (result == 12)
            {
                return "testOneNumber Passed! " + "Expected: 12 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 12 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a simple division
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleDivision()
        {
            int result = Evaluator.Evaluate("4/2", SimpleLookup);
            if (result == 2)
            {
                return "testOneNumber Passed! " + "Expected: 2 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 2 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with just a number and parenthesis
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testOneNumberWithParenthesis()
        {
            int result = Evaluator.Evaluate("(5)", SimpleLookup);
            if (result == 5)
            {
                return "testOneNumber Passed! " + "Expected: 5 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 5 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with addition and parenthesis
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleAdditionWithParenthesis()
        {
            int result = Evaluator.Evaluate("(5+5)", SimpleLookup);
            if (result == 10)
            {
                return "testOneNumber Passed! " + "Expected: 10 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 10 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with substraction and parenthesis
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleSubstractionWithParenthesis()
        {
            int result = Evaluator.Evaluate("(5-3)", SimpleLookup);
            if (result == 2)
            {
                return "testOneNumber Passed! " + "Expected: 2 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 2 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with multiplication and parenthesis
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleMultiplicationWithParenthesis()
        {
            int result = Evaluator.Evaluate("(2*6)", SimpleLookup);
            if (result == 12)
            {
                return "testOneNumber Passed! " + "Expected: 12 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 12 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with division and parenthesis
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleDivisionWithParenthesis()
        {
            int result = Evaluator.Evaluate("(4/2)", SimpleLookup);
            if (result == 2)
            {
                return "testOneNumber Passed! " + "Expected: 2 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 2 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a simple expression
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testSimpleExpression()
        {
            int result = Evaluator.Evaluate("(2 + 3) * 5 + 2", SimpleLookup);
            if (result == 27)
            {
                return "testOneNumber Passed! " + "Expected: 27 Actual: " + result;
            }
            else
            {
                return "testOneNumber Failed! " + "Expected: 27 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with null input
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testNull()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate(null, SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testNull Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testNull Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a variable not in the dictionary
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testWrongVariable()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("A123+5", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testWrongVariable Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testWrongVariable Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with variables
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testWithVariables()
        {
            int result = Evaluator.Evaluate("(2 + 3) * A7 + 2 + A1", SimpleLookup);
            if (result == 29)
            {
                return "testWithVariables Passed! " + "Expected: 29 Actual: " + result;
            }
            else
            {
                return "testWithVariables Failed! " + "Expected: 29 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a division by zero
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testDivideByZero()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("5/0", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testDivideByZero Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testDivideByZero Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a division by zero with parenthesis
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testDivideByZeroWithParenthesis()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("(5/0)", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testDivideByZeroWithParenthesis Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testDivideByZeroWithParenthesis Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with an expression that is not possible
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testWeirdExpression()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("(/4+2)*3)", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testWeirdExpression Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testWeirdExpression Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a variable with strange characters
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testWeirdVariable()
        {
            int result = Evaluator.Evaluate("jfg34^wuf$u", SimpleLookup);
            if (result == 10)
            {
                return "testWeirdVariable Passed! " + "Expected: 10 Actual: " + result;
            }
            else
            {
                return "testWeirdVariable Failed! " + "Expected: 10 Actual: " + result;
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works by just using a symbol as input
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testJustSymbol()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("*", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testJustSymbol Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testJustSymbol Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a missing number on the expression
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testMissingNumber()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("*4", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testMissingNumber Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testMissingNumber Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a multiplication that is not correct
        /// </summary>
        /// <returns>If the program catched the exception successfully or not</returns>
        public static string testWeirdMultiplication()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate("2(5)", SimpleLookup);
            }
            catch (ArgumentException)
            {
                passed = true;
            }
            if (passed)
            {
                return "testWeirdMultiplication Passed! " + "Expected: ArgumentException Actual: ArgumentException";
            }
            else
            {
                return "testWeirdMultiplication Failed! " + "Expected: ArgumentException";
            }
        }
        /// <summary>
        /// Tests if the FormulaEvaluator works with a division that results in an integer
        /// </summary>
        /// <returns>If the test failed or passed</returns>
        public static string testNonIntDivision()
        {
            int result = Evaluator.Evaluate("1/2", SimpleLookup);
            if (result == 0)
            {
                return "testNonIntDivision Passed! " + "Expected: 0 Actual: " + result;
            }
            else
            {
                return "testNonIntDivision Failed! " + "Expected: 0 Actual: " + result;
            }
        }
        //Test 1 number
        //Test 1 variable
        //Test 1 sign
        //Test normal expression
        //Test hard expression
        //Test expression with delegates
        //Test expression with delegates not existing
        //Test SimpleLookup expression
        //Test empty expression
        //Add text to exceptions
        //You should write at least one test for each operator, one for parentheses, one for order of operation, and then as many more as you can think of. 
    }
}
