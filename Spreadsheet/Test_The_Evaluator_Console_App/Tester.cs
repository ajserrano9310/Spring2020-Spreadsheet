using FormulaEvaluator;
using System;
namespace Test_The_Evaluator_Console_App
{
    class Tester
    {
        static void Main(string[] args)
        {
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
        }
        public static int SimpleLookup(string v)
        {
            // Do anything here. Decide whether or not this delegate 
            // has a value for v, and return its value, or throw if it doesn't.

            if (v == "A7")
                return 5;
            else if (v == "jfg34^wuf$u")
                return 10;
            else if (v == "A1")
                return 2;
            else
                throw new ArgumentException();
        }


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

        public static string testSimpleAddition ()
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

        //

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
        //
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
        
        public static string testNull()
        {
            Boolean passed = false;
            try
            {
                Evaluator.Evaluate(null, SimpleLookup);
            }
            catch(ArgumentException)
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


        //Test 1 number
        //Test 1 variable
        //Test 1 sign
        //Test normal expression
        //Test hard expression
        //Test expression with delegates
        //Test expression with delegates not existing
        //Test SimpleLookup expression
        //Test empty expression
        //You should write at least one test for each operator, one for parentheses, one for order of operation, and then as many more as you can think of. 
    }
}
