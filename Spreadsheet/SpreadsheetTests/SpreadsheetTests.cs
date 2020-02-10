/// <summary> 
/// Author:    Alejandro Rubio
/// Partner:   None
/// Date:      2/9/2020
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Alejandro Rubio - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Alejandro Rubio, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// This file tests the Spreadsheet Library that uses DependencyGraph and Formula
/// </summary>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SpreadsheetTests
{
    /// <summary>
    /// Class to test the functionality of the Spreadsheet class made from the AbstractSpreadsheet.
    /// </summary>
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod1()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.GetCellContents(null);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod2()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.GetCellContents("+A1");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod3()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula= new Formula("1");
            spreadsheet.SetCellContents(null,formula);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod4()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = new Formula("1");
            spreadsheet.SetCellContents("+A2", formula);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod5()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = null;
            spreadsheet.SetCellContents("A2", formula);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod6()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            string content = null;
            spreadsheet.SetCellContents("A3", content);
        }
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestMethod7()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = new Formula("A1+A1");
            spreadsheet.SetCellContents("A1", formula);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod8()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents(null, "Hello");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod9()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("+A1", "Hello");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod10()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents(null, 4.2);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod11()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("+A7", 4.7);
        }
        [TestMethod]
        public void TestMethod12()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("A7", 4.7);
            HashSet<string> result = new HashSet<string>();
            result.Add("A7");
            Assert.IsTrue(spreadsheet.SetCellContents("A7",123.0).SetEquals(result));
        }
        [TestMethod]
        public void TestMethod13()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("A8", 5.7);
            Assert.AreEqual(spreadsheet.GetCellContents("A8"), 5.7);
        }
        [TestMethod]
        public void TestMethod14()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Assert.AreEqual(spreadsheet.GetCellContents("A8"), "");
        }
        [TestMethod]
        public void TestMethod15()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", "Hello");
            spreadsheet.SetCellContents("F2", "World");
            List<string> result = new List<string>();
            result.Add("F1");
            result.Add("F2");
            Assert.IsTrue(spreadsheet.GetNamesOfAllNonemptyCells().SequenceEqual(result));
        }
        [TestMethod]
        public void TestMethod16()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", "Hello");
            spreadsheet.SetCellContents("F1", "World");
            Assert.IsTrue(spreadsheet.GetCellContents("F1").Equals("World"));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod17()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents(null, "Hello");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod18()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("$A3", "World");
        }
        [TestMethod]
        public void TestMethod19()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", 5.0);
            HashSet<string> result = new HashSet<string>();
            result.Add("F2");
            Assert.IsTrue(spreadsheet.SetCellContents("F2", new Formula("F1+5")).SetEquals(result));
        }
        [TestMethod]
        public void TestMethod20()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", new Formula("1+1"));
            spreadsheet.SetCellContents("F1", new Formula("2+2"));
            HashSet<string> result = new HashSet<string>();
            result.Add("F1");
            Assert.IsTrue(spreadsheet.SetCellContents("F1", new Formula("2+2")).SetEquals(result));
        }
    }
}
