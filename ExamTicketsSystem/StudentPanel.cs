using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace мой_курсовой_проект
{
    public class StudentPanel : Form
    {
        private string connectionString = "Data Source=DESKTOP-BMNSL6Q;Initial Catalog=ExamTicketsDB;Integrated Security=SSPI;TrustServerCertificate=True";
        private DataGridView dataGridViewTickets;
        private Label lblUserInfo;
        private string username;

        public StudentPanel(string username)
        {
            this.username = username;
            this.Text = "Просмотр билетов - Студент";
            this.Size = new System.Drawing.Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            CreateUI();
            LoadTickets();
        }

        private void CreateUI()
        {
            // Информация о пользователе
            lblUserInfo = new Label();
            lblUserInfo.Text = $"Пользователь: {username} (Student)";
            lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            lblUserInfo.Location = new System.Drawing.Point(20, 20);
            lblUserInfo.Size = new System.Drawing.Size(600, 30);

            // Кнопка "Назад"
            Button btnBack = new Button();
            btnBack.Text = "◀ Назад";
            btnBack.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnBack.Location = new System.Drawing.Point(20, 70);
            btnBack.Size = new System.Drawing.Size(120, 35);
            btnBack.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnBack.ForeColor = System.Drawing.Color.White;
            btnBack.Click += (s, e) => { this.Close(); };

            // Таблица билетов
            dataGridViewTickets = new DataGridView();
            dataGridViewTickets.Location = new System.Drawing.Point(20, 120);
            dataGridViewTickets.Size = new System.Drawing.Size(1140, 520);
            dataGridViewTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTickets.ReadOnly = true;
            dataGridViewTickets.AllowUserToAddRows = false;

            // Добавляем все элементы
            this.Controls.Add(lblUserInfo);
            this.Controls.Add(btnBack);
            this.Controls.Add(dataGridViewTickets);
        }

        private void LoadTickets()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TicketID, Question1, Question2, Question3 FROM Tickets ORDER BY TicketID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);

                    dataGridViewTickets.DataSource = dt;

                    dataGridViewTickets.Columns["TicketID"].HeaderText = "Номер билета";
                    dataGridViewTickets.Columns["Question1"].HeaderText = "Вопрос 1";
                    dataGridViewTickets.Columns["Question2"].HeaderText = "Вопрос 2";
                    dataGridViewTickets.Columns["Question3"].HeaderText = "Вопрос 3";

                    lblUserInfo.Text = $"Пользователь: {username} (Student) | Всего билетов: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }
    }
}