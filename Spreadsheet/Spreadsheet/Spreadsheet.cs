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
/// This file creates a Spreadsheet cell using the Formula and DependencyGraph Library.
/// </summary>
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
namespace SS
{
    /// <summary>
    /// Method implementations from the AbstractSpreadsheet class which uses the DependencyGraph and Formula Libraries.
    /// It creates a Spreadsheet Cell with some text, a number or a Formula as the content.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // Create variables to initialize later
        private DependencyGraph dependencyGraph;
        private Dictionary<string, Cell> cells;
        private bool changed;
        public override bool Changed { get { return changed; } protected set { changed = value; } }
        /// <summary>
        /// Constructor for a default Spreadsheet
        /// </summary>
        public Spreadsheet() : base(f => true, f => f, "default")
        {
            // Initialize all variables
            dependencyGraph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
            changed = false;
        }
        /// <summary>
        /// Constructor for a Spreadsheet constructor with isValid, normalize and version
        /// </summary>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            // Initialize all variables
            dependencyGraph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
            changed = false;
        }
        /// <summary>
        /// Constructor for a Spreadsheet constructor with pathToFile, isValid, normalize and version
        /// </summary>
        public Spreadsheet(string pathToFile, Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            // Initialize all variables
            dependencyGraph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
            changed = false;
            // If the version does not match throw an error
            if (!GetSavedVersion(pathToFile).Equals(version))
            {
                throw new SpreadsheetReadWriteException("The version of the file does not match.");
            }
        }
        public override object GetCellContents(string name)
        {
            // name cant be null
            if (name is null)
            {
                throw new InvalidNameException();
                // name needs to have a valid format
            }
            else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
            // If the cell exists and has content, return the content
            else if (cells.ContainsKey(name))
            {
                return cells[name].cellContent;
            }
            // Otherwise return an empty string
            return "";
        }
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            // Create list to store non empty cells
            List<string> nonEmptyCells = new List<string>();
            // Go trough each key
            for (int i = 0; i < cells.Count; i++)
            {
                // Get the key and add it to our list
                nonEmptyCells.Add(cells.ElementAt(i).Key);
            }
            // Return the list with non empty cells
            return nonEmptyCells;
        }
        protected override IList<string> SetCellContents(string name, double number)
        {
            // Create List for later
            List<string> cellsToRecalculate = new List<string>();
            // Create cell with the number
            Cell newNumber = new Cell(number);
            // If it exists we just replace it
            if (cells.ContainsKey(name))
            {
                cells[name] = newNumber;
            }
            // Otherwise we add the new cell
            else
            {
                cells.Add(name, newNumber);
            }
            // We have to replace cells linked to that cell
            dependencyGraph.ReplaceDependees(name, cellsToRecalculate);
            // We also have to recalculate them
            cellsToRecalculate = new List<string>(GetCellsToRecalculate(name));
            // Finally we return that list
            return cellsToRecalculate;
        }
        protected override IList<string> SetCellContents(string name, string text)
        {
            // Create List for later
            List<string> cellsToRecalculate = new List<string>();
            // Create cell with the number
            Cell newText = new Cell(text);
            // If it exists we just replace it
            if (cells.ContainsKey(name))
            {
                cells[name] = newText;
            }
            // Otherwise we add the new cell
            else
            {
                cells.Add(name, newText);
            }
            // We have to replace cells linked to that cell
            dependencyGraph.ReplaceDependees(name, cellsToRecalculate);
            // We also have to recalculate them
            cellsToRecalculate = new List<string>(GetCellsToRecalculate(name));
            // Finally we return that list
            return cellsToRecalculate;
        }
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            // Create List for later
            List<string> newDependees;
            // Call Replace dependees with formula variables
            dependencyGraph.ReplaceDependees(name, formula.GetVariables());
            // Try to recalculate cells
            try
            {
                newDependees = new List<string>(GetCellsToRecalculate(name));
            }
            // Throw exception if it finds a CircularException
            catch (Exception)
            {
                throw new CircularException();
            }
            // Create a cell with the formula
            Cell newFormula = new Cell(formula, lookup);
            // If the cell already exists just replace the content
            if (cells.ContainsKey(name))
            {
                cells[name] = newFormula;
            }
            // Otherwise add the formula
            else
            {
                cells.Add(name, newFormula);
            }
            // Return the List with new dependees
            return newDependees;
        }
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            // Just return the list of dependents from name
            return dependencyGraph.GetDependents(name);
        }
        /// <summary>
        /// Class that creates a Cell with some data as content
        /// </summary>
        private class Cell
        {
            /// <summary>
            /// Stores the content of the Cell
            /// </summary>
            public Object cellContent;
            public Object cellValue;
            public Boolean isFormula;
            /// <summary>
            /// Constructor for Cell with a number as data
            /// </summary>
            /// <param name="content">The value of the Cell</param>
            public Cell(double content)
            {
                cellContent = content;
                cellValue = cellContent;
                isFormula = false;
            }
            /// <summary>
            /// Constructor for Cell with a string as data
            /// </summary>
            /// <param name="content">The value of the Cell</param>
            public Cell(string content)
            {
                cellContent = content;
                cellValue = cellContent;
                isFormula = false;
            }
            /// <summary>
            /// Constructor for Cell with some content as data
            /// </summary>
            /// <param name="content">The value of the Cell</param>
            /// <param name="lookup">Dictionary to get values</param>
            public Cell(Formula content, Func<string, double> lookup)
            {
                cellContent = content;
                isFormula = true;
                cellValue = content.Evaluate(lookup);
            }
        }
        /// <summary>
        /// Checks if the format matches the string
        /// </summary>
        /// <param name="name">the content to check</param>
        /// <returns>true if it matches the format and false otherwise</returns>
        private Boolean formatValidator(string name)
        {
            // Create Regex with desired format to check
            Regex nameFormat = new Regex("^[A-Za-z]+[0-9]");
            // Check if name has the Regex format
            if (nameFormat.IsMatch(name))
            {
                return true;
            }
            return false;
        }

        public override IList<string> SetContentsOfCell(string name, string content)
        {
            List<string> cellsToRecalculate = new List<string>();
            if (content is null)
            {
                throw new ArgumentNullException();
            }
            else if (name is null)
            {
                throw new InvalidNameException();
            }
            else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
            // Case where the content is empty
            if (content.Length == 0)
            {
                cellsToRecalculate = new List<string>(SetCellContents(name, content));
            }
            else
            {
                // Case where the content is a number
                if (Double.TryParse(content, out double number))
                {
                    cellsToRecalculate = new List<string>(SetCellContents(name, number));
                }
                // Case where the content is a formula
                else if (content[0].Equals('='))
                {
                    cellsToRecalculate = new List<string>(SetCellContents(name, new Formula(content.Substring(1, content.Length-1), Normalize, IsValid)));
                }
                // Case where the content is text
                else
                {
                    cellsToRecalculate = new List<string>(SetCellContents(name, content));
                }
            }
            // Recalculate every Formula
            foreach (string s in cellsToRecalculate)
            {
                Cell cellVal;
                if (cells.TryGetValue(s, out cellVal))
                {
                    // If its a Formula recalculate the value
                    if (cellVal.isFormula)
                    {
                        Formula f = (Formula)cellVal.cellContent;
                        // Call evaluate to get new value
                        cellVal.cellValue = f.Evaluate(lookup);
                    }
                }
            }
            // Set changed to tru
            changed = true;
            return cellsToRecalculate;
        }

        public override string GetSavedVersion(string filename)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "spreadsheet":
                                    return reader["version"];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            throw new SpreadsheetReadWriteException("Version not found");
        }

        public override void Save(string filename)
        {
            if (filename is null)
            {
                throw new SpreadsheetReadWriteException("The name of the file cannot be null");
            }
            else if (filename.Equals(""))
            {
                throw new SpreadsheetReadWriteException("The name of the file cannot be empty");
            }
            try
            {
                // XmlWriter settings
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    // Start adding the content
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", null, Version);
                    // Add every cell
                    foreach (string cell in cells.Keys)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", cell);

                        string cellString;
                        if (cells[cell].cellContent is Formula)
                        {
                            cellString = "=" + cells[cell].cellContent;
                        }
                        else
                        {
                            cellString = cells[cell].cellContent.ToString();
                        }

                        writer.WriteElementString("contents", cellString);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.ToString());
            }
            // Set changed to false
            changed = false;
        }
        public override object GetCellValue(string name)
        {
            Cell cell;
            if (name is null)
            {
                throw new InvalidNameException();
            }
            else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
            // Try to get the value and return it
            else if (cells.TryGetValue(name, out cell))
            {
                return cell.cellValue;
            }
            return "";
        }
        /// <summary>
        /// Private method to try and get the value from the cells dictionary
        /// </summary>
        /// <param name="s">the name of the cell to search</param>
        /// <returns>the value of the cell</returns>
        private double lookup(string s)
        {
            // Try to get the value
            return (double)(GetCellValue(s));
            throw new ArgumentException();
        }
    }
}