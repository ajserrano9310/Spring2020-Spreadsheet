// Author:     Alejandro Rubio
// Partner:    None
// Date:       1/31/2020
// Course:     CS 3500, University of Utah, School of Computing
// Assignment: Assignment 3 - Formula
// Copyright:  CS 3500 and Alejandro Rubio - This work may not be copied for use in Academic Coursework.
// This file is part of a Library that tests the Formula
// I pledge that I did this work myself
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        /// <summary>
        /// Tests if the Formula constructor throws the desired exception when entering a blank field as the formula
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormula1()
        {
            Formula f = new Formula("");
        }
        /// <summary>
        /// Tests if the Formula constructor throws the desired exception when entering a space as the formula
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormula2()
        {
            Formula f = new Formula(" ");
        }
        /// <summary>
        /// Test the Formula constructor with a variable
        /// </summary>
        [TestMethod()]
        public void TestConstructorVariable()
        {
            Formula f = new Formula("X3");
        }
        /// <summary>
        /// Tests the Formula constructor with a number
        /// </summary>
        [TestMethod()]
        public void TestConstructorNumber()
        {
            Formula f = new Formula("5");
        }
        /// <summary>
        /// Tests the Formula constructor if user creates a formula with null as parameter
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNullFormula()
        {
            Formula f = new Formula(null);
        }
        /// <summary>
        /// Tests if the isValid Function throws the desired exception if the variable does not match the desired format
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestIsValidFalse()
        {
            Formula f = new Formula("CS", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests the GetVariables method with two equal variables
        /// </summary>
        [TestMethod]
        public void TestVariablesList1()
        {
            Formula f = new Formula("X1+x1", Normalizer, IsValid);
            List<string> expected = new List<string>();
            expected.Add("X1");
            Assert.AreEqual(expected.ToString(), f.GetVariables().ToString());
        }
        /// <summary>
        /// Tests the GetVariables method with two different variables
        /// </summary>
        [TestMethod]
        public void TestVariablesList2()
        {
            Formula f = new Formula("F5+F10", Normalizer, IsValid);
            List<string> expected = new List<string>();
            expected.Add("F5");
            expected.Add("F10");
            Assert.AreEqual(expected.ToString(), f.GetVariables().ToString());
        }
        /// <summary>
        /// Tests the Evaluate method with a simple operation
        /// </summary>
        [TestMethod]
        public void TestExpression1()
        {
            Formula f = new Formula("5+10", Normalizer, IsValid);
            Assert.AreEqual(15.0, f.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the Evaluate method with a more advanced operation
        /// </summary>
        [TestMethod]
        public void TestExpression2()
        {
            Formula f = new Formula("(5*2)/(5-0)", Normalizer, IsValid);
            Assert.AreEqual(2.0, f.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the Evaluate method with a single number
        /// </summary>
        [TestMethod]
        public void TestExpression3()
        {
            Formula f = new Formula("5");
            Assert.AreEqual(5.0, f.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests if the constructor throws the desired error if the user inputs two operators together
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression1()
        {
            Formula f = new Formula("++", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error when the user inputs just an operator
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression2()
        {
            Formula f = new Formula("+)", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error when the user inputs a wrong formula
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression3()
        {
            Formula f = new Formula("(-", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error when the user inputs just two parenthesis
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression4()
        {
            Formula f = new Formula("()", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error when parenthesis do not match
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression5()
        {
            Formula f = new Formula("(22+2)))))", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error if a variable is placed in a wrong place
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression6()
        {
            Formula f = new Formula("(22+2)x", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error if a ( is placed in a wrong place and ) number do not match
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression7()
        {
            Formula f = new Formula("X1(", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the constructor throws the desired error if the user inputs just an operator
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression8()
        {
            Formula f = new Formula("+", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if FormulaError is thrown when dividing by 0
        /// </summary>
        [TestMethod()]
        public void TestInvalidExpression9()
        {
            Formula f = new Formula("5/0", Normalizer, IsValid);
            Assert.IsTrue(f.Evaluate(Lookup) is FormulaError);
        }
        /// <summary>
        /// Tests if FormulaError is thrown when dividing by 0 with a more complicated expression
        /// </summary>
        [TestMethod()]
        public void TestInvalidExpression10()
        {
            Formula f = new Formula("(5-0)/(0+0)", Normalizer, IsValid);
            Assert.IsTrue(f.Evaluate(Lookup) is FormulaError);
        }
        /// <summary>
        /// Tests if the exception is thrown if a number does not have a valid operation next to it
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression11()
        {
            Formula f = new Formula("5(", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the exception is thrown if the number of parenthesis does not match
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression12()
        {
            Formula f = new Formula("(((((((((((5+2))))))", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests if the exception is thrown if the last token is an operator
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression13()
        {
            Formula f = new Formula("5+2*", Normalizer, IsValid);
        }
        /// <summary>
        /// Tests an addition between parenthesis
        /// </summary>
        [TestMethod()]
        public void TestExpression4()
        {
            Formula f = new Formula("(5-0)+(0+0)", Normalizer, IsValid);
            Assert.AreEqual(5.0, f.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests a substraction between parenthesis
        /// </summary>
        [TestMethod()]
        public void TestExpression5()
        {
            Formula f = new Formula("(5-0)-(0+0)", Normalizer, IsValid);
            Assert.AreEqual(5.0, f.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests multiplication between a variable and a number
        /// </summary>
        [TestMethod()]
        public void TestExpression6()
        {
            Formula f = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(10.0, f.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the GetHashCode Method with two equal formulas
        /// </summary>
        [TestMethod()]
        public void TestHashCode1()
        {
            Formula f1 = new Formula("10*X1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
        }
        /// <summary>
        /// Tests the GetHashCode Method with different variables that convert to same variables due to the Normalizer
        /// </summary>
        [TestMethod()]
        public void TestHashCode2()
        {
            Formula f1 = new Formula("10*x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
        }
        /// <summary>
        /// Tests the Equals method using different variables that convert to same variables due to the Normalizer
        /// </summary>
        [TestMethod()]
        public void TestEquals1()
        {
            Formula f1 = new Formula("10*x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(true, f1.Equals(f2));
        }
        /// <summary>
        /// Tests the Equals method with different Formulas
        /// </summary>
        [TestMethod()]
        public void TestEquals2()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(false, f1.Equals(f2));
        }
        /// <summary>
        /// Tests the equals sign (==) with two different formulas
        /// </summary>
        [TestMethod()]
        public void TestEqualsSign1()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(false, f1 == f2);
        }
        /// <summary>
        /// Tests the not equals sign (!=) with two different formulas
        /// </summary>
        [TestMethod()]
        public void TestEqualsSign2()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(true, f1 != f2);
        }
        /// <summary>
        /// Tests the ToString method with a variable that converts due to the Normalizer
        /// </summary>
        [TestMethod()]
        public void TestFormulaToString1()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Assert.AreEqual("10-X1", f1.ToString());
        }
        /// <summary>
        /// Tests the ToString method with a formula that has spaces
        /// </summary>
        [TestMethod()]
        public void TestFormulaToString2()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual("10-X1", f1.ToString());
        }
        /// <summary>
        /// Tests the Equals method with null as the object
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithNull()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(false, f1.Equals(null));
        }
        /// <summary>
        /// Tests the Equals method with string as the object
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithString()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            String s = "Hello";
            Assert.AreEqual(false, f1.Equals(s));
        }
        /// <summary>
        /// Tests the Equals method with a different formula
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithDifferentFormula()
        {
            Formula f1 = new Formula("10 - x1+7", Normalizer, IsValid);
            Formula f2 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(false, f1.Equals(f2));
        }
        /// <summary>
        /// Tests the Equals method with an equal formula
        /// </summary>
        [TestMethod()]
        public void TestEqualsWithEqualFormula()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            Formula f2 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(true, f1.Equals(f2));
        }
        /// <summary>
        /// Tests the Equals sign method with f1 null
        /// </summary>
        [TestMethod()]
        public void TestEqualsSignF1Null()
        {
            Formula f2 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(false, f2 == null);
        }
        /// <summary>
        /// Tests the Equals sign method with f2 null
        /// </summary>
        [TestMethod()]
        public void TestEqualsSignF2Null()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(false, f1 == null);
        }
        /// <summary>
        /// Tests the Not Equals sign method with f1 null
        /// </summary>
        [TestMethod()]
        public void TestNotEqualsSignF1Null()
        {
            Formula f2 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(true, f2 != null);
        }
        /// <summary>
        /// Tests the Not Equals sign method with f2 null
        /// </summary>
        [TestMethod()]
        public void TestNotEqualsSignF2Null()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual(true, f1 != null);
        }
        /*
         * These Formula expressions were taken from the Assignment One - Formula Evaluator Grading Tests and modified for the Evaluate method tests
         */
        /// <summary>
        /// Tests the Evaluate method with a complex formula
        /// </summary>
        [TestMethod()]
        public void TestEvaluateComplex1()
        {
            Formula f1 = new Formula("2+3*5+(3+4*8)*5+2", Normalizer, IsValid);
            Assert.AreEqual(194.0, f1.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the Evaluate method with a complex formula
        /// </summary>
        [TestMethod()]
        public void TestEvaluateComplex2()
        {
            Formula f1 = new Formula("2+3*(3+5)", Normalizer, IsValid);
            Assert.AreEqual(26.0, f1.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the Evaluate method with a complex formula
        /// </summary>
        [TestMethod()]
        public void TestEvaluateComplex3()
        {
            Formula f1 = new Formula("(1*1)-2/2", Normalizer, IsValid);
            Assert.AreEqual(0.0, f1.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the Evaluate method with a complex formula
        /// </summary>
        [TestMethod()]
        public void TestEvaluateComplex4()
        {
            Formula f1 = new Formula("2+(3+5*9)", Normalizer, IsValid);
            Assert.AreEqual(50.0, f1.Evaluate(Lookup));
        }
        /// <summary>
        /// Tests the Evaluate method with a complex formula
        /// </summary>
        [TestMethod()]
        public void TestEvaluateComplex5()
        {
            Formula f1 = new Formula("2+(3+5)", Normalizer, IsValid);
            Assert.AreEqual(10.0, f1.Evaluate(Lookup));
        }
        /// <summary>
        /// Converts the string to uppercase characters
        /// </summary>
        /// <param name="s">The string to normalize</param>
        /// <returns>The converted string</returns>
        private static string Normalizer(string s)
        {
            return s.ToUpper();
        }
        /// <summary>
        /// Checks if the string is valid according to the parameter
        /// </summary>
        /// <param name="s">The string to evaluate</param>
        /// <returns>true if it is valid and false otherwise</returns>
        private static Boolean IsValid(string s)
        {
            if (!(Regex.IsMatch(s, "^[a-z A-Z]+[0-9]")))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Small dictionary that assigns values to variables
        /// </summary>
        /// <param name="s">The variable to look for</param>
        /// <returns>The value of the variable</returns>
        private static double Lookup(string s)
        {
            if (s.Equals("X1"))
            {
                return 1;
            }
            if (s.Equals("X2"))
            {
                return 2;
            }
            if (s.Equals("X5"))
            {
                return 0;
            }
            throw new System.ArgumentException();
        }
    }
}
