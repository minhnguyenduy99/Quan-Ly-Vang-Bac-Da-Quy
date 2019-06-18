using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExportService
{
    public class ExportFile
    {
        protected string title;
        protected Table table;
        protected Dictionary<Cell, string> contents;

        public Dictionary<Cell, string> Contents
        {
            get => contents;
        }

        public ExportFile()
        {
            table = new Table();
        }


        public void AddContent(Cell cellPosition, string content)
        {
            Contents.Add(cellPosition, content);
        }

        public void AddTable(Cell cellPosition, Table table)
        {

        }


        
        public void AddColumn(Cell cellPosition, TableColumn column)
        {
        }


        
    }
}
