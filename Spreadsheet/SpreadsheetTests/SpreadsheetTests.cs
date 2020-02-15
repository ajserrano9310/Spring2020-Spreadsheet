/// <summary> 
/// Assignment: Assignment Five - Spreadsheet Model
/// Author:    Alejandro Rubio
/// Partner:   None
/// Date:      2/14/2020
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
        /// Test for SetContentsOfCell throwing InvalidNameException with null and a Formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNullWithFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell(null, "=1");
        }
        /// <summary>
        /// Test for SetContentsOfCell throwing InvalidNameException with an invalid name and a Formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellInvalidWithFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("+A2", "=1");
        }
        /// <summary>
        /// Test for SetContentsOfCell throwing ArgumentNullException with a null Formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetContentsOfCellNullFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A2", null);
        }
        /// <summary>
        /// Test for SetContentsOfCell throwing ArgumentNullException with null as text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetContentsOfCellNullContent()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            string content = null;
            spreadsheet.SetContentsOfCell("A3", content);
        }
        /// <summary>
        /// Test for SetContentsOfCell throwing CircularException when using and assigning the same cell to a formula.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestSetContentsOfCellCircularException()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=A1+A1");
        }
        /// <summary>
        /// Test SetContentsOfCell throwing InvalidNameException with null as input and some text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNullWithText()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell(null, "Hello");
        }
        /// <summary>
        /// Tests SetContentsOfCell throwing InvalidNameException with an invalid cell name and some text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellInvalidWithText()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("+A1", "Hello");
        }
        /// <summary>
        ///Test SetContentsOfCell throwing InvalidNameException with a null and a double. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNullWithDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell(null, "4.2");
        }
        /// <summary>
        /// Test SetContentsOfCell throwing InvalidNameException with an invalid cell name and a double. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellInvalidWithDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("+A7", "4.7");
        }
        /// <summary>
        /// Test SetContentsOfCell with valid values.
        /// </summary>
        [TestMethod]
        public void TestValidSetContentsOfCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A7", "4.7");
            List<string> result = new List<string>();
            result.Add("A7");
            Assert.IsTrue(spreadsheet.SetContentsOfCell("A7", "123.0").SequenceEqual(result));
        }
        /// <summary>
        /// Test GetCellContents with valid values.
        /// </summary>
        [TestMethod]
        public void TestValidGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A8", "5.7");
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
            spreadsheet.SetContentsOfCell("F1", "Hello");
            spreadsheet.SetContentsOfCell("F2", "World");
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
            spreadsheet.SetContentsOfCell("F1", "Hello");
            spreadsheet.SetContentsOfCell("F1", "World");
            Assert.IsTrue(spreadsheet.GetCellContents("F1").Equals("World"));
        }
        /// <summary>
        /// Tests SetContentsOfCell throwing InvalidNameException with null and a text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellNullWithText2()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell(null, "Hello");
        }
        /// <summary>
        /// Tests SetContentsOfCell throwing InvalidNameException with an invalid cell name and some text.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetContentsOfCellInvalidWithText2()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("$A3", "World");
        }
        /// <summary>
        /// Test SetContentsOfCell with a formula that uses another cell
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("F1", "5.0");
            List<string> result = new List<string>();
            result.Add("F2");
            Assert.IsTrue(spreadsheet.SetContentsOfCell("F2", "=F1+5").SequenceEqual(result));
        }
        /// <summary>
        /// Test SetContentsOfCell with replaced Formula
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellWithReplacedCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("F1", "=1+1");
            List<string> result = new List<string>();
            result.Add("F1");
            Assert.IsTrue(spreadsheet.SetContentsOfCell("F1", "=2+2").SequenceEqual(result));
        }
        /// <summary>
        /// Test SetContentsOfCell where it has to recalculate previous values
        /// </summary>
        [TestMethod]
        public void TestMultipleSetContentsOfCellWithRecalculate()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "5.0");
            spreadsheet.SetContentsOfCell("A2", "6.0");
            spreadsheet.SetContentsOfCell("B1", "=A1+A2");
            spreadsheet.SetContentsOfCell("C1", "=B1+3");
            List<string> result = new List<string>();
            result.Add("A1");
            result.Add("B1");
            result.Add("C1");
            Assert.IsTrue(spreadsheet.SetContentsOfCell("A1", "4.0").SequenceEqual(result));
        }
        /// <summary>
        /// Test where we try to save the content to a file and check if the version matches
        /// </summary>
        [TestMethod]
        public void TestSaveAndGetSavedVersion()
        {
            AbstractSpreadsheet ss = new Spreadsheet(s => true, s => s, "1.0");
            ss.Save("Test1.txt");
            Assert.AreEqual("1.0", new Spreadsheet().GetSavedVersion("Test1.txt"));
        }
        /// <summary>
        /// Test where we try to create a spreadsheet with a different version from the same file
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestSaveDifferentVersion()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "hello");
            s1.Save("save4.txt");
            Assert.IsFalse(s1.Changed);
            AbstractSpreadsheet s2 = new Spreadsheet("save4.txt",s => true, s => s, "hello2");
        }
        /// <summary>
        /// Test where we check if the boolean named changed is false and we set contents of a cell with an empty string
        /// </summary>
        [TestMethod]
        public void TestSetContentsOfCellEmptyString()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "hello");
            Assert.IsFalse(s1.Changed);
            s1.SetContentsOfCell("A1", "");
        }
        /// <summary>
        /// Test where we try to get the saved version of a file that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestInvalidFileFetSavedVersion()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "hello");
            s1.GetSavedVersion("asjdjasds.txt");
        }
        /// <summary>
        /// Test where we try to save a file with null as a name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestSaveNullFileName ()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "");
            s1.Save(null);
        }
        /// <summary>
        /// Test where we try to save a file with an empty name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestSaveEmptyFileName()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "");
            s1.Save("");
        }
        /// <summary>
        /// Test where we try to the get value of a null cell
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestNullGetCellValue()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "");
            s1.GetCellValue(null);
        }
        /// <summary>
        /// Test where we try to get the value of a cell that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidGetCellValue()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "");
            s1.GetCellValue("0834509430");
        }
        /// <summary>
        /// Test where we try to save a json file with a formula
        /// </summary>
        [TestMethod]
        public void TestSaveWithFormula()
        {
            AbstractSpreadsheet s1 = new Spreadsheet(s => true, s => s, "");
            s1.SetContentsOfCell("A1", "Hello");
            s1.SetContentsOfCell("A2", "5.0");
            s1.SetContentsOfCell("A3", "=A2");
            s1.Save("A1Hello.txt");
        }
    }
}
