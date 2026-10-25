using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class StudentResultsForm : Form
    {
        private DatabaseHelper dbHelper;
        private int studentId;
        private string studentName;

        private DataGridView dataGridViewResults;
        private Label lblTitle;
        private Label lblUserInfo;
        private Button btnRefresh;
        private Button btnBack;
        private Panel panelHeader;
        private Panel panelButtons;
        private Panel panelGrid;
        private Label lblAverageScore;
        private Label lblTotalExams;

        public StudentResultsForm(int userId, string fullName)
        {
            studentId = userId;
            studentName = fullName;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadResults();
            LoadStatistics();
        }

        private void InitializeComponent()
        {
            this.dataGridViewResults = new DataGridView();
            this.lblTitle = new Label();
            this.lblUserInfo = new Label();
            this.btnRefresh = new Button();
            this.btnBack = new Button();
            this.panelHeader = new Panel();
            this.panelButtons = new Panel();
            this.panelGrid = new Panel();
            this.lblAverageScore = new Label();
            this.lblTotalExams = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelGrid.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = Color.FromArgb(52, 73, 94);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblUserInfo);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 80);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(350, 37);
            this.lblTitle.Text = "Мои результаты экзаменов";

            // lblUserInfo
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new Font("Segoe UI", 12F);
            this.lblUserInfo.ForeColor = Color.White;
            this.lblUserInfo.Location = new System.Drawing.Point(20, 55);

            // panelButtons
            this.panelButtons.BackColor = Color.FromArgb(236, 240, 241);
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Controls.Add(this.btnBack);
            this.panelButtons.Controls.Add(this.lblAverageScore);
            this.panelButtons.Controls.Add(this.lblTotalExams);
            this.panelButtons.Dock = DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 80);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(900, 60);

            // btnRefresh
            this.btnRefresh.BackColor = Color.FromArgb(52, 152, 219);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(700, 12);
            this.btnRefresh.Size = new System.Drawing.Size(130, 36);
            this.btnRefresh.Text = "🔄 Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // btnBack (кнопка Назад)
            this.btnBack.BackColor = Color.FromArgb(231, 76, 60);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Location = new System.Drawing.Point(840, 12);
            this.btnBack.Size = new System.Drawing.Size(50, 36);
            this.btnBack.Text = "✖";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // lblAverageScore
            this.lblAverageScore.AutoSize = true;
            this.lblAverageScore.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblAverageScore.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblAverageScore.Location = new System.Drawing.Point(20, 20);

            // lblTotalExams
            this.lblTotalExams.AutoSize = true;
            this.lblTotalExams.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblTotalExams.ForeColor = Color.FromArgb(52, 152, 219);
            this.lblTotalExams.Location = new System.Drawing.Point(350, 20);

            // panelGrid
            this.panelGrid.Controls.Add(this.dataGridViewResults);
            this.panelGrid.Dock = DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 140);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new Padding(10);
            this.panelGrid.Size = new System.Drawing.Size(900, 400);

            // dataGridViewResults
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.BackgroundColor = Color.White;
            this.dataGridViewResults.BorderStyle = BorderStyle.None;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Dock = DockStyle.Fill;
            this.dataGridViewResults.Location = new System.Drawing.Point(10, 10);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.RowHeadersVisible = false;
            this.dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.Size = new System.Drawing.Size(880, 380);
            this.dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridViewResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // StudentResultsForm
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelHeader);
            this.Name = "StudentResultsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Мои результаты";

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void LoadResults()
        {
            try
            {
                DataTable results = dbHelper.GetExamResultsByStudent(studentId);
                dataGridViewResults.DataSource = results;

                if (dataGridViewResults.Columns.Contains("ResultID"))
                    dataGridViewResults.Columns["ResultID"].Visible = false;

                if (dataGridViewResults.Columns.Contains("TicketNumber"))
                    dataGridViewResults.Columns["TicketNumber"].HeaderText = "Номер билета";

                if (dataGridViewResults.Columns.Contains("Score"))
                    dataGridViewResults.Columns["Score"].HeaderText = "Оценка";

                if (dataGridViewResults.Columns.Contains("Answers"))
                    dataGridViewResults.Columns["Answers"].HeaderText = "Ответы";

                if (dataGridViewResults.Columns.Contains("ExamDate"))
                    dataGridViewResults.Columns["ExamDate"].HeaderText = "Дата экзамена";

                dataGridViewResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewResults.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                lblUserInfo.Text = $"Студент: {studentName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки результатов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                DataTable results = dbHelper.GetExamResultsByStudent(studentId);

                int totalExams = results.Rows.Count;
                int totalScore = 0;
                int maxScore = 0;

                foreach (DataRow row in results.Rows)
                {
                    int score = Convert.ToInt32(row["Score"]);
                    totalScore += score;
                    if (score > maxScore) maxScore = score;
                }

                double averageScore = totalExams > 0 ? (double)totalScore / totalExams : 0;

                lblTotalExams.Text = $"📊 Всего экзаменов: {totalExams}";
                lblAverageScore.Text = $"⭐ Средний балл: {averageScore:F1} | 🏆 Лучший результат: {maxScore}";

                if (totalExams == 0)
                {
                    lblAverageScore.Text = "⭐ Нет сданных экзаменов";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статистики: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadResults();
            LoadStatistics();
            MessageBox.Show("Результаты обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}