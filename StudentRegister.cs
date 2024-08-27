using System;
using System.Windows.Forms;
using System.Drawing; // Ensure this namespace is imported
using System.Drawing.Imaging; // Import the ImageFormat class for saving images
using QRCoder;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace AttendanceMark
{
    public class StudentRegister : Form
    {
        private Label? firstName, lastName, nic, dob, whatsapp, caretaker, caretakerWhatsapp, institute, grade, course, index_number;
        private TextBox? firstNametxt, lastNametxt, nictxt, whatsapptxt, caretakertxt, caretakerWhatsapptxt, index_numbertxt;
        private DateTimePicker? dobtxt;
        private ComboBox? institutetxt, coursetxt;
        private NumericUpDown? gradetxt;
        private Button? qr_gen;
        private PictureBox? qrPictureBox;

        public StudentRegister()
        {
            this.Text = "Student Register";
            this.Width = 800;
            this.Height = 600;

            InitializeControls();
            AddControlsToForm();
        }

        private void InitializeControls()
        {
            firstName = new Label { Text = "First Name", AutoSize = true, Location = new System.Drawing.Point(50, 30) };
            firstNametxt = new TextBox { Location = new System.Drawing.Point(200, 30), Width = 200 };

            lastName = new Label { Text = "Last Name", AutoSize = true, Location = new System.Drawing.Point(50, 60) };
            lastNametxt = new TextBox { Location = new System.Drawing.Point(200, 60), Width = 200 };

            nic = new Label { Text = "NIC", AutoSize = true, Location = new System.Drawing.Point(50, 90) };
            nictxt = new TextBox { Location = new System.Drawing.Point(200, 90), Width = 200 };

            dob = new Label { Text = "Date of Birth", AutoSize = true, Location = new System.Drawing.Point(50, 120) };
            dobtxt = new DateTimePicker { Location = new System.Drawing.Point(200, 120), Width = 200 };

            whatsapp = new Label { Text = "WhatsApp", AutoSize = true, Location = new System.Drawing.Point(50, 150) };
            whatsapptxt = new TextBox { Location = new System.Drawing.Point(200, 150), Width = 200 };

            caretaker = new Label { Text = "Caretaker", AutoSize = true, Location = new System.Drawing.Point(50, 180) };
            caretakertxt = new TextBox { Location = new System.Drawing.Point(200, 180), Width = 200 };

            caretakerWhatsapp = new Label { Text = "Caretaker WhatsApp", AutoSize = true, Location = new System.Drawing.Point(50, 210) };
            caretakerWhatsapptxt = new TextBox { Location = new System.Drawing.Point(200, 210), Width = 200 };

            institute = new Label { Text = "Institute", AutoSize = true, Location = new System.Drawing.Point(50, 240) };
            institutetxt = new ComboBox { Location = new System.Drawing.Point(200, 240), Width = 200 };


            course = new Label { Text = "Course", AutoSize = true, Location = new System.Drawing.Point(50, 270) };
            coursetxt = new ComboBox { Location = new System.Drawing.Point(200, 270), Width = 200 };

            grade = new Label { Text = "Grade", AutoSize = true, Location = new System.Drawing.Point(50, 300) };
            gradetxt = new NumericUpDown { Location = new System.Drawing.Point(200, 300), Width = 200, Minimum = 1, Maximum = 12 };

            index_number = new Label { Text = "Index Number", AutoSize = true, Location = new System.Drawing.Point(50, 330) };
            index_numbertxt = new TextBox { Text = GenerateIndexNumber(), Location = new System.Drawing.Point(200, 330), Width = 200 };

            qr_gen = new Button { Text = "Generate QR And Save", AutoSize = true, Location = new System.Drawing.Point(200, 360) };
            qr_gen.Click += new EventHandler(GenerateQRCode);

            qrPictureBox = new PictureBox { Location = new System.Drawing.Point(200, 390), Size = new System.Drawing.Size(300, 300) };
            LoadInstituteData();
        }
        private void LoadInstituteData()
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT id, name FROM Institutes"; // Adjust the query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var instituteData = new Dictionary<int, string>();

                    while (reader.Read())
                    {
                        int instituteId = reader.GetInt32(0); // assuming id is the first column
                        string instituteName = reader.GetString(1); // assuming name is the second column

                        if (!string.IsNullOrEmpty(instituteName))
                        {
                            instituteData.Add(instituteId, instituteName);
                        }
                        else
                        {
                            instituteData.Add(instituteId, "Unknown Institute");
                        }
                    }

                    institutetxt.DataSource = new BindingSource(instituteData, null);
                    institutetxt.DisplayMember = "Value";
                    institutetxt.ValueMember = "Key";
                    institutetxt.SelectedIndexChanged += Institutetxt_SelectedIndexChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving institutes: " + ex.Message);
                }
            }
        }

        private void Institutetxt_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (institutetxt.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                int selectedId = selectedItem.Key;
                LoadCourseData(selectedId); // Load courses based on selected institute
            }
        }

        private void LoadCourseData(int instituteId)
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT Course_fee.id, Courses.name " +
                       "FROM Course_fee " +
                       "LEFT JOIN Courses ON Course_fee.Course_ID = Courses.id " +
                       "WHERE Course_fee.Institute_ID = @InstituteId";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InstituteId", instituteId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var courseData = new Dictionary<int, string>();

                    while (reader.Read())
                    {
                        int courseId = reader.GetInt32(0); // assuming id is the first column
                        string courseName = reader.GetString(1); // assuming name is the second column

                        if (!string.IsNullOrEmpty(courseName))
                        {
                            courseData.Add(courseId, courseName);
                        }
                        else
                        {
                            courseData.Add(courseId, "Unknown Course");
                        }
                    }

                    coursetxt.DataSource = new BindingSource(courseData, null);
                    coursetxt.DisplayMember = "Value";
                    coursetxt.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while retrieving courses: " + ex.Message);
                }
            }
        }

        private void Coursetxt_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (coursetxt.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                int selectedId = selectedItem.Key;
                MessageBox.Show($"Selected Course ID: {selectedId}");
            }
        }




        private void AddControlsToForm()
        {
            this.Controls.Add(firstName);
            this.Controls.Add(firstNametxt);
            this.Controls.Add(lastName);
            this.Controls.Add(lastNametxt);
            this.Controls.Add(nic);
            this.Controls.Add(nictxt);
            this.Controls.Add(dob);
            this.Controls.Add(dobtxt);
            this.Controls.Add(whatsapp);
            this.Controls.Add(whatsapptxt);
            this.Controls.Add(caretaker);
            this.Controls.Add(caretakertxt);
            this.Controls.Add(caretakerWhatsapp);
            this.Controls.Add(caretakerWhatsapptxt);
            this.Controls.Add(institute);
            this.Controls.Add(institutetxt);
            this.Controls.Add(course);
            this.Controls.Add(coursetxt);
            this.Controls.Add(grade);
            this.Controls.Add(gradetxt);
            this.Controls.Add(index_number);
            this.Controls.Add(index_numbertxt);
            this.Controls.Add(qr_gen);
            this.Controls.Add(qrPictureBox);
        }

        private void GenerateQRCode(object? sender, EventArgs e)
        {
            var studentData = new
            {
                FirstName = firstNametxt?.Text,
                LastName = lastNametxt?.Text,
                NIC = nictxt?.Text,
                DateOfBirth = dobtxt?.Value.ToString("yyyy-MM-dd"),
                WhatsApp = whatsapptxt?.Text,
                Caretaker = caretakertxt?.Text,
                CaretakerWhatsApp = caretakerWhatsapptxt?.Text,
                Institute = institutetxt?.Text,
                Course = coursetxt?.Text,
                Grade = gradetxt?.Value.ToString(),
                IndexNumber = index_numbertxt?.Text
            };

            string qrContent = $"First Name: {studentData.FirstName}\n" +
                               $"Last Name: {studentData.LastName}\n" +
                               $"NIC: {studentData.NIC}\n" +
                               $"Date of Birth: {studentData.DateOfBirth}\n" +
                               $"WhatsApp: {studentData.WhatsApp}\n" +
                               $"Caretaker: {studentData.Caretaker}\n" +
                               $"Caretaker WhatsApp: {studentData.CaretakerWhatsApp}\n" +
                               $"Institute: {studentData.Institute}\n" +
                               $"Course: {studentData.Course}\n" +
                               $"Grade: {studentData.Grade}\n" +
                               $"Index Number: {studentData.IndexNumber}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                int pixelSize = 2; // Example: adjust this value as needed
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(pixelSize);
                    // Save QR code image to a folder
                    string outputDirectory = Path.GetDirectoryName(Application.ExecutablePath);

                    // Navigate to the desired folder (create if not exists)
                    string folderPath = Path.Combine(outputDirectory, "qrcodes");
                    Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist

                    // Generate a unique file name for the QR code image
                    string fileName = $"QRCode_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png";
                    string filePath = Path.Combine(folderPath, fileName);

                    // Save QR code image as PNG
                    qrCodeImage.Save(filePath, ImageFormat.Png);
                    SaveDataToDatabase(studentData, fileName);

                    // Optionally display the QR code in PictureBox
                    qrPictureBox!.Image = qrCodeImage;
                }
            }
        }

        private void SaveDataToDatabase(dynamic studentData, string fileName)
        {
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SaveStudentData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FirstName", studentData.FirstName);
                    command.Parameters.AddWithValue("@LastName", studentData.LastName);
                    command.Parameters.AddWithValue("@NIC", studentData.NIC);
                    command.Parameters.AddWithValue("@DateOfBirth", studentData.DateOfBirth);
                    command.Parameters.AddWithValue("@WhatsApp", studentData.WhatsApp);
                    command.Parameters.AddWithValue("@Caretaker", studentData.Caretaker);
                    command.Parameters.AddWithValue("@CaretakerWhatsApp", studentData.CaretakerWhatsApp);
                    command.Parameters.AddWithValue("@Institute", studentData.Institute);
                    command.Parameters.AddWithValue("@Course", studentData.Course);
                    command.Parameters.AddWithValue("@Grade", studentData.Grade);
                    command.Parameters.AddWithValue("@IndexNumber", studentData.IndexNumber);
                    command.Parameters.AddWithValue("@QRCodeFileName", fileName);

                    connection.Open();
                    int returnValue = (int)command.ExecuteScalar(); // Assuming the stored procedure returns a value
                    connection.Close();

                    if (returnValue == 0)
                    {
                        MessageBox.Show("Data saved to database successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to save data to database.");
                    }
                }
            }
        }

        private string GenerateIndexNumber()
        {
            string indexNumber = "STU_001"; // Default index number format

            // Replace with your actual connection string
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";
            string query = "SELECT TOP 1 StudentID FROM StudentData ORDER BY StudentID DESC"; // Retrieve the last inserted student ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int lastId = Convert.ToInt32(result);
                        indexNumber = $"STU_{(lastId + 1):000}"; // Format the next index number
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while generating index number: " + ex.Message);
                }
            }

            return indexNumber;
        }

    }
}


