using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace MyZilla.UI
{
    class TableRdlGenerator
    {
        private List<string> m_fields;
        private List<string> m_captions;
        private List<int> m_width;

        public List<string> Fields
        {
            get { return m_fields; }
            set { m_fields = value; }
        }

        public List<string> Captions
        {
            get { return m_captions; }
            set { m_captions = value; }
        }

        public List<int> Width
        {
            get { return m_width; }
            set { m_width = value; }
        }


        public Rdl.TableType CreateTable()
        {
            Rdl.TableType table = new Rdl.TableType();
            table.Name = "Table1";
            table.Items = new object[]
                {
                    CreateTableColumns(),
                    CreateHeader(),
                    CreateDetails(),
                };
            table.ItemsElementName = new Rdl.ItemsChoiceType21[]
                {
                    Rdl.ItemsChoiceType21.TableColumns,
                    Rdl.ItemsChoiceType21.Header,
                    Rdl.ItemsChoiceType21.Details,
                };
            return table;
        }

        private Rdl.HeaderType CreateHeader()
        {
            Rdl.HeaderType header = new Rdl.HeaderType();
            header.Items = new object[]
                {
                    CreateHeaderTableRows(),
                };
            header.ItemsElementName = new Rdl.ItemsChoiceType20[]
                {
                    Rdl.ItemsChoiceType20.TableRows,
                };
            return header;
        }

        private Rdl.TableRowsType CreateHeaderTableRows()
        {
            Rdl.TableRowsType headerTableRows = new Rdl.TableRowsType();
            headerTableRows.TableRow = new Rdl.TableRowType[] { CreateHeaderTableRow() };
            return headerTableRows;
        }

        private Rdl.TableRowType CreateHeaderTableRow()
        {
            Rdl.TableRowType headerTableRow = new Rdl.TableRowType();
            headerTableRow.Items = new object[] { CreateHeaderTableCells(), "0.25in" };
            return headerTableRow;
        }

        private Rdl.TableCellsType CreateHeaderTableCells()
        {
            Rdl.TableCellsType headerTableCells = new Rdl.TableCellsType();
            headerTableCells.TableCell = new Rdl.TableCellType[m_fields.Count];
            for (int i = 0; i < m_captions.Count; i++)
            {
                headerTableCells.TableCell[i] = TableRdlGenerator.CreateHeaderTableCell(m_captions[i]);
            }
            return headerTableCells;
        }

        private static Rdl.TableCellType CreateHeaderTableCell(string fieldName)
        {
            Rdl.TableCellType headerTableCell = new Rdl.TableCellType();
            headerTableCell.Items = new object[] { TableRdlGenerator.CreateHeaderTableCellReportItems(fieldName) };
            return headerTableCell;
        }

        private static Rdl.ReportItemsType CreateHeaderTableCellReportItems(string fieldName)
        {
            Rdl.ReportItemsType headerTableCellReportItems = new Rdl.ReportItemsType();
            headerTableCellReportItems.Items = new object[] { TableRdlGenerator.CreateHeaderTableCellTextbox(fieldName) };
            return headerTableCellReportItems;
        }

        private static Rdl.TextboxType CreateHeaderTableCellTextbox(string fieldName)
        {
            Rdl.TextboxType headerTableCellTextbox = new Rdl.TextboxType();
            headerTableCellTextbox.Name = fieldName.Replace(' ','_').Replace('(','_').Replace(')', '_') + "_Header";
            headerTableCellTextbox.Items = new object[] 
                {
                    fieldName,
                    TableRdlGenerator.CreateHeaderTableCellTextboxStyle(),
                    true,
                    "0.3in",
                };
            headerTableCellTextbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
                {
                    Rdl.ItemsChoiceType14.Value,
                    Rdl.ItemsChoiceType14.Style,
                    Rdl.ItemsChoiceType14.CanGrow,
                    Rdl.ItemsChoiceType14.Width,
                };
            return headerTableCellTextbox;
        }

        private static Rdl.StyleType CreateHeaderTableCellTextboxStyle()
        {
            Rdl.StyleType headerTableCellTextboxStyle = new Rdl.StyleType();
            headerTableCellTextboxStyle.Items = new object[]
                {
                    "700",
                    "11pt",
                };
            headerTableCellTextboxStyle.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.FontSize,
                };
            return headerTableCellTextboxStyle;
        }

        private Rdl.DetailsType CreateDetails()
        {
            Rdl.DetailsType details = new Rdl.DetailsType();
            details.Items = new object[] { CreateTableRows() };
            return details;
        }

        private Rdl.TableRowsType CreateTableRows()
        {
            Rdl.TableRowsType tableRows = new Rdl.TableRowsType();
            tableRows.TableRow = new Rdl.TableRowType[] { CreateTableRow() };
            return tableRows;
        }

        private Rdl.TableRowType CreateTableRow()
        {
            Rdl.TableRowType tableRow = new Rdl.TableRowType();
            tableRow.Items = new object[] { CreateTableCells(), "0.25in" };
            return tableRow;
        }

        private Rdl.TableCellsType CreateTableCells()
        {
            Rdl.TableCellsType tableCells = new Rdl.TableCellsType();
            tableCells.TableCell = new Rdl.TableCellType[m_fields.Count];
            for (int i = 0; i < m_fields.Count; i++)
            {
                tableCells.TableCell[i] = TableRdlGenerator.CreateTableCell(m_fields[i]);
            }
            return tableCells;
        }

        private static Rdl.TableCellType CreateTableCell(string fieldName)
        {
            Rdl.TableCellType tableCell = new Rdl.TableCellType();
            tableCell.Items = new object[] { TableRdlGenerator.CreateTableCellReportItems(fieldName) };
            return tableCell;
        }

        private static Rdl.ReportItemsType CreateTableCellReportItems(string fieldName)
        {
            Rdl.ReportItemsType reportItems = new Rdl.ReportItemsType();
            reportItems.Items = new object[] { TableRdlGenerator.CreateTableCellTextbox(fieldName) };
            return reportItems;
        }

        private static Rdl.TextboxType CreateTableCellTextbox(string fieldName)
        {
            Rdl.TextboxType textbox = new Rdl.TextboxType();
            textbox.Name = fieldName;
            textbox.Items = new object[] 
                {
                    "=Fields!" + fieldName + ".Value",
                    TableRdlGenerator.CreateTableCellTextboxStyle(),
                    true,
                };
            textbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
                {
                    Rdl.ItemsChoiceType14.Value,
                    Rdl.ItemsChoiceType14.Style,
                    Rdl.ItemsChoiceType14.CanGrow,
                };
            return textbox;
        }

        private static Rdl.StyleType CreateTableCellTextboxStyle()
        {
            Rdl.StyleType style = new Rdl.StyleType();
            style.Items = new object[]
                {
                    "=iif(RowNumber(Nothing) mod 2, \"AliceBlue\", \"White\")",
                    "Left",
                    "8pt",
                };
            style.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.BackgroundColor,
                    Rdl.ItemsChoiceType5.TextAlign,
                    Rdl.ItemsChoiceType5.FontSize,
                };
            return style;
        }

        private Rdl.TableColumnsType CreateTableColumns()
        {
            Rdl.TableColumnsType tableColumns = new Rdl.TableColumnsType();
            tableColumns.TableColumn = new Rdl.TableColumnType[m_fields.Count];
            for (int i = 0; i < m_fields.Count; i++)
            {
                tableColumns.TableColumn[i] = TableRdlGenerator.CreateTableColumn(m_width[i]);
            }
            return tableColumns;
        }

        private static Rdl.TableColumnType CreateTableColumn()
        {
            Rdl.TableColumnType tableColumn = new Rdl.TableColumnType();
            tableColumn.Items = new object[] { "2in" };
            return tableColumn;
        }

        private static Rdl.TableColumnType CreateTableColumn(int widthInPixels)
        {
            double widthInInch = (double)widthInPixels / 100;
            Rdl.TableColumnType tableColumn = new Rdl.TableColumnType();
            string format = widthInInch.ToString("0.00") + "in";
            //next line was necessary because comma raises error when regional settings uses format like 1.234,88
            format = format.Replace(',', '.');
            tableColumn.Items = new object[] { format };
            return tableColumn;
        }
    }
}
