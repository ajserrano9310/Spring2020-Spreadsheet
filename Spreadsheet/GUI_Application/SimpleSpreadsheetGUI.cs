﻿/// <summary> 
/// AbstractSpreadsheet
/// 
///   The SpreadsheetGUI containing cells was originally written by Joe Zachary and H. James de St. Germain. 
///   The current SpreadsheetGUI was used by Alejandro Rubio and Alejandro Serrano.
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
/// This file creates a Spreadsheet cell using the Formula and DependencyGraph Library.
/// </summary>
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SpreadsheetGUITests")]
namespace SpreadsheetGrid_Core
{
    public partial class SimpleSpreadsheetGUI : Form
    {
        // X coordinate for setting horizontal value
        internal int X;
        // Y coordinate for setting vertical value
        internal int Y;
        // textbox that will hold content of cell
        internal TextBox box;
        // internal representation of spreadsheet
        internal Spreadsheet s;
        // Dialogs for saving and loading files
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        internal System.Windows.Forms.SaveFileDialog saveFileDialog1;
        // Filepath for saving
        internal string FilePath;
        // Stacks for the Undo implementation
        internal Stack<string> cellName;
        internal Stack<string> cellContent;
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
        internal void DisplaySelection(SpreadsheetGridWidget ss)
        {
            ss.GetValue(X, Y, out string val);
            
                if (!val.Equals("")){
                    
                        evaluate_Button_Click(null, null);
                }
            int row, col;
            string value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);
            X = col;
            Y = row;
            int newY = Y + 1;
            // generates the string cell name in the form A1
            String cell = lookupCordToVar(X) + newY;
            if (s.GetCellContents(cell) is Formula)
            {
                    evaluate_textbox.Text = "=" + s.GetCellContents(cell).ToString();
            }
            else
            {
                evaluate_textbox.Text = s.GetCellContents(cell).ToString();
            }
            cellTextBox.Text = cell;
            valTextBox.Text = s.GetCellValue(cell).ToString();

        }
        // Deals with the New menu
        internal void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tell the application context to run the form on the same
            // thread as the other forms.
            Spreadsheet_Window.getAppContext().RunForm(new SimpleSpreadsheetGUI());
        }
        // Deals with the Close menu
        internal void CloseToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Failed to save");
                }
            }
        }
        /// <summary>
        /// Evaluates contents of the textbox
        /// </summary>
        /// <param name="sender"> not used </param>
        /// <param name="e"> not used </param>
        internal void evaluate_Button_Click(object sender, EventArgs e)
        {
            if (!(box is null))
            {
                int newY = Y + 1;
                String cell = lookupCordToVar(X) + newY;
             
                try
                {
                    grid_widget.GetValue(X, Y, out string val);
                    Object f = s.GetCellContents(cell);
                    // we check if its the type of formula to push in the stack as
                    // "=A1+A2"
                    if (f is Formula) {
                        cellContent.Push("="+f.ToString());
                    }
                    else
                    {
                        cellContent.Push(f.ToString());
                    }
                    
                    s.SetContentsOfCell(cell, box.Text);
                    cellName.Push(cell);
                    // enables the undo botton after the first modification
                    undo_Button.Enabled = true;

                        grid_widget.SetValue(X, Y, s.GetCellValue(cell).ToString());
                    bg_worker.RunWorkerAsync();
                }
                // catching possible exceptions 
                catch (FormulaFormatException)
                {

                    string message = "The Formula you entered is not valid";
                    string title = "Invalid Formula";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                }
                catch(CircularException)
                {
                    string message = "Circular dependency detected";
                    string title = "Circular Exception";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Textbox to write the contents of a cell 
        /// </summary>
        /// <param name="sender"> the textbox </param>
        /// <param name="e">not used</param>
        internal void evaluate_textbox_TextChanged(object sender, EventArgs e)
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
        internal string lookupCordToVar(int num)
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
        internal int lookupVarToCord(char letter)
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
        internal void grid_widget_KeyDown(object sender, KeyEventArgs e)
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
        /// <summary>
        /// Save tool strip menu that will allow user to keep
        /// saving on the same file or create a new one 
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        internal void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // opens a save file dialog that allows the user to save a file with 
                // personalized name 
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
        /// <summary>
        /// Save click that saves on the same file
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">mouse click</param>
        internal void save_Click(object sender, System.EventArgs e)
        {
            s.Save(FilePath);
        }
        /// <summary>
        /// Load button click that loads a file and overrides
        /// the current one. 
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        internal void load_Button_Click(object sender, System.EventArgs e)
        {
            try
            {
                // opens a file dialog box that will allow the user to load a new file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "sprd files (*.sprd)|*.sprd|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                string filepath = openFileDialog1.FileName;
                if (filepath.Length != 0)
                {
                    s = new Spreadsheet(filepath, f => true, f => f.ToUpper(), "six");
                    List<string> nonEmptyCells = new List<string>(s.GetNamesOfAllNonemptyCells());
                    ClearCells();
                    // recalculates the cells after loaded file
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
                    load_Button_Click(null, null);
                }
            }
        }
        /// <summary>
        /// When the sample_textbox is active
        /// the keybindings are deactivated. 
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">entering the textbox</param>
        internal void evaluate_textbox_Enter(object sender, EventArgs e)
        {
            this.KeyPreview = false;
        }
        /// <summary>
        /// When the sample_textbox is not active
        /// keybindings are activated again.
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">leaving texbox</param>
        internal void evaluate_textbox_Leave(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
        /// <summary>
        /// Pressing enter will move the cell selection down one. 
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">key pressed</param>
        internal void evaluate_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.grid_widget.SetSelection(X, Y + 1);
            }
        }
        /// <summary>
        /// Recalculates the values of the cells after
        /// some modification has been made. 
        /// </summary>
        internal void recalculateText()
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
        /// <summary>
        /// Event for the Undo button when it is clicked
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        internal void undo_Button_Click(object sender, EventArgs e)
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
            // background worker recalculates cells 
            bg_worker.RunWorkerAsync();
        }
        /// <summary>
        /// Action of the BackgroundWorker
        /// Recalculates all the cells
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        internal void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            recalculateText();
        }
        /// <summary>
        /// Clears all the content of the spreadsheet
        /// </summary>
        internal void ClearCells()
        {
            for(int i = 0; i < 27; i++)
            {
                for(int j = 0; j < 100; j++)
                {
                    grid_widget.SetValue(i, j, "");
                }
            }
        }
        /// <summary>
        /// Gives a message box with the information on how to use 
        /// the Spreadsheet. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome TA(s) to A6 Spreedsheet." + System.Environment.NewLine
                + "This messagebox will help you understand a little bit how our Spreadsheet works." 
                +System.Environment.NewLine 
                +System.Environment.NewLine + 
                "    1) For moving between cells, we set up WASD keys for the purposes of better navigation." 
                + System.Environment.NewLine +
                "    2) Pressing enter will allow to drop one value down on the Y coordinate." 
                +System.Environment.NewLine +
                "    3) Pressing enter or the Evaluate button will evaluate the corresponding cell" 
                +System.Environment.NewLine +
                "    4) Our undo button works similar to CTRL+Z, except not as sophisticated. It erases all contents" +
                "of the cell and returns it to its previous content." 
                +System.Environment.NewLine+
                "    5) On the file menu, there are three important additions:"+
                System.Environment.NewLine+
                "        a) Load: which will load the file."+
                System.Environment.NewLine+
                "        b) Save: which saves the file in the current filepath." +
                System.Environment.NewLine+
                "        c) Save as: allows user to save in a new file." +
                System.Environment.NewLine+
                "    6) Overall, better than Excel", "Help", MessageBoxButtons.OK);
        }
        /// <summary>
        /// This is a helper method for SpreadsheetGUITests since we it does not have access to grid_widget
        /// Returns the value at position x,y
        /// </summary>
        /// <param name="x">The coordinate x</param>
        /// <param name="y">The coordinate y</param>
        /// <returns>The value at coordinates x,y</returns>
        internal string getValue(int x, int y)
        {
            grid_widget.GetValue(x, y, out string val);
            return val;
        }
    }
}
