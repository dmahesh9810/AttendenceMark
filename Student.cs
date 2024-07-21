using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using AttendenceMark;

namespace AttendanceMark
{
    public class Student : Form
    {
        private DataGridView DataStudent;
        private Button DeleteButton;
        private Button UpdateButton;
        private Button ExitStudent;
        private PictureBox QRCodePictureBox;

        public Student()
        {
            this.Text = "Student Management";
            this.Width = 1080;
            this.Height = 720;

            Button Logout = new Button();
        Logout.Text = "Logout";
        Logout.Location = new System.Drawing.Point(350, 50);
        Logout.Click += (sender, e) =>
        {
            Login form2 = new Login();
             this.Close();           
            form2.Show();
        };


            DataStudent = new DataGridView
            {
                Location = new Point(40, 200),
                Width = this.ClientSize.Width - 80,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill // Adjust columns to fill the DataGridView width
            };

            DeleteButton = new Button
            {
                Text = "Delete",
                Location = new Point(50, 50),
                AutoSize = true
            };

            UpdateButton = new Button
            {
                Text = "Update",
                Location = new Point(150, 50),
                AutoSize = true
            };
            ExitStudent = new Button
            {
                Text = "Back",
                Location = new Point(250, 50),
                AutoSize = true
            };

            QRCodePictureBox = new PictureBox
            {
                Location = new Point(400, 500),
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            ExitStudent.Click += ExitStudent_Click;
            DeleteButton.Click += DeleteButton_Click;
            UpdateButton.Click += UpdateButton_Click;
            DataStudent.SelectionChanged += DataStudent_SelectionChanged;

            this.Controls.Add(DataStudent);
            this.Controls.Add(DeleteButton);
            this.Controls.Add(UpdateButton);
            this.Controls.Add(QRCodePictureBox);
            this.Controls.Add(ExitStudent);
            this.Controls.Add(Logout);

            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT * FROM StudentData"; // Adjust the query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Set the DataSource of the DataGridView
                DataStudent.DataSource = dataTable;
            }
        }
        private void ExitStudent_Click(object sender, EventArgs e)
        {
            Dashbord dashboard = new Dashbord();
            this.Hide();
            dashboard.Show();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (DataStudent.SelectedRows.Count > 0)
            {
                // Assume the first column is the ID column
                int studentId = Convert.ToInt32(DataStudent.SelectedRows[0].Cells["StudentId"].Value);
                DeleteStudent(studentId);
                LoadData(); // Refresh data after deletion
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void DeleteStudent(int studentId)
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "DELETE FROM StudentData WHERE StudentId = @StudentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataStudent.Rows)
            {
                if (row.IsNewRow) continue;

                int studentId = Convert.ToInt32(row.Cells["StudentId"].Value);
                string firstName = row.Cells["FirstName"].Value.ToString();
                string lastName = row.Cells["LastName"].Value.ToString();
                string nic = row.Cells["NIC"].Value.ToString();
                string dateOfBirth = row.Cells["DateOfBirth"].Value.ToString();
                string whatsApp = row.Cells["WhatsApp"].Value.ToString();
                string caretaker = row.Cells["Caretaker"].Value.ToString();
                string caretakerWhatsApp = row.Cells["CaretakerWhatsApp"].Value.ToString();
                string course = row.Cells["Course"].Value.ToString();
                string grade = row.Cells["Grade"].Value.ToString();
                string indexNumber = row.Cells["IndexNumber"].Value.ToString();

                UpdateStudent(studentId, firstName, lastName, nic, dateOfBirth, whatsApp, caretaker, caretakerWhatsApp, course, grade, indexNumber);
            }

            LoadData(); // Refresh data after updating
        }

        private void UpdateStudent(int studentId, string firstName, string lastName, string nic, string dateOfBirth, string whatsApp, string caretaker, string caretakerWhatsApp, string course, string grade, string indexNumber)
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = @"UPDATE StudentData 
                             SET FirstName = @FirstName, 
                                 LastName = @LastName, 
                                 NIC = @NIC, 
                                 DateOfBirth = @DateOfBirth, 
                                 WhatsApp = @WhatsApp, 
                                 Caretaker = @Caretaker, 
                                 CaretakerWhatsApp = @CaretakerWhatsApp, 
                                 Course = @Course, 
                                 Grade = @Grade, 
                                 IndexNumber = @IndexNumber 
                             WHERE StudentId = @StudentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@NIC", nic);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@WhatsApp", whatsApp);
                command.Parameters.AddWithValue("@Caretaker", caretaker);
                command.Parameters.AddWithValue("@CaretakerWhatsApp", caretakerWhatsApp);
                command.Parameters.AddWithValue("@Course", course);
                command.Parameters.AddWithValue("@Grade", grade);
                command.Parameters.AddWithValue("@IndexNumber", indexNumber);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void DataStudent_SelectionChanged(object sender, EventArgs e)
        {
            if (DataStudent.SelectedRows.Count > 0)
            {
                string QRCodeFileName = DataStudent.SelectedRows[0].Cells["QRCodeFileName"].Value.ToString();
                LoadQRCodeImage(QRCodeFileName);
            }
        }

        private void LoadQRCodeImage(string qrCodeFileName)
        {
            string outputDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string folderPath = Path.Combine(outputDirectory, "qrcodes");
            Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist

            // Generate a unique file name for the QR code image
            string fileName = $"{qrCodeFileName}";
            string filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                QRCodePictureBox.Image = Image.FromFile(filePath);
            }
            else
            {
                QRCodePictureBox.Image = null; // Clear the picture box if the image doesn't exist
            }
        }

    }
}
