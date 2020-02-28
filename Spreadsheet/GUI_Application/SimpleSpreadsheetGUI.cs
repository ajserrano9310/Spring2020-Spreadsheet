﻿/// <summary>
///   Original Author: Joe Zachary
///   Further Authors: H. James de St. Germain
///   
///   Dates          : 2012-ish - Original 
///                    2020     - Updated for use with ASP Core
///                    
///   This code represents a Windows Form element for a Spreadsheet. It includes
///   a Menu Bar with two operations (close/new) as well as the GRID of the spreadsheet.
///   The GRID is a separate class found in SpreadsheetGridWidget
///   
///   This code represents manual elements added to the GUI as well as the ability
///   to show a pop up of information, and the event handlers for a New window and to Close the window.
///
///   See the SimpleSpreadsheetGUIExample.Designer.cs for "auto-generated" code.
///   
///   This code relies on the SpreadsheetPanel "widget"
///  
/// </summary>
using SpreadsheetGrid_Core;
using System.Collections.Generic;
using SpreadsheetUtilities;
using SS;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace SpreadsheetGrid_Core
{
    public partial class SimpleSpreadsheetGUI : Form
    {
        private int X;
        private int Y;
        private TextBox box;
        private Spreadsheet s;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private string FilePath;
        private Stack<string> cellName;
        private Stack<string> cellContent;
        public SimpleSpreadsheetGUI()
        {
            cellName = new Stack<string>();
            cellContent = new Stack<string>();
            // Allows to use the keybinding
            this.KeyPreview = true;
            this.grid_widget = new SpreadsheetGridWidget();
            // Call the AutoGenerated code
            InitializeComponent();
            // Add event handler and select a start cell
            grid_widget.SelectionChanged += DisplaySelection;
            grid_widget.SetSelection(0, 0, false);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            s = new Spreadsheet(f => true, f => f.ToUpper(), "six");
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        }
        /// <summary>
        /// Given a spreadsheet, find the current selected cell and
        /// create a popup that contains the information from that cell
        /// </summary>
        /// <param name="ss"></param>
        private void DisplaySelection(SpreadsheetGridWidget ss)
        {
            ss.GetValue(X, Y, out string val);
            
                if (!val.Equals("")){
                    
                        sample_button_Click(null, null);
                }
            int row, col;
            string value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);
            X = col;
            Y = row;
            int newY = Y + 1;
            String cell = lookup(X) + newY;
            if (s.GetCellContents(cell) is Formula)
            {
                    evaluate_textbox.Text = "=" + s.GetCellContents(cell).ToString();
            }
            else
            {
                evaluate_textbox.Text = s.GetCellContents(cell).ToString();
            }
            cellTextBox.Text = cell;
            this.cellTextBox.ReadOnly = true;
            valTextBox.Text = s.GetCellValue(cell).ToString();
            this.valTextBox.ReadOnly = true;
        }
        // Deals with the New menu
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tell the application context to run the form on the same
            // thread as the other forms.
            Spreadsheet_Window.getAppContext().RunForm(new SimpleSpreadsheetGUI());
        }
        // Deals with the Close menu
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to save?";
            var result = MessageBox.Show(message, "Closing Spreadsheet",
                                 MessageBoxButtons.YesNo);
            if(result == DialogResult.No)
            {
                Close();    
            }
            else
            {
                try
                {
                    saveToolStripMenuItem_Click(null, null);
                    Close();
                }
                catch (SpreadsheetReadWriteException)
                {
                    MessageBox.Show("Task failed succesfully");
                }
            }
        }
        /// <summary>
        /// Example of how to use a button
        /// </summary>
        /// <param name="sender"> not used </param>
        /// <param name="e"> not used </param>
        private void sample_button_Click(object sender, EventArgs e)
        {
            if (!(box is null))
            {
                int newY = Y + 1;
                String cell = lookup(X) + newY;
                grid_widget.GetValue(X, Y, out string val);
                    cellContent.Push(s.GetCellContents(cell).ToString());
                    s.SetContentsOfCell(cell, box.Text);
                    cellName.Push(cell);
                    undo_Button.Enabled = true;
                    if (s.GetCellValue(cell) is FormulaError)
                    {
                        grid_widget.SetValue(X, Y, "Error");
                    }
                    else
                    {
                        grid_widget.SetValue(X, Y, s.GetCellValue(cell).ToString());
                    }
                bg_worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// Checkbox handler
        /// </summary>
        /// <param name="sender"> the checkbox (note the casting operator as)</param>
        /// <param name="e">not used</param>
        private void sample_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                grid_widget.SetValue(1, 1, "checked");
            }
            else
            {
                grid_widget.SetValue(1, 1, "not checked");
            }
        }
        /// <summary>
        /// Textbox handler
        /// </summary>
        /// <param name="sender"> the textbox </param>
        /// <param name="e">not used</param>
        private void sample_textbox_TextChanged(object sender, EventArgs e)
        {
            box = sender as TextBox;
            grid_widget.SetValue(X, Y, box.Text);

        }
        /// <summary>
        /// lookup to change the value of the coordinate
        /// to a cell name
        /// </summary>
        /// <param name="num">X coordinate</param>
        /// <returns>cell name</returns>
        private string lookup(int num)
        {
            num = num + 65;
            char numChar = (char)num;
            return numChar.ToString();
        }
        /// <summary>
        /// Changes the letter to the respective X coordinate
        /// value. 
        /// </summary>
        /// <param name="letter">first letter of cell name</param>
        /// <returns>X coordinate</returns>
        private int lookupVarToCord(char letter)
        {
            int index = char.ToUpper(letter) - 65;
            return index;
        }
        /// <summary>
        /// Key bindings for WASD and enter
        /// WASD moves up, down, left or right
        /// enter goes to the cell underneath
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">key pressed</param>
        private void grid_widget_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                this.grid_widget.SetSelection(X, Y + 1);
            }
            if (e.KeyCode == Keys.D)
            {
                this.grid_widget.SetSelection(X + 1, Y);
            }
            if (e.KeyCode == Keys.A)
            {
                this.grid_widget.SetSelection(X - 1, Y);
            }
            if (e.KeyCode == Keys.W)
            {
                this.grid_widget.SetSelection(X, Y - 1);
            }
            if (e.KeyCode == Keys.Enter)
                this.grid_widget.SetSelection(X, Y + 1);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";
                saveFileDialog1.ShowDialog();
                string filepath = saveFileDialog1.FileName;
                if (filepath.Length != 0)
                {
                    FilePath = filepath;
                    s.Save(FilePath);
                    saveToolStripMenuItem.Enabled = true;
                }
            }
            catch (Exception)
            {
                string message = "Do you want to retry?";
                string title = "Error saving file";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
        }
        private void save_Click(object sender, System.EventArgs e)
        {
            s.Save(FilePath);
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                string filepath = openFileDialog1.FileName;
                if (filepath.Length != 0)
                {
                    s = new Spreadsheet(filepath, f => true, f => f.ToUpper(), "six");
                    List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
                    ClearCells();
                    foreach (string name in nonEmptyCells)
                    {
                        string numS = name.Substring(1, name.Length - 1);
                        int.TryParse(numS, out int a);
                        grid_widget.SetValue(lookupVarToCord(name[0]), a - 1, s.GetCellValue(name).ToString());
                    }
                }
            }catch(Exception)
            {
                string message = "Do you want to retry?";
                string title = "Error loading file";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    button1_Click(null, null);
                }
            }
        }
        private void sample_textbox_Enter(object sender, EventArgs e)
        {
            this.KeyPreview = false;
        }
        private void sample_textbox_Leave(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
        private void sample_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.grid_widget.SetSelection(X, Y + 1);
            }
        }
        /// <summary>
        /// Recalculates the values of the cells
        /// </summary>
        private void recalculateText()
        {
            List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
            foreach (string cell in nonEmptyCells)
            {
                if (s.GetCellContents(cell) is Formula)
                {
                    string numS = cell.Substring(1, cell.Length - 1);
                    int.TryParse(numS, out int a);
                    grid_widget.SetValue(lookupVarToCord(cell[0]), a - 1, s.GetCellValue(cell).ToString());
                }
            }
        }
        private void undo_Button_Click(object sender, EventArgs e)
        {
            string content = cellContent.Pop();
            string name = cellName.Pop();
            s.SetContentsOfCell(name, content);
            string numS = name.Substring(1, name.Length - 1);
            int.TryParse(numS, out int a);
            grid_widget.SetValue(lookupVarToCord(name[0]), a - 1, content);
            if (cellContent.Count == 0)
            {
                undo_Button.Enabled = false;
            }
            bg_worker.RunWorkerAsync();
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            recalculateText();
        }
        private void ClearCells()
        {
            for(int i = 0; i < 27; i++)
            {
                for(int j = 0; j < 100; j++)
                {
                    grid_widget.SetValue(i, j, "");
                }
            }
        }
    }
}
