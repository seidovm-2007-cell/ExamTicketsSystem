using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class TicketEditForm : Form
    {
        private DatabaseHelper dbHelper;
        private int ticketId = -1;
        private int currentUserId;

        private Label lblTitle;
        private Label lblTicketNumber;
        private TextBox txtTicketNumber;
        private Label lblQuestion1;
        private TextBox txtQuestion1;
        private Label lblQuestion2;
        private TextBox txtQuestion2;
        private Label lblQuestion3;
        private TextBox txtQuestion3;
        private Button btnSave;
        private Button btnCancel;

        public TicketEditForm(int userId)
        {
            currentUserId = userId;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            this.Text = "Добавление билета";
        }

        public TicketEditForm(int id, int userId)
        {
            ticketId = id;
            currentUserId = userId;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadTicketData();
            this.Text = "Редактирование билета";
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblTicketNumber = new Label();
            this.txtTicketNumber = new TextBox();
            this.lblQuestion1 = new Label();
            this.txtQuestion1 = new TextBox();
            this.lblQuestion2 = new Label();
            this.txtQuestion2 = new TextBox();
            this.lblQuestion3 = new Label();
            this.txtQuestion3 = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.Text = "Работа с билетом";

            // lblTicketNumber
            this.lblTicketNumber.AutoSize = true;
            this.lblTicketNumber.Location = new System.Drawing.Point(30, 70);
            this.lblTicketNumber.Size = new System.Drawing.Size(104, 20);
            this.lblTicketNumber.Text = "Номер билета:";

            // txtTicketNumber
            this.txtTicketNumber.Location = new System.Drawing.Point(150, 67);
            this.txtTicketNumber.Size = new System.Drawing.Size(100, 27);

            // lblQuestion1
            this.lblQuestion1.AutoSize = true;
            this.lblQuestion1.Location = new System.Drawing.Point(30, 110);
            this.lblQuestion1.Size = new System.Drawing.Size(78, 20);
            this.lblQuestion1.Text = "Вопрос 1:";

            // txtQuestion1
            this.txtQuestion1.Location = new System.Drawing.Point(30, 140);
            this.txtQuestion1.Size = new System.Drawing.Size(450, 27);
            this.txtQuestion1.Multiline = true;
            this.txtQuestion1.Height = 60;

            // lblQuestion2
            this.lblQuestion2.AutoSize = true;
            this.lblQuestion2.Location = new System.Drawing.Point(30, 220);
            this.lblQuestion2.Size = new System.Drawing.Size(78, 20);
            this.lblQuestion2.Text = "Вопрос 2:";

            // txtQuestion2
            this.txtQuestion2.Location = new System.Drawing.Point(30, 250);
            this.txtQuestion2.Size = new System.Drawing.Size(450, 27);
            this.txtQuestion2.Multiline = true;
            this.txtQuestion2.Height = 60;

            // lblQuestion3
            this.lblQuestion3.AutoSize = true;
            this.lblQuestion3.Location = new System.Drawing.Point(30, 330);
            this.lblQuestion3.Size = new System.Drawing.Size(78, 20);
            this.lblQuestion3.Text = "Вопрос 3:";

            // txtQuestion3
            this.txtQuestion3.Location = new System.Drawing.Point(30, 360);
            this.txtQuestion3.Size = new System.Drawing.Size(450, 27);
            this.txtQuestion3.Multiline = true;
            this.txtQuestion3.Height = 60;

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(120, 450);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel (кнопка Отмена/Назад)
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(270, 450);
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // TicketEditForm
            this.ClientSize = new System.Drawing.Size(514, 521);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTicketNumber);
            this.Controls.Add(this.txtTicketNumber);
            this.Controls.Add(this.lblQuestion1);
            this.Controls.Add(this.txtQuestion1);
            this.Controls.Add(this.lblQuestion2);
            this.Controls.Add(this.txtQuestion2);
            this.Controls.Add(this.lblQuestion3);
            this.Controls.Add(this.txtQuestion3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "TicketEditForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadTicketData()
        {
            DataTable ticket = dbHelper.GetTicketById(ticketId);
            if (ticket.Rows.Count > 0)
            {
                DataRow row = ticket.Rows[0];
                txtTicketNumber.Text = row["TicketNumber"].ToString();
                txtQuestion1.Text = row["Question1"].ToString();
                txtQuestion2.Text = row["Question2"].ToString();
                txtQuestion3.Text = row["Question3"] != DBNull.Value ? row["Question3"].ToString() : "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTicketNumber.Text) ||
                string.IsNullOrWhiteSpace(txtQuestion1.Text) ||
                string.IsNullOrWhiteSpace(txtQuestion2.Text))
            {
                MessageBox.Show("Заполните номер билета и первые два вопроса!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtTicketNumber.Text, out int ticketNumber))
            {
                MessageBox.Show("Номер билета должен быть числом!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string question1 = txtQuestion1.Text.Trim();
            string question2 = txtQuestion2.Text.Trim();
            string question3 = txtQuestion3.Text.Trim();

            bool result;
            if (ticketId == -1)
            {
                result = dbHelper.AddTicket(ticketNumber, question1, question2, question3, currentUserId);
            }
            else
            {
                result = dbHelper.UpdateTicket(ticketId, ticketNumber, question1, question2, question3);
            }

            if (result)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении!", "Ошибка",
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