using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace UIProject.ServiceProviders
{
    /// <summary>
    /// Provides functionalities for converting from a collection of <see cref="ObservableObject"/> to a table
    /// </summary>
    public class CollectionToTableConverter
    {
        /// <summary>
        /// Converts from a <see cref="IEnumerable{ObservableObject}"/> to a table
        /// </summary>
        /// <param name="objects">The collection of <see cref="ObservableObject"/></param>
        /// <param name="propertyNames">The array of property's names needed to retrieve and displayed on table</param>
        /// <param name="table">The table to converted</param>
        /// <returns>A value indicating if the conversion is successful</returns
        /// <remarks>The table must already had a <see cref="TableRowGroup"/> contains a <see cref="TableRow"/> 
        /// which defines the header</remarks>
        public static bool ConvertToTable(IEnumerable<ObservableObject> objects, string[] propertyNames, Table table)
        {
            if (table == null)
                return false;
            var dataRowGroup = new TableRowGroup();
            foreach(var obj in objects)
            {
                bool convertSuccess = ConvertToTableRow(obj, propertyNames, table.RowGroups[0].Rows[0], out TableRow tableRow);
                if (!convertSuccess)
                    return false;
                dataRowGroup.Rows.Add(tableRow);
            }
            table.RowGroups.Add(dataRowGroup);
            return true;
        }

        public static bool AddRowHeaders(string[] headerNames, Table table)
        {
            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow();
            for (int i = 0; i < headerNames.Length; i++)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run(headerNames[i]))));
            }
            headerGroup.Rows.Add(headerRow);
            table.RowGroups.Add(headerGroup);
            return true;
        }

        public static bool ConvertToTableRow(ObservableObject obj, string[] propertyNames, TableRow headerRow, out TableRow tableRow)
        {
            tableRow = new TableRow();
            for(int i=0;i<propertyNames.Length; i++)
            {
                try
                {
                    object propValue = ObservableObject.GetPropValue(obj, propertyNames[i]);
                    TableCell cell = new TableCell();
                    cell.Blocks.Add(new Paragraph(new Run(propValue.ToString())));
                    ApplyHeaderCellStyleToDataCell(cell, headerRow.Cells[i]);
                    tableRow.Cells.Add(cell);
                }
                catch
                {
                    tableRow = null;
                    return false;
                }
            }

            return true;
        }

        public static void ApplyHeaderCellStyleToDataCell(TableCell dataCell, TableCell headerCell)
        {
            dataCell.Style = headerCell.Style;

            // Apply properties which are not set in the Style 
            dataCell.TextAlignment = headerCell.TextAlignment;
        }
    }
}
