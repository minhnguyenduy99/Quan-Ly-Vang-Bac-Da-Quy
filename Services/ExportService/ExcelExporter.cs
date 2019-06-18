using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExportService
{
    public class ExcelExporter
    {
        private SpreadsheetDocument spreadSheetDocument;
        private string fileName;

        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        public ExcelExporter()
        {

        }
        public void Export()
        {
            if (!File.Exists(FileName))
                throw new FileNotFoundException("File not found", FileName);
            File.Create(FileName);
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(FileName, true))
            {
                // Add a blank worksheet
                WorksheetPart newWorksheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet(new SheetData());

                // Create a Sheets object in the Workbook.  
                Sheets sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = document.WorkbookPart.GetIdOfPart(newWorksheetPart);

                // Create a unique ID for the new worksheet.  
                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Count() > 0)
                {
                    sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                // Give the new worksheet a name.  
                string sheetName = "mySheet" + sheetId;

                // Append the new worksheet and associate it with the workbook.  
                Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
                sheets.Append(sheet);
            }
        }
    }
}
