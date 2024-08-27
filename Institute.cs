using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data;
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
                Text = "City ",
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

            string connectionString = "Server=DESKTOP-NSG87D3\\SQLEXPRESS;Database=student_tracking;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertInstitute", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@City", city);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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