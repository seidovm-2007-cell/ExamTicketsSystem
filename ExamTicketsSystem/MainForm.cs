using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public partial class MainForm : Form
    {
        private DatabaseHelper dbHelper;
        private int currentUserId;
        private string userFullName;
        private string userRole;
        private Form previousForm;

        private DataGridView dataGridViewTickets;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnBack;
        private Label lblUserInfo;

        public MainForm(int userId, string fullName, string role)
        {
            currentUserId = userId;
            userFullName = fullName;
            userRole = role;
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            LoadTickets();
        }

        private void InitializeComponent()
        {
            this.dataGridViewTickets = new DataGridView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.btnBack = new Button();
            this.lblUserInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTickets)).BeginInit();
            this.SuspendLayout();

            // dataGridViewTickets
            this.dataGridViewTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewTickets.Location = new Point(12, 50);
            this.dataGridViewTickets.Size = new Size(960, 450);
            this.dataGridViewTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTickets.AllowUserToAddRows = false;
            this.dataGridViewTickets.AllowUserToDeleteRows = false;
            this.dataGridViewTickets.ReadOnly = true;
            this.dataGridViewTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTickets.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewTickets_CellDoubleClick);

            // lblUserInfo
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Location = new Point(12, 15);
            this.lblUserInfo.Font = new Font("Segoe UI", 10F);

            // btnAdd
            this.btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.Location = new Point(12, 510);
            this.btnAdd.Size = new Size(100, 35);
            this.btnAdd.Text = "➕ Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.BackColor = Color.FromArgb(52, 152, 219);
            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnEdit.Location = new Point(120, 510);
            this.btnEdit.Size = new Size(100, 35);
            this.btnEdit.Text = "✏ Редактировать";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.Location = new Point(230, 510);
            this.btnDelete.Size = new Size(100, 35);
            this.btnDelete.Text = "🗑 Удалить";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            if (userRole != "Admin")
            {
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.Gray;
            }

            // btnRefresh
            this.btnRefresh.BackColor = Color.FromArgb(149, 165, 166);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefresh.Location = new Point(340, 510);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.Text = "🔄 Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // btnBack
            this.btnBack.BackColor = Color.FromArgb(231, 76, 60);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBack.Location = new Point(860, 510);
            this.btnBack.Size = new Size(110, 35);
            this.btnBack.Text = "◀ Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // MainForm
            this.ClientSize = new Size(984, 561);
            this.Controls.Add(this.dataGridViewTickets);
            this.Controls.Add(this.lblUserInfo);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Управление билетами";
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadTickets()
        {
            try
            {
                DataTable tickets = dbHelper.GetAllTickets();
                dataGridViewTickets.DataSource = tickets;

                if (dataGridViewTickets.Columns.Contains("TicketID"))
                    dataGridViewTickets.Columns["TicketID"].Visible = false;

                if (dataGridViewTickets.Columns.Contains("TicketNumber"))
                    dataGridViewTickets.Columns["TicketNumber"].HeaderText = "Номер билета";

                if (dataGridViewTickets.Columns.Contains("Question1"))
                    dataGridViewTickets.Columns["Question1"].HeaderText = "Вопрос 1";

                if (dataGridViewTickets.Columns.Contains("Question2"))
                    dataGridViewTickets.Columns["Question2"].HeaderText = "Вопрос 2";

                if (dataGridViewTickets.Columns.Contains("Question3"))
                    dataGridViewTickets.Columns["Question3"].HeaderText = "Вопрос 3";

                dataGridViewTickets.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewTickets.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                lblUserInfo.Text = $"Пользователь: {userFullName} ({userRole}) | Всего билетов: {tickets.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TicketEditForm editForm = new TicketEditForm(currentUserId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadTickets();
                MessageBox.Show("Билет добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewTickets.CurrentRow != null)
            {
                int ticketId = Convert.ToInt32(dataGridViewTickets.CurrentRow.Cells["TicketID"].Value);
                TicketEditForm editForm = new TicketEditForm(ticketId, currentUserId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTickets();
                    MessageBox.Show("Билет обновлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите билет!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTickets.CurrentRow != null)
            {
                int ticketId = Convert.ToInt32(dataGridViewTickets.CurrentRow.Cells["TicketID"].Value);
                int ticketNumber = Convert.ToInt32(dataGridViewTickets.CurrentRow.Cells["TicketNumber"].Value);

                DialogResult result = MessageBox.Show($"Удалить билет №{ticketNumber}?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes && dbHelper.DeleteTicket(ticketId))
                {
                    LoadTickets();
                    MessageBox.Show("Билет удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTickets();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnEdit_Click(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Просто закрываем форму, не открываем новую (возврат на предыдущую)
        }
    }
}