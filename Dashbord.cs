using AttendanceMark;

namespace AttendenceMark;

public partial class Dashbord : Form
{
    private Panel dynamicPanel;
    public Dashbord()
    {
        InitializeComponent();

        this.Text = "Dashboard";
        this.Width = 830;
        this.Height = 630;

        dynamicPanel = new Panel();
        dynamicPanel.Location = new System.Drawing.Point(200, 20);
        dynamicPanel.Size = new System.Drawing.Size(580, 560);
        dynamicPanel.BorderStyle = BorderStyle.FixedSingle;

        this.Controls.Add(dynamicPanel);



        GroupBox groupBox1 = new GroupBox();
        groupBox1.Text = "User Information";
        groupBox1.Location = new System.Drawing.Point(20, 20);
        groupBox1.Size = new System.Drawing.Size(170, 320);

        Button Logout = new Button();
        Logout.Text = "Logout";
        Logout.Location = new System.Drawing.Point(20, 280);
        Logout.Width = 130;
        Logout.Click += (sender, e) =>
        {
            Login form2 = new Login();
             this.Close();           
            form2.Show();
        };


        Button institute_reg = new Button();
        institute_reg.Text = "Institute Register";
        institute_reg.Location = new System.Drawing.Point(20, 30);
        institute_reg.Width = 130;
        institute_reg.Click += (sender, e) =>
        {
            // Create and show Form2 in the panel
            Institute form2 = new Institute();
            form2.TopLevel = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.Dock = DockStyle.Fill;

            // Clear existing controls in the panel
            dynamicPanel.Controls.Clear();
            dynamicPanel.Controls.Add(form2);
            form2.Show();
        };

        Button add_course = new Button();
        add_course.Text = "Add Course";
        add_course.Location = new System.Drawing.Point(20, 60);
        add_course.Width = 130;
        add_course.Click += (sender, e) =>
        {
            // Create and show Form2 in the panel
            Course form2 = new Course();
            form2.TopLevel = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.Dock = DockStyle.Fill;

            // Clear existing controls in the panel
            dynamicPanel.Controls.Clear();
            dynamicPanel.Controls.Add(form2);
            form2.Show();
        };

        Button course_reg = new Button();
        course_reg.Text = "Course Register";
        course_reg.Location = new System.Drawing.Point(20, 90);
        course_reg.Width = 130;
        course_reg.Click += (sender, e) =>
        {
            CourseRegister form2 = new CourseRegister();
            form2.TopLevel = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.Dock = DockStyle.Fill;

            // Clear existing controls in the panel
            dynamicPanel.Controls.Clear();
            dynamicPanel.Controls.Add(form2);
            form2.Show();
        };
        Button student_reg = new Button();
        student_reg.Text = "Student Register";
        student_reg.Location = new System.Drawing.Point(20, 120);
        student_reg.Width = 130;
        student_reg.Click += (sender, e) =>
         {
            // Create and show Form2 in the panel
            StudentRegister form2 = new StudentRegister();
            form2.TopLevel = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.Dock = DockStyle.Fill;

            // Clear existing controls in the panel
            dynamicPanel.Controls.Clear();
            dynamicPanel.Controls.Add(form2);
            form2.Show();
        };

        Button attendence_mark = new Button();
        attendence_mark.Text = "Attendance Mark";
        attendence_mark.Location = new System.Drawing.Point(20, 150);
        attendence_mark.Width = 130;
        attendence_mark.Click += (sender, e) =>
        {
            Attendence form2 = new Attendence();
            form2.TopLevel = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.Dock = DockStyle.Fill;

            // Clear existing controls in the panel
            dynamicPanel.Controls.Clear();
            dynamicPanel.Controls.Add(form2);
            form2.Show();
        };

        Button payment = new Button();
        payment.Text = "Payment";
        payment.Location = new System.Drawing.Point(20, 180);
        payment.Width = 130;
        payment.Click += (sender, e) =>
        {
            Login form2 = new Login();
            form2.Show();
        };

        Button student = new Button();
        student.Text = "Student";
        student.Location = new System.Drawing.Point(20, 210);
        student.Width = 130;
        student.Click += (sender, e) =>
        {
            Student form2 = new Student();
            this.Hide();
            form2.Show();
        };

        Button attendence_check = new Button();
        attendence_check.Text = "Attendence Check";
        attendence_check.Location = new System.Drawing.Point(20, 240);
        attendence_check.Width = 130;
        attendence_check.Click += (sender, e) =>
        {
            Login form2 = new Login();
            form2.Show();
        };
        groupBox1.Controls.Add(institute_reg);
        groupBox1.Controls.Add(add_course);
        groupBox1.Controls.Add(course_reg);
        groupBox1.Controls.Add(student_reg);
        groupBox1.Controls.Add(attendence_mark);
        groupBox1.Controls.Add(payment);
        groupBox1.Controls.Add(student);
        groupBox1.Controls.Add(attendence_check);
        groupBox1.Controls.Add(Logout);

        this.Controls.Add(groupBox1);


    }
}
