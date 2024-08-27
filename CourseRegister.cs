using System;
using System.Windows.Forms;
using System.Data;
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

            Add.Click += new EventHandler(Add_Click); // Event handler for the Add button

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

private void Add_Click(object sender, EventArgs e)
{
    string instituteName = InstituteTxt.SelectedItem?.ToString();
    string courseName = CourseTxt.SelectedItem?.ToString();
    string courseFeeText = CourseFeeTxt.Text.Trim();

    if (string.IsNullOrEmpty(instituteName) || string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(courseFeeText))
    {
        MessageBox.Show("Please fill all fields.");
        return;
    }

    if (!decimal.TryParse(courseFeeText, out decimal courseFee))
    {
        MessageBox.Show("Invalid course fee amount.");
        return;
    }

    int instituteId = GetInstituteId(instituteName);
    int courseId = GetCourseId(courseName);

    if (instituteId == -1 || courseId == -1)
    {
        MessageBox.Show("Selected Institute or Course does not exist.");
        return;
    }

    string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand("InsertCourseFee", connection); // Change procedure name if needed
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@InstituteId", instituteId);
        command.Parameters.AddWithValue("@CourseId", courseId);
        command.Parameters.AddWithValue("@Fee", courseFee);

        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 0)
            {
                MessageBox.Show("Course fee registered successfully.");
                CourseFeeTxt.Clear();
            }
            else
            {
                MessageBox.Show("Failed to register course fee. No rows affected.");
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("SQL Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred while registering course fee: " + ex.Message);
        }
    }
}








        private int GetInstituteId(string instituteName)
        {
            int instituteId = -1; // Default value if institute is not found

            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT id FROM Institutes WHERE name = @InstituteName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InstituteName", instituteName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        instituteId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching Institute ID: " + ex.Message);
                }
            }

            return instituteId;
        }


        private int GetCourseId(string courseName)
        {
            int courseId = -1; // Default value if course is not found

            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT id FROM Courses WHERE name = @CourseName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CourseName", courseName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        courseId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching Course ID: " + ex.Message);
                }
            }

            return courseId;
        }

    }
}
