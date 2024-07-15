using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace AttendenceMark
{
    public class Attendence : Form
    {
        private GroupBox Scan;
        private RadioButton RadioManual;
        private RadioButton RadioQr;
        private TextBox ManualSearchTxt;
        private Button ManualSearch;
        private Label StuId;
        private Label IndexNumberLbl;
        private Label IndexNumberTxt;
        private Label FullName;
        private Label FullNameTxt;
        private GroupBox ManualGroup;
        private GroupBox QrGroup;
        private GroupBox InOutGroup;
        private RadioButton InOutGroupIn;
        private RadioButton InOutGroupOut;
        private Button MarkBtn;
        public Attendence()
        {

            this.Text = "Attendence Mark";
            this.Width = 1080;
            this.Height = 720;
            this.Load += Attendence_Load;
            Scan = new GroupBox
            {
                Text = "Scan ",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 70),
                Size = new System.Drawing.Size(400, 100)
            };
            RadioManual = new RadioButton
            {
                Text = "Manual Mark ",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 50),
                Checked = true,
            };
            RadioQr = new RadioButton
            {
                Text = "Qr Scan",
                AutoSize = true,
                Location = new System.Drawing.Point(200, 50),
            };

            ManualGroup = new GroupBox
            {
                Text = "Manual Group ",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 200),
                Size = new System.Drawing.Size(400, 400)
            };
            ManualSearchTxt = new TextBox
            {
                AutoSize = true,
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(230, 100)
            };
            ManualSearch = new Button
            {
                Text = "Manual Search",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 80),
                Size = new System.Drawing.Size(230, 20)
            };
            IndexNumberLbl = new Label
            {
                Text = "Index Number : ",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 130),
            };
            StuId = new Label
            {
                Text = "StuId : ",
                AutoSize = true,
                Location = new System.Drawing.Point(5, 5),
            };
            IndexNumberTxt = new Label
            {
                Text = "ABC",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 130),
            };
            FullName = new Label
            {
                Text = "Name : ",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 170),
            };
            FullNameTxt = new Label
            {
                Text = "ABC",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 170),
            };
            InOutGroup = new GroupBox
            {
                Text = "In Out",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 210),
                Size = new System.Drawing.Size(230, 70)
            };
            InOutGroupIn = new RadioButton
            {
                Text = "In Out",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 30),
            };
            InOutGroupOut = new RadioButton
            {
                Text = "In Out",
                AutoSize = true,
                Location = new System.Drawing.Point(130, 30),
            };
            MarkBtn = new Button
            {
                Text = "Mark",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 300),
                Size = new System.Drawing.Size(230, 20)
            };

            RadioManual.Click += RadioManual_Click; // Attach event handler to RadioManual click event
            ManualSearch.Click += ManualSearch_Click; // Attach event handler to RadioManual click event
            MarkBtn.Click += MarkBtn_Click; // Attach event handler to RadioManual click event
            RadioQr.Click += RadioQr_Click; // Attach event handler to RadioQr click event

            Scan.Controls.Add(RadioManual);
            Scan.Controls.Add(RadioQr);
            Scan.Controls.Add(ManualGroup);
            ManualGroup.Controls.Add(ManualSearchTxt);
            ManualGroup.Controls.Add(ManualSearch);
            ManualGroup.Controls.Add(IndexNumberLbl);
            ManualGroup.Controls.Add(IndexNumberTxt);
            ManualGroup.Controls.Add(FullName);
            ManualGroup.Controls.Add(FullNameTxt);
            ManualGroup.Controls.Add(InOutGroup);
            // ManualGroup.Controls.Add(StuId);

            InOutGroup.Controls.Add(InOutGroupIn);
            InOutGroup.Controls.Add(InOutGroupOut);
            ManualGroup.Controls.Add(MarkBtn);




            this.Controls.Add(ManualGroup);
            this.Controls.Add(Scan);


            QrGroup = new GroupBox
            {
                Text = "Qr Scan",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 200),
                Size = new System.Drawing.Size(400, 400)
            };
            // ManualGroup.Controls.Add(QrGroup);

            this.Controls.Add(QrGroup);



        }

        private void MarkBtn_Click(object? sender, EventArgs e)
        {

            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            DateTime today = DateTime.Today;
            int studentId;

            // Convert StuId.Text to int assuming it holds an integer value
            if (!int.TryParse(StuId.Text, out studentId))
            {
                MessageBox.Show("Invalid Student ID format.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Check if attendance record exists for today
                string selectQuery = "SELECT COUNT(*) FROM Attendances WHERE Student_Id = @StudentId AND CAST(InTime AS DATE) = @Today";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@StudentId", studentId);
                selectCommand.Parameters.AddWithValue("@Today", today);

                try
                {
                    connection.Open();
                    int existingRecordsCount = (int)selectCommand.ExecuteScalar();

                    if (existingRecordsCount > 0)
                    {
                        if (InOutGroupOut.Checked)
                        {
                            string updateQuery = "UPDATE Attendances SET Check_Out = @CheckOutS, OutTime = @OutTime WHERE Student_Id = @StudentIdS";
                            SqlCommand insertCommand = new SqlCommand(updateQuery, connection);
                            insertCommand.Parameters.AddWithValue("@CheckOutS", 0);
                            insertCommand.Parameters.AddWithValue("@StudentIdS", studentId);
                            insertCommand.Parameters.AddWithValue("@OutTime", DateTime.Now); // Assuming OutTime is not yet known
                            int rowsAffected = insertCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Attendance record Update successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to insert attendance record.");
                            }
                        }
                        else if (InOutGroupIn.Checked)
                        {
                            MessageBox.Show("Allrady in Student");
                        }


                    }
                    else if (InOutGroupIn.Checked)
                    {
                        // Insert attendance record
                        string insertQuery = "INSERT INTO Attendances (Check_In, Check_Out, Student_Id, InTime, OutTime) VALUES (@CheckInS, @CheckOutS, @StudentIdS, @InTime, @OutTime)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@CheckInS", 1);
                        insertCommand.Parameters.AddWithValue("@CheckOutS", 0);
                        insertCommand.Parameters.AddWithValue("@StudentIdS", studentId);
                        insertCommand.Parameters.AddWithValue("@InTime", DateTime.Now);
                        insertCommand.Parameters.AddWithValue("@OutTime", DBNull.Value); // Assuming OutTime is not yet known

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Attendance record inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert attendance record.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Student not come today");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }





        }
        private void ManualSearch_Click(object? sender, EventArgs e)
        {

            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT StudentID, FirstName,LastName,IndexNumber From StudentData where IndexNumber='" + ManualSearchTxt.Text + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        var studentIdObj = reader["StudentID"];
                        int StudentID = studentIdObj != DBNull.Value ? Convert.ToInt32(studentIdObj) : 0;
                        var FirstName = reader["FirstName"] as string;
                        var LastName = reader["LastName"] as string;
                        var IndexNumber = reader["IndexNumber"] as string;
                        if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(IndexNumber))
                        {

                            StuId.Text = StudentID.ToString();
                            IndexNumberTxt.Text = IndexNumber;
                            FullNameTxt.Text = $"{FirstName} {LastName}";
                        }
                        else
                        {
                            MessageBox.Show("dddd ");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving institutes: " + ex.Message);
                }
            }
        }

        private void ManualSearch_Click()
        {
            ManualGroup.Visible = false;
            QrGroup.Visible = false;

        }
        private void Attendence_Load(object sender, EventArgs e)
        {
            // Initially hide both groups
            ManualGroup.Visible = false;
            QrGroup.Visible = false;
        }
        private void RadioManual_Click(object sender, EventArgs e)
        {
            ManualGroup.Visible = true;
            QrGroup.Visible = false;
        }
        private void RadioQr_Click(object sender, EventArgs e)
        {
            ManualGroup.Visible = false;
            QrGroup.Visible = true;
        }
    }

}