// Author: Alejandro Rubio
// Date: 1/31/2020
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
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormula1()
        {
            Formula f = new Formula("");
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormula2()
        {
            Formula f = new Formula(" ");
        }
        [TestMethod()]
        public void TestConstructorVariable()
        {
            Formula f = new Formula("X3");
        }
        [TestMethod()]
        public void TestConstructorNumber()
        {
            Formula f = new Formula("5");
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNullFormula()
        {
            Formula f = new Formula(null);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestIsValidFalse()
        {
            Formula f = new Formula("CS",Normalizer,IsValid);
        }
        [TestMethod]
        public void TestVariablesCounter1()
        {
            Formula f = new Formula("X1+x1", Normalizer, IsValid);
            List<string> expected = new List<string>();
            expected.Add("X1");
            Assert.AreEqual(expected.ToString(), f.GetVariables().ToString());
        }
        [TestMethod]
        public void TestVariablesCounter2()
        {
            Formula f = new Formula("F5+F10", Normalizer, IsValid);
            List<string> expected = new List<string>();
            expected.Add("F5");
            expected.Add("F10");
            Assert.AreEqual(expected.ToString(), f.GetVariables().ToString());
        }
        [TestMethod]
        public void TestExpression1()
        {
            Formula f = new Formula("5+10", Normalizer, IsValid);
            Assert.AreEqual(15.0, f.Evaluate(Lookup));
        }
        [TestMethod]
        public void TestExpression2()
        {
            Formula f = new Formula("(5*2)/(5-0)", Normalizer, IsValid);
            Assert.AreEqual(2.0, f.Evaluate(Lookup));
        }
        [TestMethod]
        public void TestExpression3()
        {
            Formula f = new Formula("5");
            Assert.AreEqual(5.0, f.Evaluate(Lookup));
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression1()
        {
            Formula f = new Formula("++",Normalizer,IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression2()
        {
            Formula f = new Formula("+)", Normalizer, IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression3()
        {
            Formula f = new Formula("(-", Normalizer, IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression4()
        {
            Formula f = new Formula("()", Normalizer, IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression5()
        {
            Formula f = new Formula("(22+2)))))", Normalizer, IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression6()
        {
            Formula f = new Formula("(22+2)x", Normalizer, IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression7()
        {
            Formula f = new Formula("X1(", Normalizer, IsValid);
        }
        [TestMethod(), Timeout(5000)]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidExpression8()
        {
            Formula f = new Formula("+", Normalizer, IsValid);
        }
        [TestMethod()]
        public void TestInvalidExpression9()
        {
            Formula f = new Formula("5/0", Normalizer, IsValid);
            Assert.IsTrue(f.Evaluate(Lookup) is FormulaError);
        }
        [TestMethod()]
        public void TestInvalidExpression10()
        {
            Formula f = new Formula("(5-0)/(0+0)", Normalizer, IsValid);
            Assert.IsTrue(f.Evaluate(Lookup) is FormulaError);
        }
        [TestMethod()]
        public void TestExpression4()
        {
            Formula f = new Formula("(5-0)+(0+0)", Normalizer, IsValid);
            Assert.AreEqual(5.0, f.Evaluate(Lookup));
        }
        [TestMethod()]
        public void TestExpression5()
        {
            Formula f = new Formula("(5-0)-(0+0)", Normalizer, IsValid);
            Assert.AreEqual(5.0, f.Evaluate(Lookup));
        }
        [TestMethod()]
        public void TestExpression6()
        {
            Formula f = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(10.0, f.Evaluate(Lookup));
        }
        [TestMethod()]
        public void TestHashCode1()
        {
            Formula f1 = new Formula("10*X1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(f1.GetHashCode(),f2.GetHashCode());
        }
        [TestMethod()]
        public void TestHashCode2()
        {
            Formula f1 = new Formula("10*x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
        }
        [TestMethod()]
        public void TestEquals1()
        {
            Formula f1 = new Formula("10*x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(true, f1.Equals(f2));
        }
        [TestMethod()]
        public void TestEquals2()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(false, f1.Equals(f2));
        }
        [TestMethod()]
        public void TestEqualsSign1()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(false, f1==f2);
        }
        [TestMethod()]
        public void TestEqualsSign2()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Formula f2 = new Formula("10*X1", Normalizer, IsValid);
            Assert.AreEqual(true, f1 != f2);
        }
        [TestMethod()]
        public void TestFormulaToString1()
        {
            Formula f1 = new Formula("10-x1", Normalizer, IsValid);
            Assert.AreEqual("10-X1", f1.ToString());
        }
        [TestMethod()]
        public void TestFormulaToString2()
        {
            Formula f1 = new Formula("10 - x1", Normalizer, IsValid);
            Assert.AreEqual("10-X1", f1.ToString());
        }
        private static string Normalizer(string s)
        {
            return s.ToUpper();
        }
        private static Boolean IsValid(string s)
        {
            if (!(Regex.IsMatch(s, "^[a-z A-Z]+[0-9]")))
            {
                return false;
            }
            return true;
        }
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
