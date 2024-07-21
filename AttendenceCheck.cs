using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using AttendenceMark;

namespace AttendanceMark
{
    public class AttendanceCheck : Form
    {
        private TextBox IndexNumber;
        private DateTimePicker StartDate;
        private DateTimePicker EndDate;
        private Label StartDateLbl;
        private Label EndDateLbl;
        private Label IndexNumbrLbl;
        private Button Search;
        private DataGridView AttendanceData;
        public AttendanceCheck()
        {
            this.Text = "Attendance Check";
            this.Width = 1080;
            this.Height = 720;
            AttendanceData = new DataGridView
            {
                Location = new Point(40, 250),
                Width = this.ClientSize.Width - 80,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill // Adjust columns to fill the DataGridView width
            };

            IndexNumbrLbl = new Label
            {
                Text = "Index Number",
                Location = new Point(50, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            IndexNumber = new TextBox
            {
                Location = new Point(150, 30),
                Width = 200,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            StartDateLbl = new Label
            {
                Text = "Start Date",
                Location = new Point(50, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            StartDate = new DateTimePicker
            {
                Location = new Point(150, 100),
                Width = 200,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            EndDateLbl = new Label
            {
                Text = "End Date",
                Location = new Point(50, 170),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            EndDate = new DateTimePicker
            {
                Location = new Point(150, 170),
                Width = 200,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };
            Search = new Button
            {
                Text = "Search",
                Location = new Point(150, 200),
                Width = 200,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            };

            Search.Click += LoadData_Click;

            this.Controls.Add(IndexNumbrLbl);
            this.Controls.Add(IndexNumber);
            this.Controls.Add(StartDateLbl);
            this.Controls.Add(StartDate);
            this.Controls.Add(EndDateLbl);
            this.Controls.Add(EndDate);
            this.Controls.Add(Search);
            this.Controls.Add(AttendanceData);
        }

        private void LoadData_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT StudentId FROM StudentData WHERE IndexNumber = @IndexNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IndexNumber", IndexNumber.Text);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Assuming StudentId is of type int; adjust the type if necessary
                    int studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                    LoadData(studentId);
                }
                else
                {
                    MessageBox.Show("No records found.");
                }

                reader.Close();

            }
        }
        private void LoadData(int studentId)
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

            // Adjust the query to use parameters
            string query = @"
    SELECT * 
    FROM Attendances 
    WHERE Student_Id = @studentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with the correct data types
                    command.Parameters.AddWithValue("@studentId", studentId);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    AttendanceData.DataSource = dataTable;
                }

            }
        }
        }
    }