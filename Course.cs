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

            Add.Click += Add_Click;

            this.Controls.Add(CourseLbl);
            this.Controls.Add(CourseTxt);
            this.Controls.Add(Add);
        }
        private void Add_Click(object? sender, EventArgs e)
        {
            string name = CourseTxt.Text;

            // Connection string to your SQL Server database
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Courses (name) VALUES (@Name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check if the insert was successful
                    if (result < 0)
                        MessageBox.Show("Error inserting data into Database!");
                    else
                        MessageBox.Show("Data successfully inserted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}