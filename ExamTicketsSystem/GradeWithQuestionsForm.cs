using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class GradeWithQuestionsForm : Form
    {
        private DatabaseHelper dbHelper;
        private int resultId;
        private string studentName;
        private int currentScore;

        private string question1;
        private string question2;
        private string question3;
        private string answer1;
        private string answer2;
        private string answer3;

        private Label lblTitle;
        private Label lblStudentInfo;
        private Label lblCurrentScore;
        private Label lblNewScore;
        private NumericUpDown nudScore;

        private GroupBox groupBox1;
        private Label lblQuestion1;
        private TextBox txtQuestion1;
        private Label lblAnswer1;
        private TextBox txtAnswer1;

        private GroupBox groupBox2;
        private Label lblQuestion2;
        private TextBox txtQuestion2;
        private Label lblAnswer2;
        private TextBox txtAnswer2;

        private GroupBox groupBox3;
        private Label lblQuestion3;
        private TextBox txtQuestion3;
        private Label lblAnswer3;
        private TextBox txtAnswer3;

        private Button btnSave;
        private Button btnCancel;

        public GradeWithQuestionsForm(int id, string name, int score,
            string q1, string q2, string q3,
            string a1, string a2, string a3)
        {
            resultId = id;
            studentName = name;
            currentScore = score;
            question1 = q1;
            question2 = q2;
            question3 = q3;
            answer1 = a1;
            answer2 = a2;
            answer3 = a3;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblStudentInfo = new Label();
            this.lblCurrentScore = new Label();
            this.lblNewScore = new Label();
            this.nudScore = new NumericUpDown();

            this.groupBox1 = new GroupBox();
            this.txtQuestion1 = new TextBox();
            this.lblQuestion1 = new Label();
            this.txtAnswer1 = new TextBox();
            this.lblAnswer1 = new Label();

            this.groupBox2 = new GroupBox();
            this.txtQuestion2 = new TextBox();
            this.lblQuestion2 = new Label();
            this.txtAnswer2 = new TextBox();
            this.lblAnswer2 = new Label();

            this.groupBox3 = new GroupBox();
            this.txtQuestion3 = new TextBox();
            this.lblQuestion3 = new Label();
            this.txtAnswer3 = new TextBox();
            this.lblAnswer3 = new Label();

            this.btnSave = new Button();
            this.btnCancel = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(180, 20);
            this.lblTitle.Size = new System.Drawing.Size(300, 32);
            this.lblTitle.Text = "Проверка экзамена";

            // lblStudentInfo
            this.lblStudentInfo.AutoSize = true;
            this.lblStudentInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblStudentInfo.Location = new System.Drawing.Point(20, 70);
            this.lblStudentInfo.Size = new System.Drawing.Size(0, 0);

            // lblCurrentScore
            this.lblCurrentScore.AutoSize = true;
            this.lblCurrentScore.Font = new Font("Segoe UI", 11F);
            this.lblCurrentScore.Location = new System.Drawing.Point(20, 105);
            this.lblCurrentScore.Text = "Текущая оценка: ";

            // lblNewScore
            this.lblNewScore.AutoSize = true;
            this.lblNewScore.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblNewScore.Location = new System.Drawing.Point(20, 140);
            this.lblNewScore.Text = "Новая оценка (0-100):";

            // nudScore
            this.nudScore.Font = new Font("Segoe UI", 11F);
            this.nudScore.Location = new System.Drawing.Point(220, 137);
            this.nudScore.Minimum = 0;
            this.nudScore.Maximum = 100;
            this.nudScore.Size = new System.Drawing.Size(80, 27);
            this.nudScore.Value = currentScore;

            // ==================== ВОПРОС 1 ====================
            this.groupBox1.Text = "Вопрос 1 (Теория)";
            this.groupBox1.Location = new System.Drawing.Point(20, 180);
            this.groupBox1.Size = new System.Drawing.Size(700, 150);
            this.groupBox1.TabIndex = 0;

            this.lblQuestion1.AutoSize = true;
            this.lblQuestion1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblQuestion1.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblQuestion1.Location = new System.Drawing.Point(10, 25);
            this.lblQuestion1.Text = "Вопрос:";

            this.txtQuestion1.Font = new Font("Segoe UI", 10F);
            this.txtQuestion1.Location = new System.Drawing.Point(10, 50);
            this.txtQuestion1.Size = new System.Drawing.Size(680, 40);
            this.txtQuestion1.Multiline = true;
            this.txtQuestion1.ReadOnly = true;
            this.txtQuestion1.BackColor = Color.FromArgb(240, 248, 255);

            this.lblAnswer1.AutoSize = true;
            this.lblAnswer1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAnswer1.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblAnswer1.Location = new System.Drawing.Point(10, 95);
            this.lblAnswer1.Text = "Ответ студента:";

            this.txtAnswer1.Font = new Font("Segoe UI", 10F);
            this.txtAnswer1.Location = new System.Drawing.Point(10, 120);
            this.txtAnswer1.Size = new System.Drawing.Size(680, 40);
            this.txtAnswer1.Multiline = true;
            this.txtAnswer1.ReadOnly = true;
            this.txtAnswer1.BackColor = Color.FromArgb(255, 255, 225);

            this.groupBox1.Controls.Add(this.lblQuestion1);
            this.groupBox1.Controls.Add(this.txtQuestion1);
            this.groupBox1.Controls.Add(this.lblAnswer1);
            this.groupBox1.Controls.Add(this.txtAnswer1);

            // ==================== ВОПРОС 2 ====================
            this.groupBox2.Text = "Вопрос 2 (Теория)";
            this.groupBox2.Location = new System.Drawing.Point(20, 340);
            this.groupBox2.Size = new System.Drawing.Size(700, 150);
            this.groupBox2.TabIndex = 1;

            this.lblQuestion2.AutoSize = true;
            this.lblQuestion2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblQuestion2.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblQuestion2.Location = new System.Drawing.Point(10, 25);
            this.lblQuestion2.Text = "Вопрос:";

            this.txtQuestion2.Font = new Font("Segoe UI", 10F);
            this.txtQuestion2.Location = new System.Drawing.Point(10, 50);
            this.txtQuestion2.Size = new System.Drawing.Size(680, 40);
            this.txtQuestion2.Multiline = true;
            this.txtQuestion2.ReadOnly = true;
            this.txtQuestion2.BackColor = Color.FromArgb(240, 248, 255);

            this.lblAnswer2.AutoSize = true;
            this.lblAnswer2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAnswer2.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblAnswer2.Location = new System.Drawing.Point(10, 95);
            this.lblAnswer2.Text = "Ответ студента:";

            this.txtAnswer2.Font = new Font("Segoe UI", 10F);
            this.txtAnswer2.Location = new System.Drawing.Point(10, 120);
            this.txtAnswer2.Size = new System.Drawing.Size(680, 40);
            this.txtAnswer2.Multiline = true;
            this.txtAnswer2.ReadOnly = true;
            this.txtAnswer2.BackColor = Color.FromArgb(255, 255, 225);

            this.groupBox2.Controls.Add(this.lblQuestion2);
            this.groupBox2.Controls.Add(this.txtQuestion2);
            this.groupBox2.Controls.Add(this.lblAnswer2);
            this.groupBox2.Controls.Add(this.txtAnswer2);

            // ==================== ВОПРОС 3 ====================
            this.groupBox3.Text = "Вопрос 3 (Практика)";
            this.groupBox3.Location = new System.Drawing.Point(20, 500);
            this.groupBox3.Size = new System.Drawing.Size(700, 150);
            this.groupBox3.TabIndex = 2;

            this.lblQuestion3.AutoSize = true;
            this.lblQuestion3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblQuestion3.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblQuestion3.Location = new System.Drawing.Point(10, 25);
            this.lblQuestion3.Text = "Вопрос:";

            this.txtQuestion3.Font = new Font("Segoe UI", 10F);
            this.txtQuestion3.Location = new System.Drawing.Point(10, 50);
            this.txtQuestion3.Size = new System.Drawing.Size(680, 40);
            this.txtQuestion3.Multiline = true;
            this.txtQuestion3.ReadOnly = true;
            this.txtQuestion3.BackColor = Color.FromArgb(240, 248, 255);

            this.lblAnswer3.AutoSize = true;
            this.lblAnswer3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAnswer3.ForeColor = Color.FromArgb(46, 204, 113);
            this.lblAnswer3.Location = new System.Drawing.Point(10, 95);
            this.lblAnswer3.Text = "Ответ студента:";

            this.txtAnswer3.Font = new Font("Segoe UI", 10F);
            this.txtAnswer3.Location = new System.Drawing.Point(10, 120);
            this.txtAnswer3.Size = new System.Drawing.Size(680, 40);
            this.txtAnswer3.Multiline = true;
            this.txtAnswer3.ReadOnly = true;
            this.txtAnswer3.BackColor = Color.FromArgb(255, 255, 225);

            this.groupBox3.Controls.Add(this.lblQuestion3);
            this.groupBox3.Controls.Add(this.txtQuestion3);
            this.groupBox3.Controls.Add(this.lblAnswer3);
            this.groupBox3.Controls.Add(this.txtAnswer3);

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(200, 670);
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.Text = "💾 Сохранить оценку";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(380, 670);
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // GradeWithQuestionsForm
            this.ClientSize = new System.Drawing.Size(750, 740);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStudentInfo);
            this.Controls.Add(this.lblCurrentScore);
            this.Controls.Add(this.lblNewScore);
            this.Controls.Add(this.nudScore);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "GradeWithQuestionsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Проверка экзамена студента";
            this.FormClosing += new FormClosingEventHandler(this.GradeWithQuestionsForm_FormClosing);

            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadData()
        {
            lblStudentInfo.Text = $"Студент: {studentName}";
            lblCurrentScore.Text = $"Текущая оценка: {currentScore} баллов";

            txtQuestion1.Text = question1;
            txtQuestion2.Text = question2;
            txtQuestion3.Text = question3;

            txtAnswer1.Text = answer1;
            txtAnswer2.Text = answer2;
            txtAnswer3.Text = answer3;

            // Подсветка текущей оценки
            if (currentScore >= 90)
                lblCurrentScore.ForeColor = Color.Green;
            else if (currentScore >= 70)
                lblCurrentScore.ForeColor = Color.DarkOrange;
            else if (currentScore >= 50)
                lblCurrentScore.ForeColor = Color.Orange;
            else if (currentScore > 0)
                lblCurrentScore.ForeColor = Color.Red;
            else
                lblCurrentScore.ForeColor = Color.Gray;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int newScore = (int)nudScore.Value;

            if (dbHelper.UpdateExamResultScore(resultId, newScore))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении оценки!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GradeWithQuestionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ничего не делаем
        }
    }
}