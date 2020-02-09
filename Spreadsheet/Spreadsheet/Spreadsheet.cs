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
            return cells[name].cellContent;
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
