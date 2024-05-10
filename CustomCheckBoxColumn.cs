
namespace UserForm
{
    public class CustomCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        private string label = "";
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public CustomCheckBoxColumn()
        {
            CellTemplate = new CustomCheckBoxCell();
        }
    }

    public class CustomCheckBoxCell : DataGridViewCheckBoxCell
    {
        // checkbox string   
        private string label = "";

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        // override Paint  
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            // the base Paint  
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // get the check box bounds  
            Rectangle contentBounds = GetContentBounds(rowIndex);

            // the string location  
            Point stringLocation = new()
            {
                Y = cellBounds.Y + 2,
                X = cellBounds.X + contentBounds.Right + 10
            };

            // paint the string.  
            if (Label == null)
            {
                CustomCheckBoxColumn col = (CustomCheckBoxColumn)OwningColumn;
                label = col.Label;
            }

            graphics.DrawString(
            Label, Control.DefaultFont, Brushes.Black, stringLocation);

        }

        public static explicit operator CustomCheckBoxCell(Color v)
        {
            throw new NotImplementedException();
        }
    }

}
