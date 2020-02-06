using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreadsheet
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        public override object GetCellContents(string name)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            throw new NotImplementedException();
        }

        public override ISet<string> SetCellContents(string name, double number)
        {
            throw new NotImplementedException();
        }

        public override ISet<string> SetCellContents(string name, string text)
        {
            throw new NotImplementedException();
        }

        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            throw new NotImplementedException();
        }
        private class Cell
        {
            private Cell content;
            private Cell getCellContent<T>()
            {
                return this.content;
            }
            private void setCellContent<T>(<T> content)
            {
                theCell.content = content;
            }
        }

    }
}
