namespace AttendenceMark;

public partial class Dashbord : Form
{
    public Dashbord()
    {
        InitializeComponent();

        this.Text = "Dashboard";
        this.Width = 800;
        this.Height = 600;


        Button button1 = new Button();
        button1.Text = "Open Form2";
        button1.Location = new System.Drawing.Point(350, 300);
        button1.Click += (sender, e) =>
        {
            Login form2 = new Login();
            form2.Show();
        };

        this.Controls.Add(button1);
    }
}
