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
            Formula f = new Formula("3x+4x", Normalizer, IsValid);
            List<string> expected = new List<string>();
            expected.Add("3X");
            expected.Add("4X");
            Assert.AreEqual(expected, f.GetVariables());
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
