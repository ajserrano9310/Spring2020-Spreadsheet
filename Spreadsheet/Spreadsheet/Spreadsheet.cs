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
/// This file creates a Spreadsheet using the Formula and DependendcyGraph Library
/// </summary>
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        private DependencyGraph dependencyGraph;
        private Dictionary<string, Cell> cells;
        public Spreadsheet()
        {
            dependencyGraph = new DependencyGraph();
            cells = new Dictionary<string, Cell>();
        }
        public override object GetCellContents(string name)
        {
            if(name is null)
            {
                throw new InvalidNameException();
            }
            if (cells.ContainsKey(name))
            {
                return cells[name].cellContent;
            }
            return "";
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            List<string> nonEmptyCells = new List<string>();
            for(int i = 0; i < cells.Count; i++)
            {
                nonEmptyCells.Add(cells.ElementAt(i).Key);
            }
            return nonEmptyCells;
    }

        public override ISet<string> SetCellContents(string name, double number)
        {
            if (name is null)
            {
                throw new InvalidNameException();
            }
            HashSet<string> test = new HashSet<string>();
            Cell newNumber = new Cell(number);
            if (cells.ContainsKey(name))
            {
                cells[name] = newNumber;
            }
            else
            {
                cells.Add(name, newNumber);
            }
            dependencyGraph.ReplaceDependees(name, test);
            test = new HashSet<string>(GetCellsToRecalculate(name));
            return test;
        }

        public override ISet<string> SetCellContents(string name, string text)
        {
            if (name is null)
            {
                throw new InvalidNameException();
            }
            else if (text is null)
            {
                throw new ArgumentNullException();
            }
            HashSet<string> test = new HashSet<string>();
            Cell newText = new Cell(text);
            if (cells.ContainsKey(name))
            {
                cells[name] = newText;
            }
            else
            {
                cells.Add(name, newText);
            }
            dependencyGraph.ReplaceDependees(name, test);
            test = new HashSet<string>(GetCellsToRecalculate(name));
            return test;
        }

        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            if (name is null)
            {
                throw new InvalidNameException();
            }else if(formula is null)
            {
                throw new ArgumentNullException();
            }
            dependencyGraph.ReplaceDependees(name, formula.GetVariables());
            HashSet<string> newDependees = new HashSet<string>(GetCellsToRecalculate(name));
            Cell newFormula = new Cell(formula);
            if (cells.ContainsKey(name))
            {
                cells[name] = newFormula;
            }
            else
            {
                cells.Add(name, newFormula);
            }
            return newDependees;
        }

        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if(name is null)
            {
                throw new ArgumentNullException();
            }
            return dependencyGraph.GetDependents(name);
        }
        private class Cell
        {
            public Object cellContent;
            public Object getContent()
            {
                return this.cellContent;
            }
            public Cell(Object content)
            {
                cellContent = content;
            }
        }
    }
}
