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
namespace SpreadsheetTests
{
    /// <summary>
    /// Class to test the functionality of the Spreadsheet class made from the AbstractSpreadsheet.
    /// </summary>
    [TestClass]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Test for GetCellContents throwing InvalidNameException when using null as parameter.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsNull()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.GetCellContents(null);
        }
        /// <summary>
        /// Test for GetCellContents throwing InvalidNameException with an invalid cell name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.GetCellContents("+A1");
        }
        /// <summary>
        /// Test for SetCellContents throwing InvalidNameException with null and a Formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNullWithFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = new Formula("1");
            spreadsheet.SetCellContents(null, formula);
        }
        /// <summary>
        /// Test for SetCellContents throwing InvalidNameException with an invalid name and a Formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsInvalidWithFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = new Formula("1");
            spreadsheet.SetCellContents("+A2", formula);
        }
        /// <summary>
        /// Test for SetCellContents throwing ArgumentNullException with a null Formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetCellContentsNullFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = null;
            spreadsheet.SetCellContents("A2", formula);
        }
        /// <summary>
        /// Test for SetCellContents throwing ArgumentNullException with null as text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetCellContentsNullContent()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            string content = null;
            spreadsheet.SetCellContents("A3", content);
        }
        /// <summary>
        /// Test for SetCellContents throwing CircularException when using and assigning the same cell to a formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestSetCellContentsCircularException()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula = new Formula("A1+A1");
            spreadsheet.SetCellContents("A1", formula);
        }
        /// <summary>
        /// Test SetCellContents throwing InvalidNameException with null as input and some text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNullWithText()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents(null, "Hello");
        }
        /// <summary>
        /// Tests SetCellContents throwing InvalidNameException with an invalid cell name and some text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsInvalidWithText()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("+A1", "Hello");
        }
        /// <summary>
        ///Test SetCellContents throwing InvalidNameException with a null and a double. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNullWithDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents(null, 4.2);
        }
        /// <summary>
        /// Test SetCellContents throwing InvalidNameException with an invalid cell name and a double. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsInvalidWithDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("+A7", 4.7);
        }
        /// <summary>
        /// Test SetCellContents with valid values.
        /// </summary>
        [TestMethod]
        public void TestValidSetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("A7", 4.7);
            HashSet<string> result = new HashSet<string>();
            result.Add("A7");
            Assert.IsTrue(spreadsheet.SetCellContents("A7", 123.0).SetEquals(result));
        }
        /// <summary>
        /// Test GetCellContents with valid values.
        /// </summary>
        [TestMethod]
        public void TestValidGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("A8", 5.7);
            Assert.AreEqual(spreadsheet.GetCellContents("A8"), 5.7);
        }
        /// <summary>
        /// Tests GetCellContents with a cell that does not exist.
        /// </summary>
        [TestMethod]
        public void TestEmptyGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Assert.AreEqual(spreadsheet.GetCellContents("A8"), "");
        }
        /// <summary>
        /// Tests GetNameOfAllNonemptyCells with two valid Cells.
        /// </summary>
        [TestMethod]
        public void TestGetNamesOfAllNonemptyCells()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", "Hello");
            spreadsheet.SetCellContents("F2", "World");
            List<string> result = new List<string>();
            result.Add("F1");
            result.Add("F2");
            Assert.IsTrue(spreadsheet.GetNamesOfAllNonemptyCells().SequenceEqual(result));
        }
        /// <summary>
        /// Tests GetCellContents with replaced values
        /// </summary>
        [TestMethod]
        public void TestReplacedGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", "Hello");
            spreadsheet.SetCellContents("F1", "World");
            Assert.IsTrue(spreadsheet.GetCellContents("F1").Equals("World"));
        }
        /// <summary>
        /// Tests SetCellContents throwing InvalidNameException with null and a text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsNullWithText2()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents(null, "Hello");
        }
        /// <summary>
        /// Tests SetCellContents throwing InvalidNameException with an invalid cell name and some text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetCellContentsInvalidWithText2()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("$A3", "World");
        }
        /// <summary>
        /// Test SetCellContents with a formula that uses another cell
        /// </summary>
        [TestMethod]
        public void TestSetCellContentsWithFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", 5.0);
            HashSet<string> result = new HashSet<string>();
            result.Add("F2");
            Assert.IsTrue(spreadsheet.SetCellContents("F2", new Formula("F1+5")).SetEquals(result));
        }
        /// <summary>
        /// Test SetCellContents with replaced Formula
        /// </summary>
        [TestMethod]
        public void TestSetCellContentsWithReplacedCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellContents("F1", new Formula("1+1"));
            HashSet<string> result = new HashSet<string>();
            result.Add("F1");
            Assert.IsTrue(spreadsheet.SetCellContents("F1", new Formula("2+2")).SetEquals(result));
        }
        /// <summary>
        /// Test SetCellContents where it has to recalculate previous values
        /// </summary>
        [TestMethod]
        public void TestMultipleSetCellContentsWithRecalculate()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Formula formula1 = new Formula("A1+A2");
            Formula formula2 = new Formula("B1+3");
            spreadsheet.SetCellContents("A1", 5.0);
            spreadsheet.SetCellContents("A2", 6.0);
            spreadsheet.SetCellContents("B1", formula1);
            spreadsheet.SetCellContents("C1", formula2);
            HashSet<string> result = new HashSet<string>();
            result.Add("A1");
            result.Add("B1");
            result.Add("C1");
            Assert.IsTrue(spreadsheet.SetCellContents("A1", 4.0).SetEquals(result));
        }
    }
}
