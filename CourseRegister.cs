using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace AttendenceMark
{
    public class CourseRegister : Form
    {
        private Label InstituteLbl;
        private ComboBox InstituteTxt;
        private Label CourseLbl;
        private ComboBox CourseTxt;
        private Label CourseFeeLbl;
        private TextBox CourseFeeTxt;
        private Button Add;
        public CourseRegister()
        {
            this.Text = "Course Register";
            this.Width = 500;
            this.Height = 300;

            InstituteLbl = new Label
            {
                Text = "Institute ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 70),
            };
            InstituteTxt = new ComboBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 70),
            };
            CourseLbl = new Label
            {
                Text = "Course ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 100),
            };
            CourseTxt = new ComboBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 100),
            };
            CourseFeeLbl = new Label
            {
                Text = "Fee ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 130),
            };
            CourseFeeTxt = new TextBox
            {
                Text = "",
                AutoSize = true,
                Width = 120,
                Location = new System.Drawing.Point(210, 130),
            };
            Add = new Button
            {
                Text = "Register ",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 160),
                Width = 100,
            };

            this.Controls.Add(InstituteLbl);
            this.Controls.Add(InstituteTxt);
            this.Controls.Add(CourseLbl);
            this.Controls.Add(CourseTxt);
            this.Controls.Add(CourseFeeLbl);
            this.Controls.Add(CourseFeeTxt);
            this.Controls.Add(Add);

            LoadInstituteData();
            LoadCourseData();
        }
        private void LoadInstituteData()
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT name FROM Institutes"; // Adjust the query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var instituteName = reader["name"] as string;
                        if (!string.IsNullOrEmpty(instituteName))
                        {
                            InstituteTxt.Items.Add(instituteName);
                        }
                        else
                        {
                            InstituteTxt.Items.Add("Unknown Institute");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving institutes: " + ex.Message);
                }
            }
        }
        private void LoadCourseData()
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT name FROM Courses"; // Adjust the query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var courseName = reader["name"] as string;
                        if (!string.IsNullOrEmpty(courseName))
                        {
                            CourseTxt.Items.Add(courseName);
                        }
                        else
                        {
                            CourseTxt.Items.Add("Unknown Course");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving courses: " + ex.Message);
                }
            }
        }

    }
}