using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace AttendenceMark
{
    public class Login : Form
    {
        private Label UserNameLbl;
        private TextBox UserName;
        private Label PasswordLbl;
        private TextBox Password;
        private Button Submit;

        public Login()
        {
            this.Text = "Login";
            this.Width = 430;
            this.Height = 350;
            // Set the maximum and minimum sizes
            this.MaximumSize = new System.Drawing.Size(450, 380); // Set your desired maximum size
            this.MinimumSize = new System.Drawing.Size(400, 320); // Set your desired minimum size

            UserNameLbl = new Label
            {
                Text = "User Name ",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 70),
            };
            UserName = new TextBox
            {
                Location = new System.Drawing.Point(140, 100),
                Width = this.ClientSize.Width - 280,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            PasswordLbl = new Label
            {
                Text = "Password ",
                AutoSize = true,
                Location = new System.Drawing.Point(140, 140),
            };
            Password = new TextBox
            {
                Location = new System.Drawing.Point(140, 170),
                Width = this.ClientSize.Width - 280,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            Submit = new Button
            {
                Text = "Submit",
                Location = new System.Drawing.Point(140, 210),
                Width = this.ClientSize.Width - 280,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            Submit.Click += (sender, e) =>
            {
                if(UserName.Text == "iqbrave" && Password.Text == "1234"){

                Dashbord dashbord = new Dashbord();
                this.Hide();
                dashbord.Show();
                }else{
                    MessageBox.Show("Invalid username or password.");
                }
            };

            this.Controls.Add(UserNameLbl);
            this.Controls.Add(UserName);
            this.Controls.Add(PasswordLbl);
            this.Controls.Add(Password);
            this.Controls.Add(Submit);

        }

    }
}
