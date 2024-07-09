namespace AttendenceMark;

public partial class Dashbord : Form
{
    public Dashbord()
    {
        InitializeComponent();

        this.Text = "My WinForms App";
            this.Width = 800;
            this.Height = 600;

            Button button1 = new Button();
            button1.Text = "Click Me";
            button1.Location = new System.Drawing.Point(350, 250);
            button1.Click += (sender, e) => MessageBox.Show("Hello, World!");

             Button button2 = new Button();
            button2.Text = "Open Form2";
            button2.Location = new System.Drawing.Point(350, 300);
            button2.Click += (sender, e) =>
            {
                Login form2 = new Login();
                form2.Show();
            };

            this.Controls.Add(button1);
            this.Controls.Add(button2);
    }
}
