using System;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class RegisterForm : Form
    {
        private DatabaseHelper dbHelper;
        private Label lblTitle;
        private Label lblFullName;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirmPassword;
        private Label lblRole;
        private TextBox txtFullName;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private ComboBox cmbRole;
        private Button btnRegister;
        private Button btnBack;

        public RegisterForm()
        {
            dbHelper = new DatabaseHelper();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblFullName = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblConfirmPassword = new Label();
            this.lblRole = new Label();
            this.txtFullName = new TextBox();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.cmbRole = new ComboBox();
            this.btnRegister = new Button();
            this.btnBack = new Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Size = new System.Drawing.Size(180, 30);
            this.lblTitle.Text = "Регистрация";

            // lblFullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(30, 70);
            this.lblFullName.Size = new System.Drawing.Size(43, 20);
            this.lblFullName.Text = "ФИО:";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(30, 110);
            this.lblUsername.Size = new System.Drawing.Size(56, 20);
            this.lblUsername.Text = "Логин:";

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 150);
            this.lblPassword.Size = new System.Drawing.Size(62, 20);
            this.lblPassword.Text = "Пароль:";

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(30, 190);
            this.lblConfirmPassword.Size = new System.Drawing.Size(120, 20);
            this.lblConfirmPassword.Text = "Подтверждение:";

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(30, 230);
            this.lblRole.Size = new System.Drawing.Size(43, 20);
            this.lblRole.Text = "Роль:";

            // txtFullName
            this.txtFullName.Location = new System.Drawing.Point(160, 67);
            this.txtFullName.Size = new System.Drawing.Size(200, 27);

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(160, 107);
            this.txtUsername.Size = new System.Drawing.Size(200, 27);

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(160, 147);
            this.txtPassword.Size = new System.Drawing.Size(200, 27);
            this.txtPassword.UseSystemPasswordChar = true;

            // txtConfirmPassword
            this.txtConfirmPassword.Location = new System.Drawing.Point(160, 187);
            this.txtConfirmPassword.Size = new System.Drawing.Size(200, 27);
            this.txtConfirmPassword.UseSystemPasswordChar = true;

            // cmbRole
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Student", "Teacher" });
            this.cmbRole.Location = new System.Drawing.Point(160, 227);
            this.cmbRole.Size = new System.Drawing.Size(200, 28);
            this.cmbRole.SelectedIndex = 0;

            // btnRegister
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(80, 280);
            this.btnRegister.Size = new System.Drawing.Size(120, 40);
            this.btnRegister.Text = "Зарегистрировать";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

            // btnBack
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(220, 280);
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.Text = "◀ Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // RegisterForm
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnBack);
            this.Name = "RegisterForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dbHelper.RegisterUser(username, password, fullName, role))
            {
                MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}