using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGrid_Core;
using SS;
using System.IO;
using System.Windows.Forms;
/// <summary>
/// Assignment: Assignment Six - Spreadsheet GUI
/// Author:    Alejandro Rubio
/// Partner:   Alejandro Serrano
/// Date:      2/28/2020
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500, Alejandro Rubio and Alejandro Serrano - This work may not be copied for use in Academic Coursework. 
/// 
/// We, Alejandro Rubio and Alejandro Serrano, certify that we wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// This file tests certain methods for the Spreadsheet GUI. 
/// </summary>
namespace SpreadsheetGUITests
{
    [TestClass]
    public class SpreadsheetGUITests
    {
        /// <summary>
        /// test for the Save method that saves the Spreadsheet as a .sprd file
        /// </summary>
        [TestMethod]
        public void TestSave()
        {
            // Create a new SimpleSpreadsheetGUI
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            // Assign the FilePath
            form.FilePath = "test.sprd";
            // Try to save to that FilePath
            form.save_Click(null,null);
            // Check if the version is six
            Spreadsheet s = new Spreadsheet("test.sprd",f=>true,f=>f.ToUpper(),"six");
            // Check if the file exists
            Assert.IsTrue(File.Exists("test.sprd"));
        }
        /// <summary>
        /// Test for the lookupCordToVar method that converts an int to a letter
        /// </summary>
        [TestMethod]
        public void TestLookupCordToVar()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            Assert.AreEqual(form.lookupCordToVar(0), "A");
        }
        /// <summary>
        /// Test for the lookupVarToCord method that converts a char to a number
        /// </summary>
        [TestMethod]
        public void TestLookupVarToCord()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            Assert.AreEqual(form.lookupVarToCord('A'), 0);
        }
        /// <summary>
        /// Test for the Key Binding of the spreadsheet where you navigate trough the spreadsheet with the WASD keys
        /// </summary>
        [TestMethod]
        public void TestKeyBinding()
        {
            // Create a new SimpleSpreadsheetGUI
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            // Go down 1 cell
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.S));
            // Go right 1 cell
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.D));
            // Ensure it went to the right
            Assert.AreEqual(form.X, 1);
            // Ensure it went down
            Assert.AreEqual(form.Y, 1);
        }
        /// <summary>
        /// Test for the evaluate textbox and evaluate button to set values on the spreadsheet
        /// </summary>
        [TestMethod]
        public void TestSetValue()
        {
            // Create a new SimpleSpreadsheetGUI
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            // Go down 1 cell
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.S));
            // Go right 1 cell
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.D));
            // Create a new TextBox
            TextBox txtBox1 = new TextBox();
            // Put some text
            txtBox1.Text = "Hello";
            // Set the contents of the textbox to the spreadsheet
            form.evaluate_textbox_TextChanged(txtBox1, null);
            // Ensure we have the value placed
            Assert.AreEqual("Hello", form.getValue(1, 1));
        }
        /// <summary>
        /// Test for the ClearCells method that erases all the content of the spreadsheet when loading a new file
        /// </summary>
        [TestMethod]
        public void TestClearCells()
        {
            // Create a new SimpleSpreadsheetGUI
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            // Go down 1 cell
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.S));
            // Go right 1 cell
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.D));
            // Create a new TextBox
            TextBox txtBox1 = new TextBox();
            // Put some text
            txtBox1.Text = "Hello";
            // Set the contents of the textbox to the spreadsheet
            form.evaluate_textbox_TextChanged(txtBox1, null);
            // Clear all the cells
            form.ClearCells();
            // Ensure we dont have the value anymore
            Assert.AreEqual("", form.getValue(1, 1));
        }
    }
}
