using System;
using System.Windows.Forms;

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
            index_numbertxt = new TextBox { Location = new System.Drawing.Point(200, 330), Width = 200 };

            qr_gen = new Button { Text = "Generate QR", AutoSize = true, Location = new System.Drawing.Point(200, 360) };
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
        }
    }
}
