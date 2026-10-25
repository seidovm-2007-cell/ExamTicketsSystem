using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class GradeForm : Form
    {
        private DatabaseHelper dbHelper;
        private int resultId;
        private string studentName;
        private int currentScore;
        private string answers;

        private Label lblTitle;
        private Label lblStudentInfo;
        private Label lblCurrentScore;
        private Label lblNewScore;
        private NumericUpDown nudScore;
        private Label lblAnswersTitle;
        private TextBox txtAnswers;
        private Button btnSave;
        private Button btnCancel;
        private Panel panelAnswers;

        public GradeForm(int id, string name, int score, string studentAnswers)
        {
            resultId = id;
            studentName = name;
            currentScore = score;
            answers = studentAnswers;
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
            this.lblAnswersTitle = new Label();
            this.txtAnswers = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.panelAnswers = new Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).BeginInit();
            this.panelAnswers.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Size = new System.Drawing.Size(250, 32);
            this.lblTitle.Text = "Выставление оценки";

            // lblStudentInfo
            this.lblStudentInfo.AutoSize = true;
            this.lblStudentInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblStudentInfo.Location = new System.Drawing.Point(20, 70);
            this.lblStudentInfo.Text = "Студент: ";

            // lblCurrentScore
            this.lblCurrentScore.AutoSize = true;
            this.lblCurrentScore.Font = new Font("Segoe UI", 11F);
            this.lblCurrentScore.Location = new System.Drawing.Point(20, 110);
            this.lblCurrentScore.Text = "Текущая оценка: ";

            // lblNewScore
            this.lblNewScore.AutoSize = true;
            this.lblNewScore.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblNewScore.Location = new System.Drawing.Point(20, 150);
            this.lblNewScore.Text = "Новая оценка (0-100):";

            // nudScore
            this.nudScore.Font = new Font("Segoe UI", 11F);
            this.nudScore.Location = new System.Drawing.Point(220, 147);
            this.nudScore.Minimum = 0;
            this.nudScore.Maximum = 100;
            this.nudScore.Size = new System.Drawing.Size(80, 27);
            this.nudScore.Value = currentScore;

            // lblAnswersTitle
            this.lblAnswersTitle.AutoSize = true;
            this.lblAnswersTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblAnswersTitle.Location = new System.Drawing.Point(20, 200);
            this.lblAnswersTitle.Size = new System.Drawing.Size(140, 20);
            this.lblAnswersTitle.Text = "Ответы студента:";

            // panelAnswers
            this.panelAnswers.Controls.Add(this.txtAnswers);
            this.panelAnswers.Location = new System.Drawing.Point(20, 230);
            this.panelAnswers.Size = new System.Drawing.Size(540, 250);
            this.panelAnswers.BorderStyle = BorderStyle.FixedSingle;

            // txtAnswers
            this.txtAnswers.Dock = DockStyle.Fill;
            this.txtAnswers.Font = new Font("Consolas", 10F);
            this.txtAnswers.Multiline = true;
            this.txtAnswers.ReadOnly = true;
            this.txtAnswers.ScrollBars = ScrollBars.Vertical;
            this.txtAnswers.BackColor = Color.WhiteSmoke;

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(120, 500);
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.Text = "💾 Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(300, 500);
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // GradeForm
            this.ClientSize = new System.Drawing.Size(600, 570);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStudentInfo);
            this.Controls.Add(this.lblCurrentScore);
            this.Controls.Add(this.lblNewScore);
            this.Controls.Add(this.nudScore);
            this.Controls.Add(this.lblAnswersTitle);
            this.Controls.Add(this.panelAnswers);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "GradeForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Оценивание студента";

            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).EndInit();
            this.panelAnswers.ResumeLayout(false);
            this.panelAnswers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadData()
        {
            lblStudentInfo.Text = $"Студент: {studentName}";
            lblCurrentScore.Text = $"Текущая оценка: {currentScore} баллов";
            txtAnswers.Text = answers;

            // Подсветка текущей оценки
            if (currentScore >= 90)
                lblCurrentScore.ForeColor = Color.Green;
            else if (currentScore >= 70)
                lblCurrentScore.ForeColor = Color.DarkOrange;
            else if (currentScore >= 50)
                lblCurrentScore.ForeColor = Color.Orange;
            else
                lblCurrentScore.ForeColor = Color.Red;
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
    }
}