using System.Diagnostics;
using System.Drawing.Text;


namespace StatementViewer.Domain
{
    /// <summary>
    /// Расширение для ячейки DataGridView. С изменением логики логики рисования, 
    /// для возможностью слияния нескольких ячеек в одну   
    /// </summary>
    public class MergedCell(StringAlignment alignment, int leftColumnIndex, int rigthColumnIndex) : DataGridViewTextBoxCell
    {
        private readonly StringAlignment _alignment = alignment;
        private readonly int _leftColumnIndex = leftColumnIndex;
        private readonly int _rigthColumnIndex = rigthColumnIndex;



        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            try
            {
                var mergeCellsRectangle = CalculateMergeCellsRectangleSize(cellBounds);
                DrawBackgroundForMergedCellsRectangle(graphics, cellStyle.BackColor, mergeCellsRectangle);
                DrawBottomAndRightBordersForMergeCellsRectangle(graphics, mergeCellsRectangle);
                DrawTextForMergedCellsRectangle(graphics, cellStyle.ForeColor, cellStyle.Font, mergeCellsRectangle);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        private static void DrawBackgroundForMergedCellsRectangle(Graphics graphics, Color color, RectangleF mergeCellsRectangle)
        {
            using var backColorBrush = new SolidBrush(color);
            graphics.FillRectangle(backColorBrush, mergeCellsRectangle);
        }

        private void DrawTextForMergedCellsRectangle(Graphics graphics, Color color, Font font, RectangleF mergeCellsRectangle)
        {
            using var foreColorBrush = new SolidBrush(color);
            var format = new StringFormat
            {
                Alignment = _alignment,
                LineAlignment = StringAlignment.Center
            };
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.DrawString(GetTextForMergedCells(), font, foreColorBrush, mergeCellsRectangle, format);

        }

        private static void DrawBottomAndRightBordersForMergeCellsRectangle(Graphics graphics, RectangleF mergeCellsRectangle)
        {
            graphics.DrawLine(new Pen(new SolidBrush(SystemColors.InfoText)),
                mergeCellsRectangle.Left - 1,
                mergeCellsRectangle.Bottom - 1,
                mergeCellsRectangle.Right - 1,
                mergeCellsRectangle.Bottom - 1);

            graphics.DrawLine(new Pen(new SolidBrush(SystemColors.InfoText)),
                mergeCellsRectangle.Right - 1,
                mergeCellsRectangle.Top - 1,
                mergeCellsRectangle.Right - 1,
                mergeCellsRectangle.Bottom - 1);
        }

        private RectangleF CalculateMergeCellsRectangleSize(Rectangle currentCellBounds)
        {
            return new RectangleF(
                    currentCellBounds.Left - CalculateWidthFromFirstCellToCurrentCell(),
                    currentCellBounds.Top,
                    CalculateTotalWidthOfMergedCells(),
                    currentCellBounds.Height);
        }

        private int CalculateWidthFromFirstCellToCurrentCell()
        {
            int width = 0;
            for (var i = _leftColumnIndex; i < ColumnIndex; i++)
            {
                width += OwningRow.Cells[i].Size.Width;
            }

            return width;
        }

        private int CalculateTotalWidthOfMergedCells()
        {
            var width = 0;
            for (var i = _leftColumnIndex; i <= _rigthColumnIndex; i++)
            {
                width += OwningRow.Cells[i].Size.Width;
            }

            return width;
        }

        private string GetTextForMergedCells()
        {
            return OwningRow.Cells[_leftColumnIndex].Value.ToString() ?? "";
        }
    }
}


