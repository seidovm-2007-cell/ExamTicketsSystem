using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class AddUserForm : Form
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
        private Button btnAdd;
        private Button btnCancel;

        public AddUserForm()
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
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Size = new System.Drawing.Size(250, 30);
            this.lblTitle.Text = "Добавление пользователя";

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
            this.txtFullName.Size = new System.Drawing.Size(250, 27);

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(160, 107);
            this.txtUsername.Size = new System.Drawing.Size(250, 27);

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(160, 147);
            this.txtPassword.Size = new System.Drawing.Size(250, 27);
            this.txtPassword.UseSystemPasswordChar = true;

            // txtConfirmPassword
            this.txtConfirmPassword.Location = new System.Drawing.Point(160, 187);
            this.txtConfirmPassword.Size = new System.Drawing.Size(250, 27);
            this.txtConfirmPassword.UseSystemPasswordChar = true;

            // cmbRole
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Student", "Teacher" });
            this.cmbRole.Location = new System.Drawing.Point(160, 227);
            this.cmbRole.Size = new System.Drawing.Size(250, 28);
            this.cmbRole.SelectedIndex = 0;

            // btnAdd
            this.btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(80, 280);
            this.btnAdd.Size = new System.Drawing.Size(150, 40);
            this.btnAdd.Text = "➕ Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(250, 280);
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // AddUserForm
            this.ClientSize = new System.Drawing.Size(450, 360);
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
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Name = "AddUserForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Добавление пользователя";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string role = cmbRole.SelectedItem.ToString();

            // Проверка заполнения
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка совпадения паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка длины пароля
            if (password.Length < 4)
            {
                MessageBox.Show("Пароль должен содержать не менее 4 символов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Регистрация пользователя
            if (dbHelper.RegisterUser(username, password, fullName, role))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}