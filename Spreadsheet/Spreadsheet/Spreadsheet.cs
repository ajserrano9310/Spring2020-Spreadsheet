﻿using SpreadsheetUtilities;
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

        public override IList<string> SetCellContents(string name, double number)
        {
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, string text)
        {
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, Formula formula)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            throw new NotImplementedException();
        }
        private class Cell
        {
            private Object cellContent;
            private Object getContent()
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
