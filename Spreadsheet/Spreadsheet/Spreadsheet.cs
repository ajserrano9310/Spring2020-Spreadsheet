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
/// This file creates a Spreadsheet cell using the Formula and DependencyGraph Library.
/// </summary>
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
namespace SS
{
    /// <summary>
    /// Method implementations from the AbstractSpreadsheet class which uses the DependencyGraph and Formula Libraries.
    /// It creates a Spreadsheet Cell with some text, a number or a Formula as the content.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // Create variable to initialize later
        private DependencyGraph dependencyGraph;
        private Dictionary<string, Cell> cells;
        public Spreadsheet()
        {
            // Initialize all variables
            dependencyGraph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
        }
        public override object GetCellContents(string name)
        {
            // name cant be null
            if (name is null)
            {
                throw new InvalidNameException();
                // name needs to have a valid format
            } else if (!formatValidator(name)){
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
            for(int i = 0; i < cells.Count; i++)
            {
                // Get the key and add it to our list
                nonEmptyCells.Add(cells.ElementAt(i).Key);
            }
            // Return the list with non empty cells
            return nonEmptyCells;
    }
        public override ISet<string> SetCellContents(string name, double number)
        {
            // name cant be null
            if (name is null)
            {
                throw new InvalidNameException();
                // name needs to have a valid format
            } else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
            // Create HashSet for later
            HashSet<string> cellsToRecalculate = new HashSet<string>();
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
            cellsToRecalculate = new HashSet<string>(GetCellsToRecalculate(name));
            // Finally we return that list
            return cellsToRecalculate;
        }
        public override ISet<string> SetCellContents(string name, string text)
        {
            // name cant be null
            if (name is null)
            {
                throw new InvalidNameException();
            }
            // text cant be null
            else if (text is null)
            {
                throw new ArgumentNullException();
            }
            // name needs to have a valid format
            else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
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
            // Create HashSet for later
            HashSet<string> cellsToRecalculate = new HashSet<string>();
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
            cellsToRecalculate = new HashSet<string>(GetCellsToRecalculate(name));
            // Finally we return that list
            return cellsToRecalculate;
        }
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            // name cant be null
            if (name is null)
            {
                throw new InvalidNameException();
                // formula cant be null
            }else if(formula is null)
            {
                throw new ArgumentNullException();
            }
            // name needs to have a valid format
            else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
            // Create HashSet for later
            HashSet<string> newDependees;
            // Call Replace dependees with formula variables
            dependencyGraph.ReplaceDependees(name, formula.GetVariables());
            // Try to recalculate cells
            try
            {
                newDependees = new HashSet<string>(GetCellsToRecalculate(name));
            }
            // Throw exception if it finds a CircularException
            catch (Exception)
            {
                throw new CircularException();
            }
            // Create a cell with the formula
            Cell newFormula = new Cell(formula);
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
            // Return the hashset with new dependees
            return newDependees;
        }
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            // name cant be null
            if(name is null)
            {
                throw new ArgumentNullException();
                // name needs to have a valid format
            } else if (!formatValidator(name))
            {
                throw new InvalidNameException();
            }
            // Finally return the list of dependents from name
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
            /// <summary>
            /// Constructor for Cell with some content as data
            /// </summary>
            /// <param name="content">The value of the Cell</param>
            public Cell(Object content)
            {
                cellContent = content;
            }
        }
        private Boolean formatValidator(string name)
        {
            // Create Regex with desired format to check
            Regex nameFormat = new Regex("^[_A-Za-z]+[_A-Za-z0-9]");
            // Check if name has the Regex format
            if (nameFormat.IsMatch(name))
            {
                return true;
            }
            return false;
        }
    }
}
