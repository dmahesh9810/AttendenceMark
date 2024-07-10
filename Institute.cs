using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace AttendenceMark
{
    public class Institute : Form
    {
        private Label NameLbl;
        private TextBox NameTxt;
        private Label CityLbl;
        private TextBox City;
        private Button Add;
        public Institute()
        {
            this.Text = "Institute";
            this.Width = 500;
            this.Height = 300;

            NameLbl = new Label
            {
                Text = "Name ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 70),
            };
            NameTxt = new TextBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 70),
            };
            CityLbl = new Label
            {
                Text = "Qty ",
                AutoSize = true,
                Location = new System.Drawing.Point(150, 100),
            };
            City = new TextBox
            {
                Text = "",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 100),
            };
            Add = new Button
            {
                Text = "Add",
                AutoSize = true,
                Location = new System.Drawing.Point(210, 140),
                Width = 100,
            };
            Add.Click += Add_Click;

            this.Controls.Add(NameLbl);
            this.Controls.Add(NameTxt);
            this.Controls.Add(CityLbl);
            this.Controls.Add(City);
            this.Controls.Add(Add);
        }
        private void Add_Click(object? sender, EventArgs e)
        {
            string name = NameTxt.Text;
            string city = City.Text;

            // Connection string to your SQL Server database
            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Institutes (name, city) VALUES (@Name, @City)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@City", city);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check if the insert was successful
                    if (result < 0)
                        MessageBox.Show("Error inserting data into Database!");
                    else
                        MessageBox.Show("Data successfully inserted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}