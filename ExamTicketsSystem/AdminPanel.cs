using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class AdminPanel : Form
    {
        private DatabaseHelper dbHelper;
        private int adminId;
        private string adminName;

        private DataGridView dataGridViewUsers;
        private DataGridView dataGridViewResults;
        private Button btnManageTickets;
        private Button btnRefreshUsers;
        private Button btnRefreshResults;
        private Button btnAddUser;
        private Button btnDeleteUser;
        private Button btnBack;
        private Label lblTitle;
        private Label lblUserInfo;
        private TabControl tabControl;
        private TabPage tabPageUsers;
        private TabPage tabPageResults;

        public AdminPanel(int userId, string fullName)
        {
            adminId = userId;
            adminName = fullName;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadUsers();
            LoadResults();
        }

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabPageUsers = new TabPage();
            this.dataGridViewUsers = new DataGridView();
            this.tabPageResults = new TabPage();
            this.dataGridViewResults = new DataGridView();
            this.btnManageTickets = new Button();
            this.btnRefreshUsers = new Button();
            this.btnRefreshResults = new Button();
            this.btnAddUser = new Button();
            this.btnDeleteUser = new Button();
            this.btnBack = new Button();
            this.lblTitle = new Label();
            this.lblUserInfo = new Label();

            this.tabControl.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.tabPageResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(320, 20);
            this.lblTitle.Size = new System.Drawing.Size(280, 32);
            this.lblTitle.Text = "Панель администратора";

            // lblUserInfo
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Location = new System.Drawing.Point(20, 70);
            this.lblUserInfo.Font = new Font("Segoe UI", 10F);
            this.lblUserInfo.Text = "Загрузка...";

            // tabControl
            this.tabControl.Location = new System.Drawing.Point(20, 100);
            this.tabControl.Size = new System.Drawing.Size(940, 350);
            this.tabControl.TabIndex = 0;

            // tabPageUsers
            this.tabPageUsers.Text = "👥 Пользователи";
            this.tabPageUsers.Controls.Add(this.dataGridViewUsers);

            // dataGridViewUsers
            this.dataGridViewUsers.Dock = DockStyle.Fill;
            this.dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // tabPageResults
            this.tabPageResults.Text = "📊 Результаты";
            this.tabPageResults.Controls.Add(this.dataGridViewResults);

            // dataGridViewResults
            this.dataGridViewResults.Dock = DockStyle.Fill;
            this.dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.tabControl.TabPages.Add(this.tabPageUsers);
            this.tabControl.TabPages.Add(this.tabPageResults);

            // btnManageTickets
            this.btnManageTickets.BackColor = Color.FromArgb(46, 204, 113);
            this.btnManageTickets.FlatStyle = FlatStyle.Flat;
            this.btnManageTickets.ForeColor = Color.White;
            this.btnManageTickets.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnManageTickets.Location = new System.Drawing.Point(20, 470);
            this.btnManageTickets.Size = new System.Drawing.Size(150, 40);
            this.btnManageTickets.Text = "📚 Управление билетами";
            this.btnManageTickets.UseVisualStyleBackColor = false;
            this.btnManageTickets.Click += new EventHandler(this.btnManageTickets_Click);

            // btnRefreshUsers
            this.btnRefreshUsers.BackColor = Color.FromArgb(52, 152, 219);
            this.btnRefreshUsers.FlatStyle = FlatStyle.Flat;
            this.btnRefreshUsers.ForeColor = Color.White;
            this.btnRefreshUsers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefreshUsers.Location = new System.Drawing.Point(180, 470);
            this.btnRefreshUsers.Size = new System.Drawing.Size(130, 40);
            this.btnRefreshUsers.Text = "👥 Обновить";
            this.btnRefreshUsers.UseVisualStyleBackColor = false;
            this.btnRefreshUsers.Click += new EventHandler(this.btnRefreshUsers_Click);

            // btnAddUser (НОВАЯ КНОПКА)
            this.btnAddUser.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAddUser.FlatStyle = FlatStyle.Flat;
            this.btnAddUser.ForeColor = Color.White;
            this.btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAddUser.Location = new System.Drawing.Point(320, 470);
            this.btnAddUser.Size = new System.Drawing.Size(130, 40);
            this.btnAddUser.Text = "➕ Добавить";
            this.btnAddUser.UseVisualStyleBackColor = false;
            this.btnAddUser.Click += new EventHandler(this.btnAddUser_Click);

            // btnDeleteUser (НОВАЯ КНОПКА)
            this.btnDeleteUser.BackColor = Color.FromArgb(231, 76, 60);
            this.btnDeleteUser.FlatStyle = FlatStyle.Flat;
            this.btnDeleteUser.ForeColor = Color.White;
            this.btnDeleteUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDeleteUser.Location = new System.Drawing.Point(460, 470);
            this.btnDeleteUser.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteUser.Text = "🗑 Удалить";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new EventHandler(this.btnDeleteUser_Click);

            // btnRefreshResults
            this.btnRefreshResults.BackColor = Color.FromArgb(149, 165, 166);
            this.btnRefreshResults.FlatStyle = FlatStyle.Flat;
            this.btnRefreshResults.ForeColor = Color.White;
            this.btnRefreshResults.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefreshResults.Location = new System.Drawing.Point(600, 470);
            this.btnRefreshResults.Size = new System.Drawing.Size(130, 40);
            this.btnRefreshResults.Text = "📊 Обновить";
            this.btnRefreshResults.UseVisualStyleBackColor = false;
            this.btnRefreshResults.Click += new EventHandler(this.btnRefreshResults_Click);

            // btnBack
            this.btnBack.BackColor = Color.FromArgb(231, 76, 60);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(780, 470);
            this.btnBack.Size = new System.Drawing.Size(180, 40);
            this.btnBack.Text = "◀ Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // AdminPanel
            this.ClientSize = new System.Drawing.Size(984, 541);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUserInfo);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnManageTickets);
            this.Controls.Add(this.btnRefreshUsers);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnRefreshResults);
            this.Controls.Add(this.btnBack);
            this.Name = "AdminPanel";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Панель администратора";

            this.tabControl.ResumeLayout(false);
            this.tabPageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.tabPageResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadUsers()
        {
            try
            {
                string query = "SELECT UserID, Username, FullName, Role, CreatedAt FROM Users ORDER BY UserID";
                using (SqlConnection conn = dbHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewUsers.DataSource = dt;

                    if (dataGridViewUsers.Columns.Contains("UserID"))
                        dataGridViewUsers.Columns["UserID"].Visible = false;

                    // Настройка ширины колонок
                    if (dataGridViewUsers.Columns.Contains("Username"))
                        dataGridViewUsers.Columns["Username"].HeaderText = "Логин";
                    if (dataGridViewUsers.Columns.Contains("FullName"))
                        dataGridViewUsers.Columns["FullName"].HeaderText = "ФИО";
                    if (dataGridViewUsers.Columns.Contains("Role"))
                        dataGridViewUsers.Columns["Role"].HeaderText = "Роль";
                    if (dataGridViewUsers.Columns.Contains("CreatedAt"))
                        dataGridViewUsers.Columns["CreatedAt"].HeaderText = "Дата регистрации";
                }
                lblUserInfo.Text = $"Администратор: {adminName} | Пользователей: {dataGridViewUsers.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadResults()
        {
            try
            {
                DataTable results = dbHelper.GetAllExamResults();
                dataGridViewResults.DataSource = results;

                if (dataGridViewResults.Columns.Contains("ResultID"))
                    dataGridViewResults.Columns["ResultID"].Visible = false;

                if (dataGridViewResults.Columns.Contains("StudentName"))
                    dataGridViewResults.Columns["StudentName"].HeaderText = "Студент";
                if (dataGridViewResults.Columns.Contains("TicketNumber"))
                    dataGridViewResults.Columns["TicketNumber"].HeaderText = "Билет №";
                if (dataGridViewResults.Columns.Contains("Score"))
                    dataGridViewResults.Columns["Score"].HeaderText = "Оценка";
                if (dataGridViewResults.Columns.Contains("ExamDate"))
                    dataGridViewResults.Columns["ExamDate"].HeaderText = "Дата";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки результатов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Создаем форму для добавления пользователя
            AddUserForm addUserForm = new AddUserForm();
            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
                MessageBox.Show("Пользователь успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.CurrentRow != null)
            {
                int userId = Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["UserID"].Value);
                string username = dataGridViewUsers.CurrentRow.Cells["Username"].Value.ToString();
                string role = dataGridViewUsers.CurrentRow.Cells["Role"].Value.ToString();

                // Нельзя удалить самого себя
                if (userId == adminId)
                {
                    MessageBox.Show("Вы не можете удалить сами себя!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Нельзя удалить последнего администратора
                if (role == "Admin")
                {
                    int adminCount = GetAdminCount();
                    if (adminCount <= 1)
                    {
                        MessageBox.Show("Нельзя удалить последнего администратора!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя '{username}'?",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (DeleteUser(userId))
                    {
                        LoadUsers();
                        MessageBox.Show("Пользователь успешно удален!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении пользователя!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int GetAdminCount()
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin'";
            using (SqlConnection conn = dbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        private bool DeleteUser(int userId)
        {
            // Сначала удаляем результаты экзаменов пользователя
            string deleteResultsQuery = "DELETE FROM ExamResults WHERE StudentID = @UserID";
            string deleteUserQuery = "DELETE FROM Users WHERE UserID = @UserID";

            using (SqlConnection conn = dbHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Удаляем результаты
                        SqlCommand cmd = new SqlCommand(deleteResultsQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.ExecuteNonQuery();

                        // Удаляем пользователя
                        cmd = new SqlCommand(deleteUserQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private void btnManageTickets_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(adminId, adminName, "Admin");
            mainForm.ShowDialog();
            LoadResults();
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
            MessageBox.Show("Список пользователей обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefreshResults_Click(object sender, EventArgs e)
        {
            LoadResults();
            MessageBox.Show("Результаты обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из панели администратора?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
        }
    }
}