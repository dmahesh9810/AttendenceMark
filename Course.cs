using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace AttendenceMark
{
    public class Course : Form
    {
        private Label CourseLbl;
        private TextBox CourseTxt;
        private Button Add;
        public Course()
        {
            this.Text = "ADD Course";
            this.Width = 500;
            this.Height = 300;

            CourseLbl = new Label
            {
                Text = "Course ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 70),
            };
            CourseTxt = new TextBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 70),
            };
            Add = new Button
            {
                Text = "Add",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 100),
                Width = 100,
            };

            this.Controls.Add(CourseLbl);
            this.Controls.Add(CourseTxt);
            this.Controls.Add(Add);
        }
    }
}