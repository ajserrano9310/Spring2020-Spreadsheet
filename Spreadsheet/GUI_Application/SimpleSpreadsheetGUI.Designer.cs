﻿/// <summary>
///   Original Author: Joe Zachary
///   Further Authors: H. James de St. Germain
///   
///   Dates          : 2012-ish - Original 
///                    2020     - Updated for use with ASP Core
///                    
///   This code represents a Windows Form element for a Spreadsheet
///   
///   This code is the "auto-generated" portion of the SimpleSpreadsheetGUI.
///   See the SimpleSpreadsheetGUI.cs for "hand-written" code.
///  
/// </summary>

using SpreadsheetGrid_Core;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpreadsheetGrid_Core
{
    partial class SimpleSpreadsheetGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainControlArea = new System.Windows.Forms.FlowLayoutPanel();
            this.grid_widget = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluate_button = new System.Windows.Forms.Button();
            this.evaluate_textbox = new System.Windows.Forms.TextBox();
            this.undo_Button = new System.Windows.Forms.Button();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellTextBox = new System.Windows.Forms.TextBox();
            this.valTextBox = new System.Windows.Forms.TextBox();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bg_worker = new BackgroundWorker();
            this.menuStrip.SuspendLayout();
            this.MainControlArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // background worker
            bg_worker.DoWork += bgw_DoWork;

            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem, this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.load_Button_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.save_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 

            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // CloseToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // MainControlArea
            // 
            this.MainControlArea.AutoSize = true;
            this.MainControlArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainControlArea.BackColor = System.Drawing.Color.GhostWhite;
            this.MainControlArea.Controls.Add(this.evaluate_textbox);
            this.MainControlArea.Controls.Add(this.evaluate_button);
            this.MainControlArea.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainControlArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainControlArea.Location = new System.Drawing.Point(3, 3);
            this.MainControlArea.MinimumSize = new System.Drawing.Size(100, 100);
            this.MainControlArea.Name = "MainControlArea";
            this.MainControlArea.Size = new System.Drawing.Size(578, 100);
            this.MainControlArea.TabIndex = 4;
            this.MainControlArea.Controls.Add(this.cellTextBox);
            this.MainControlArea.Controls.Add(this.valTextBox);
            this.MainControlArea.Controls.Add(this.undo_Button);
            // 
            // undo_Button
            // 
            this.undo_Button.Enabled = false;
            this.undo_Button.Location = new System.Drawing.Point(535, 3);
            this.undo_Button.Name = "undo_Button";
            this.undo_Button.Size = new System.Drawing.Size(75, 23);
            this.undo_Button.TabIndex = 3;
            this.undo_Button.Text = "Undo";
            this.undo_Button.UseVisualStyleBackColor = true;
            this.undo_Button.Click += new System.EventHandler(this.undo_Button_Click);
            // 
            // grid_widget
            // 
            this.grid_widget.AutoSize = true;
            this.grid_widget.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grid_widget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_widget.Location = new System.Drawing.Point(3, 103);
            this.grid_widget.MaximumSize = new System.Drawing.Size(2100, 2000);
            this.grid_widget.Name = "grid_widget";
            this.grid_widget.Size = new System.Drawing.Size(578, 231);
            this.grid_widget.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.MainControlArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grid_widget, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 337);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // evaluate_button
            // 
            this.evaluate_button.Location = new System.Drawing.Point(168, 6);
            this.evaluate_button.Name = "evaluate_button";
            this.evaluate_button.Size = new System.Drawing.Size(75, 23);
            this.evaluate_button.TabIndex = 0;
            this.evaluate_button.Text = "Evaluate";
            this.evaluate_button.UseVisualStyleBackColor = true;
            this.evaluate_button.Click += new System.EventHandler(this.evaluate_Button_Click);
            // 
            // evaluate_textbox
            // 
            this.evaluate_textbox.Location = new System.Drawing.Point(6, 6);
            this.evaluate_textbox.Name = "evaluate_textbox";
            this.evaluate_textbox.Size = new System.Drawing.Size(100, 20);
            this.evaluate_textbox.TabIndex = 2;
            this.evaluate_textbox.TextChanged += new System.EventHandler(this.evaluate_textbox_TextChanged);
            this.evaluate_textbox.Enter += new System.EventHandler(this.evaluate_textbox_Enter);
            this.evaluate_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.evaluate_textbox_KeyDown);
            this.evaluate_textbox.Leave += new System.EventHandler(this.evaluate_textbox_Leave);
            // 
            // cellTextBox
            // 
            this.cellTextBox.Location = new System.Drawing.Point(400, 3);
            this.cellTextBox.Name = "cellTextBox";
            this.cellTextBox.Size = new System.Drawing.Size(100, 26);
            this.cellTextBox.TabIndex = 3;
            this.cellTextBox.ReadOnly = true;
            // 
            // valTextBox
            // 
            this.valTextBox.Location = new System.Drawing.Point(506, 3);
            this.valTextBox.Name = "valTextBox";
            this.valTextBox.Size = new System.Drawing.Size(100, 26);
            this.valTextBox.TabIndex = 4;
            this.valTextBox.ReadOnly = true;
            // 
            // SimpleSpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "SimpleSpreadsheetGUI";
            this.Text = "Assignment 6 - SpreadsheetGUI";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_widget_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.MainControlArea.ResumeLayout(false);
            this.MainControlArea.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);

        }

        #endregion


        private SpreadsheetGridWidget grid_widget;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

        private FlowLayoutPanel MainControlArea;
        private TableLayoutPanel tableLayoutPanel1;
        private Button evaluate_button;
        private TextBox evaluate_textbox;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TextBox cellTextBox;
        private TextBox valTextBox;
        private Button undo_Button;
        private BackgroundWorker bg_worker;
    }
}
