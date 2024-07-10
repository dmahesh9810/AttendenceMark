using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace AttendenceMark
{
    public class Institute : Form
    {
        private Label NameLbl;
        private TextBox NameTxt;
        private Label QtyLbl;
        private TextBox Qty;
        private Button Add;
        public Institute()
        {
            this.Text = "Institute";
            this.Width = 500;
            this.Height = 300;

            NameLbl = new Label
            {
                Text = "Name ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 70),
            };
            NameTxt = new TextBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 70),
            };
            QtyLbl = new Label
            {
                Text = "Qty ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 100),
            };
            Qty = new TextBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 100),
            };
            Add = new Button
            {
                Text = "Add",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 140),
                Width = 100,
            };

            this.Controls.Add(NameLbl);
            this.Controls.Add(NameTxt);
            this.Controls.Add(QtyLbl);
            this.Controls.Add(Qty);
            this.Controls.Add(Add);
        }
    }
}