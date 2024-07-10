using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace AttendenceMark
{
    public class Login : Form
    {
        private Label UserNameLbl;
        private TextBox UserName;
        private Label PasswordLbl;
        private TextBox Password;
        private Button Submit;
        private string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

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
            Submit.Click += Submit_Click;

            this.Controls.Add(UserNameLbl);
            this.Controls.Add(UserName);
            this.Controls.Add(PasswordLbl);
            this.Controls.Add(Password);
            this.Controls.Add(Submit);

        }

        private void Submit_Click(object? sender, EventArgs e)
        {
            string username = UserName.Text;
            string password = Password.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    connection.Close();

                    if (count > 0)
                    {
                        Dashbord dashboard = new Dashbord();
                        this.Hide();
                        dashboard.Show();
                    }
                    else
                    {
                        // temp
                        Dashbord temp = new Dashbord();
                        this.Hide();
                        temp.Show();
                        // temp
                        // MessageBox.Show("Invalid username or password.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
