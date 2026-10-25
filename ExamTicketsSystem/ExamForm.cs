using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class ExamForm : Form
    {
        private DatabaseHelper dbHelper;
        private int studentId;
        private string studentName;
        private DataRow currentTicket;
        private int currentTicketId;
        private int currentTicketNumber;

        private Label lblTitle;
        private Label lblTicketInfo;
        private Label lblQuestion1;
        private Label lblQuestion2;
        private Label lblQuestion3;
        private TextBox txtAnswer1;
        private TextBox txtAnswer2;
        private TextBox txtAnswer3;
        private Button btnNextTicket;
        private Button btnSubmit;
        private Button btnBack;
        private Button btnMyResults;
        private Label lblStatus;

        public ExamForm(int userId, string fullName)
        {
            studentId = userId;
            studentName = fullName;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadRandomTicket();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblTicketInfo = new Label();
            this.lblQuestion1 = new Label();
            this.lblQuestion2 = new Label();
            this.lblQuestion3 = new Label();
            this.txtAnswer1 = new TextBox();
            this.txtAnswer2 = new TextBox();
            this.txtAnswer3 = new TextBox();
            this.btnNextTicket = new Button();
            this.btnSubmit = new Button();
            this.btnBack = new Button();
            this.btnMyResults = new Button();
            this.lblStatus = new Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(250, 20);
            this.lblTitle.Size = new System.Drawing.Size(200, 32);
            this.lblTitle.Text = "Сдача экзамена";

            // lblTicketInfo
            this.lblTicketInfo.AutoSize = true;
            this.lblTicketInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTicketInfo.Location = new System.Drawing.Point(30, 70);

            // lblQuestion1
            this.lblQuestion1.AutoSize = true;
            this.lblQuestion1.Font = new Font("Segoe UI", 11F);
            this.lblQuestion1.Location = new System.Drawing.Point(30, 110);

            // txtAnswer1
            this.txtAnswer1.Font = new Font("Segoe UI", 11F);
            this.txtAnswer1.Location = new System.Drawing.Point(30, 140);
            this.txtAnswer1.Size = new System.Drawing.Size(650, 27);
            this.txtAnswer1.Multiline = true;
            this.txtAnswer1.Height = 60;

            // lblQuestion2
            this.lblQuestion2.AutoSize = true;
            this.lblQuestion2.Font = new Font("Segoe UI", 11F);
            this.lblQuestion2.Location = new System.Drawing.Point(30, 220);

            // txtAnswer2
            this.txtAnswer2.Font = new Font("Segoe UI", 11F);
            this.txtAnswer2.Location = new System.Drawing.Point(30, 250);
            this.txtAnswer2.Size = new System.Drawing.Size(650, 27);
            this.txtAnswer2.Multiline = true;
            this.txtAnswer2.Height = 60;

            // lblQuestion3
            this.lblQuestion3.AutoSize = true;
            this.lblQuestion3.Font = new Font("Segoe UI", 11F);
            this.lblQuestion3.Location = new System.Drawing.Point(30, 330);

            // txtAnswer3
            this.txtAnswer3.Font = new Font("Segoe UI", 11F);
            this.txtAnswer3.Location = new System.Drawing.Point(30, 360);
            this.txtAnswer3.Size = new System.Drawing.Size(650, 27);
            this.txtAnswer3.Multiline = true;
            this.txtAnswer3.Height = 60;

            // btnNextTicket
            this.btnNextTicket.BackColor = Color.FromArgb(52, 152, 219);
            this.btnNextTicket.FlatStyle = FlatStyle.Flat;
            this.btnNextTicket.ForeColor = Color.White;
            this.btnNextTicket.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnNextTicket.Location = new System.Drawing.Point(30, 450);
            this.btnNextTicket.Size = new System.Drawing.Size(150, 40);
            this.btnNextTicket.Text = "Следующий билет";
            this.btnNextTicket.UseVisualStyleBackColor = false;
            this.btnNextTicket.Click += new EventHandler(this.btnNextTicket_Click);

            // btnSubmit
            this.btnSubmit.BackColor = Color.FromArgb(46, 204, 113);
            this.btnSubmit.FlatStyle = FlatStyle.Flat;
            this.btnSubmit.ForeColor = Color.White;
            this.btnSubmit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(200, 450);
            this.btnSubmit.Size = new System.Drawing.Size(150, 40);
            this.btnSubmit.Text = "Завершить экзамен";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);

            // btnMyResults
            this.btnMyResults.BackColor = Color.FromArgb(149, 165, 166);
            this.btnMyResults.FlatStyle = FlatStyle.Flat;
            this.btnMyResults.ForeColor = Color.White;
            this.btnMyResults.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnMyResults.Location = new System.Drawing.Point(370, 450);
            this.btnMyResults.Size = new System.Drawing.Size(150, 40);
            this.btnMyResults.Text = "Мои результаты";
            this.btnMyResults.UseVisualStyleBackColor = false;
            this.btnMyResults.Click += new EventHandler(this.btnMyResults_Click);

            // btnBack
            this.btnBack.BackColor = Color.FromArgb(231, 76, 60);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(530, 450);
            this.btnBack.Size = new System.Drawing.Size(150, 40);
            this.btnBack.Text = "◀ Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(30, 510);
            this.lblStatus.ForeColor = Color.Gray;

            // ExamForm
            this.ClientSize = new System.Drawing.Size(714, 561);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTicketInfo);
            this.Controls.Add(this.lblQuestion1);
            this.Controls.Add(this.txtAnswer1);
            this.Controls.Add(this.lblQuestion2);
            this.Controls.Add(this.txtAnswer2);
            this.Controls.Add(this.lblQuestion3);
            this.Controls.Add(this.txtAnswer3);
            this.Controls.Add(this.btnNextTicket);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnMyResults);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblStatus);
            this.Name = "ExamForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Экзамен";
            this.FormClosing += new FormClosingEventHandler(this.ExamForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadRandomTicket()
        {
            DataTable ticket = dbHelper.GetRandomTicket();
            if (ticket.Rows.Count > 0)
            {
                currentTicket = ticket.Rows[0];
                currentTicketId = Convert.ToInt32(currentTicket["TicketID"]);
                currentTicketNumber = Convert.ToInt32(currentTicket["TicketNumber"]);

                lblTicketInfo.Text = $"Билет №{currentTicketNumber}";
                lblQuestion1.Text = $"1. {currentTicket["Question1"]}";
                lblQuestion2.Text = $"2. {currentTicket["Question2"]}";

                if (currentTicket["Question3"] != DBNull.Value && !string.IsNullOrEmpty(currentTicket["Question3"].ToString()))
                    lblQuestion3.Text = $"3. {currentTicket["Question3"]}";
                else
                    lblQuestion3.Text = "3. Нет вопроса";

                txtAnswer1.Clear();
                txtAnswer2.Clear();
                txtAnswer3.Clear();
                lblStatus.Text = "Введите ответы и нажмите 'Следующий билет'";
            }
            else
            {
                MessageBox.Show("Нет доступных билетов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveCurrentResult()
        {
            string answers = $"Вопрос 1: {txtAnswer1.Text}\nВопрос 2: {txtAnswer2.Text}\nВопрос 3: {txtAnswer3.Text}";
            int score = CalculateScore();

            if (dbHelper.SaveExamResult(studentId, currentTicketId, answers, score))
            {
                lblStatus.Text = $"Билет №{currentTicketNumber} сохранен! Оценка: {score}";
            }
        }

        private int CalculateScore()
        {
            int score = 0;
            if (!string.IsNullOrWhiteSpace(txtAnswer1.Text)) score += 10;
            if (!string.IsNullOrWhiteSpace(txtAnswer2.Text)) score += 10;
            if (!string.IsNullOrWhiteSpace(txtAnswer3.Text)) score += 10;
            return score;
        }

        private void btnNextTicket_Click(object sender, EventArgs e)
        {
            SaveCurrentResult();
            LoadRandomTicket();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveCurrentResult();
            MessageBox.Show("Экзамен завершен! Спасибо!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnToLogin();
        }

        private void btnMyResults_Click(object sender, EventArgs e)
        {
            StudentResultsForm resultsForm = new StudentResultsForm(studentId, studentName);
            resultsForm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из экзамена? Прогресс будет сохранен.",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveCurrentResult();
                ReturnToLogin();
            }
        }

        private void ReturnToLogin()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void ExamForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ReturnToLogin();
            }
        }
    }
}