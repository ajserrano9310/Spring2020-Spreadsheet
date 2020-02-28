using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGrid_Core;
using SS;
using System.IO;
using System.Windows.Forms;

namespace SpreadsheetGUITests
{
    [TestClass]
    public class SpreadsheetGUITests
    {
        [TestMethod]
        public void TestSave()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            form.FilePath = "test.sprd";
            form.save_Click(null,null);
            Spreadsheet s = new Spreadsheet("test.sprd",f=>true,f=>f.ToUpper(),"six");
            Assert.IsTrue(File.Exists("test.sprd"));
        }
        [TestMethod]
        public void TestLookupCordToVar()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            Assert.AreEqual(form.lookupCordToVar(0), "A");
        }
        [TestMethod]
        public void TestLookupVarToCord()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            Assert.AreEqual(form.lookupVarToCord('A'), 0);
        }
        [TestMethod]
        public void TestKeyBinding()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.S));
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.D));
            Assert.AreEqual(form.X, 1);
            Assert.AreEqual(form.Y, 1);
        }
        [TestMethod]
        public void TestSetValue()
        {
            SimpleSpreadsheetGUI form = new SimpleSpreadsheetGUI();
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.S));
            form.grid_widget_KeyDown(null, new KeyEventArgs(Keys.D));
            TextBox txtBox1 = new TextBox();
            txtBox1.Text = "Hello";
            form.evaluate_textbox_TextChanged(txtBox1, null);
            Assert.AreEqual("Hello", form.getValue(1, 1));
        }
    }
}
