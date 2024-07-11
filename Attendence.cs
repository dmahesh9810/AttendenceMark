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
            IndexNumberTxt = new Label
            {
                Text = "_____________________________",
                AutoSize = true,
                Location = new System.Drawing.Point(130, 130),
            };
            FullName = new Label
            {
                Text = "Name : ",
                AutoSize = true,
                Location = new System.Drawing.Point(50, 170),
            };
            FullNameTxt = new Label
            {
                Text = "_____________________________",
                AutoSize = true,
                Location = new System.Drawing.Point(130, 170),
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