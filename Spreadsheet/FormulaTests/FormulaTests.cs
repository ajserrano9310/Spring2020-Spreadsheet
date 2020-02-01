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
            Formula f = new Formula("6x",Normalizer,IsValid);
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








        private static string Normalizer(string s)
        {
            return s.ToLower();
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
