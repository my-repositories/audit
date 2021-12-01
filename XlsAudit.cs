using System;
using ExcelLibrary.SpreadSheet;

namespace Audit
{
    public class XlsAudit : IDisposable
    {
        private readonly Workbook _workbook = new Workbook();
        private readonly Worksheet _worksheet;
        private readonly string _outputPath;
        private int rowCounter = 1;

        public XlsAudit(string sheetName, string outputPath)
        {
            _outputPath = outputPath;
            _worksheet = new Worksheet(sheetName);
            _worksheet.Cells[0, 0] = new Cell("Path"); 
            _worksheet.Cells[0, 1] = new Cell("Name"); 
            _worksheet.Cells[0, 2] = new Cell("Type"); 
            _worksheet.Cells[0, 3] = new Cell("Size"); 
            _worksheet.Cells[0, 4] = new Cell("Hash");
        }

        public void AddRow(FileModel model)
        {
            _worksheet.Cells[rowCounter, 0] = new Cell(model.FullPath); 
            _worksheet.Cells[rowCounter, 1] = new Cell(model.Name); 
            _worksheet.Cells[rowCounter, 2] = new Cell(model.Type);
            _worksheet.Cells[rowCounter, 3] = new Cell(model.Size.ToString());
            _worksheet.Cells[rowCounter, 4] = new Cell(model.Hash);
            ++rowCounter;
        }

        public void Dispose()
        {
            _workbook.Worksheets.Add(_worksheet);
            _workbook.Save(_outputPath);
        }
    }
}