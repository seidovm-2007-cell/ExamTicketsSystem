using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class TeacherPanel : Form
    {
        private DatabaseHelper dbHelper;
        private int teacherId;
        private string teacherName;

        private DataGridView dataGridViewResults;
        private Button btnViewResults;
        private Button btnManageTickets;
        private Button btnGradeStudents;
        private Button btnRefresh;
        private Button btnBack;
        private Label lblTitle;
        private Label lblUserInfo;

        public TeacherPanel(int userId, string fullName)
        {
            teacherId = userId;
            teacherName = fullName;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadResults();
        }

        private void InitializeComponent()
        {
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.btnViewResults = new System.Windows.Forms.Button();
            this.btnManageTickets = new System.Windows.Forms.Button();
            this.btnGradeStudents = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.ColumnHeadersHeight = 34;
            this.dataGridViewResults.Location = new System.Drawing.Point(20, 100);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.RowHeadersWidth = 62;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.Size = new System.Drawing.Size(1132, 350);
            this.dataGridViewResults.TabIndex = 2;
            this.dataGridViewResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellDoubleClick);
            // 
            // btnViewResults
            // 
            this.btnViewResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnViewResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewResults.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewResults.ForeColor = System.Drawing.Color.White;
            this.btnViewResults.Location = new System.Drawing.Point(25, 470);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(180, 40);
            this.btnViewResults.TabIndex = 3;
            this.btnViewResults.Text = "📊 Обновить результаты";
            this.btnViewResults.UseVisualStyleBackColor = false;
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);
            // 
            // btnManageTickets
            // 
            this.btnManageTickets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnManageTickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageTickets.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageTickets.ForeColor = System.Drawing.Color.White;
            this.btnManageTickets.Location = new System.Drawing.Point(505, 470);
            this.btnManageTickets.Name = "btnManageTickets";
            this.btnManageTickets.Size = new System.Drawing.Size(180, 40);
            this.btnManageTickets.TabIndex = 5;
            this.btnManageTickets.Text = "📚 Управление билетами";
            this.btnManageTickets.UseVisualStyleBackColor = false;
            this.btnManageTickets.Click += new System.EventHandler(this.btnManageTickets_Click);
            // 
            // btnGradeStudents
            // 
            this.btnGradeStudents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGradeStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGradeStudents.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGradeStudents.ForeColor = System.Drawing.Color.White;
            this.btnGradeStudents.Location = new System.Drawing.Point(288, 470);
            this.btnGradeStudents.Name = "btnGradeStudents";
            this.btnGradeStudents.Size = new System.Drawing.Size(180, 40);
            this.btnGradeStudents.TabIndex = 4;
            this.btnGradeStudents.Text = "⭐ Выставить оценку";
            this.btnGradeStudents.UseVisualStyleBackColor = false;
            this.btnGradeStudents.Click += new System.EventHandler(this.btnGradeStudents_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(755, 470);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 40);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "🔄 Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(972, 470);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(180, 40);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "◀ Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(280, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(424, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Панель преподавателя";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUserInfo.Location = new System.Drawing.Point(20, 70);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(104, 28);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "Загрузка...";
            // 
            // TeacherPanel
            // 
            this.ClientSize = new System.Drawing.Size(1176, 541);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUserInfo);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.btnViewResults);
            this.Controls.Add(this.btnGradeStudents);
            this.Controls.Add(this.btnManageTickets);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Name = "TeacherPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Панель преподавателя";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadResults()
        {
            try
            {
                DataTable results = dbHelper.GetAllExamResultsWithQuestions(); // Новый метод с вопросами
                dataGridViewResults.DataSource = results;

                if (dataGridViewResults.Columns.Contains("ResultID"))
                    dataGridViewResults.Columns["ResultID"].Visible = false;

                if (dataGridViewResults.Columns.Contains("StudentName"))
                    dataGridViewResults.Columns["StudentName"].HeaderText = "Студент";

                if (dataGridViewResults.Columns.Contains("TicketNumber"))
                    dataGridViewResults.Columns["TicketNumber"].HeaderText = "Билет №";

                if (dataGridViewResults.Columns.Contains("Question1"))
                    dataGridViewResults.Columns["Question1"].HeaderText = "Вопрос 1";

                if (dataGridViewResults.Columns.Contains("Question2"))
                    dataGridViewResults.Columns["Question2"].HeaderText = "Вопрос 2";

                if (dataGridViewResults.Columns.Contains("Question3"))
                    dataGridViewResults.Columns["Question3"].HeaderText = "Вопрос 3";

                if (dataGridViewResults.Columns.Contains("StudentAnswer1"))
                    dataGridViewResults.Columns["StudentAnswer1"].HeaderText = "Ответ на вопрос 1";

                if (dataGridViewResults.Columns.Contains("StudentAnswer2"))
                    dataGridViewResults.Columns["StudentAnswer2"].HeaderText = "Ответ на вопрос 2";

                if (dataGridViewResults.Columns.Contains("StudentAnswer3"))
                    dataGridViewResults.Columns["StudentAnswer3"].HeaderText = "Ответ на вопрос 3";

                if (dataGridViewResults.Columns.Contains("Score"))
                {
                    dataGridViewResults.Columns["Score"].HeaderText = "Оценка";
                    foreach (DataGridViewRow row in dataGridViewResults.Rows)
                    {
                        if (row.Cells["Score"].Value != null && row.Cells["Score"].Value != DBNull.Value)
                        {
                            int score = Convert.ToInt32(row.Cells["Score"].Value);
                            if (score >= 90)
                                row.Cells["Score"].Style.BackColor = Color.LightGreen;
                            else if (score >= 70)
                                row.Cells["Score"].Style.BackColor = Color.LightYellow;
                            else if (score >= 50)
                                row.Cells["Score"].Style.BackColor = Color.Orange;
                            else if (score > 0)
                                row.Cells["Score"].Style.BackColor = Color.LightPink;
                        }
                    }
                }

                if (dataGridViewResults.Columns.Contains("ExamDate"))
                    dataGridViewResults.Columns["ExamDate"].HeaderText = "Дата сдачи";

                dataGridViewResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewResults.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                lblUserInfo.Text = $"Преподаватель: {teacherName} | Всего результатов: {results.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            LoadResults();
        }

        private void btnGradeStudents_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.CurrentRow != null)
            {
                int resultId = Convert.ToInt32(dataGridViewResults.CurrentRow.Cells["ResultID"].Value);
                string studentName = dataGridViewResults.CurrentRow.Cells["StudentName"].Value.ToString();
                int currentScore = Convert.ToInt32(dataGridViewResults.CurrentRow.Cells["Score"].Value);

                // Получаем вопросы и ответы
                string question1 = dataGridViewResults.CurrentRow.Cells["Question1"].Value?.ToString() ?? "";
                string question2 = dataGridViewResults.CurrentRow.Cells["Question2"].Value?.ToString() ?? "";
                string question3 = dataGridViewResults.CurrentRow.Cells["Question3"].Value?.ToString() ?? "";
                string answer1 = dataGridViewResults.CurrentRow.Cells["StudentAnswer1"].Value?.ToString() ?? "";
                string answer2 = dataGridViewResults.CurrentRow.Cells["StudentAnswer2"].Value?.ToString() ?? "";
                string answer3 = dataGridViewResults.CurrentRow.Cells["StudentAnswer3"].Value?.ToString() ?? "";

                GradeWithQuestionsForm gradeForm = new GradeWithQuestionsForm(
                    resultId, studentName, currentScore,
                    question1, question2, question3,
                    answer1, answer2, answer3);

                if (gradeForm.ShowDialog() == DialogResult.OK)
                {
                    LoadResults();
                    MessageBox.Show("Оценка успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите результат для выставления оценки!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnManageTickets_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(teacherId, teacherName, "Teacher");
            mainForm.ShowDialog();
            LoadResults();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadResults();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из панели преподавателя?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
        }

        private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnGradeStudents_Click(sender, e);
            }
        }
    }
}