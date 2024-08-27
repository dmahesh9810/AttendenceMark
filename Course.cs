using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data;
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

            Add.Click += Add_Click;

            this.Controls.Add(CourseLbl);
            this.Controls.Add(CourseTxt);
            this.Controls.Add(Add);
        }
        private void Add_Click(object? sender, EventArgs e)
{
    string name = CourseTxt.Text;

    if (string.IsNullOrWhiteSpace(name))
    {
        MessageBox.Show("Please enter a course name.");
        return;
    }

    string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand("InsertCourse", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Name", name);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Data successfully inserted!");
        }
        catch (SqlException ex)
        {
            MessageBox.Show("SQL Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }
}

    }
}