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
using SS;
using System;
using System.Collections.Generic;
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
    }
}
