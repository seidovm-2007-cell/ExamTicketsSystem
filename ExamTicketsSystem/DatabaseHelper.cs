using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ExamTicketsSystem
{
    public class DatabaseHelper
    {
        private string _connectionString = @"Data Source=DESKTOP-BMNSL6Q;Initial Catalog=ExamTicketsDB;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Аутентификация пользователя
        public bool AuthenticateUser(string username, string password, out string role, out string fullName, out int userId)
        {
            role = "";
            fullName = "";
            userId = 0;
            string hashedPassword = HashPassword(password);

            string query = "SELECT UserID, Role, FullName FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userId = Convert.ToInt32(reader["UserID"]);
                        role = reader["Role"].ToString();
                        fullName = reader["FullName"].ToString();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к БД: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Регистрация пользователя
        public bool RegisterUser(string username, string password, string fullName, string role)
        {
            string hashedPassword = HashPassword(password);
            string query = "INSERT INTO Users (Username, PasswordHash, FullName, Role, CreatedAt) VALUES (@Username, @PasswordHash, @FullName, @Role, GETDATE())";

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Role", role);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка регистрации: " + ex.Message, "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
            }
        }

        // Получение всех студентов
        public DataTable GetAllStudents()
        {
            string query = "SELECT UserID, FullName FROM Users WHERE Role = 'Student' ORDER BY FullName";
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Получение всех билетов
        public DataTable GetAllTickets()
        {
            string query = "SELECT TicketID, TicketNumber, Question1, Question2, Question3 FROM Tickets ORDER BY TicketNumber";
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Получение билета по ID
        public DataTable GetTicketById(int ticketId)
        {
            string query = "SELECT TicketID, TicketNumber, Question1, Question2, Question3 FROM Tickets WHERE TicketID = @TicketID";
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@TicketID", ticketId);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Получение случайного билета
        public DataTable GetRandomTicket()
        {
            string query = "SELECT TOP 1 TicketID, TicketNumber, Question1, Question2, Question3 FROM Tickets ORDER BY NEWID()";
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Добавление билета
        public bool AddTicket(int ticketNumber, string question1, string question2, string question3, int createdBy)
        {
            string query = @"INSERT INTO Tickets (TicketNumber, Question1, Question2, Question3, CreatedBy, CreatedAt) 
                           VALUES (@TicketNumber, @Question1, @Question2, @Question3, @CreatedBy, GETDATE())";

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TicketNumber", ticketNumber);
                cmd.Parameters.AddWithValue("@Question1", question1);
                cmd.Parameters.AddWithValue("@Question2", question2);
                cmd.Parameters.AddWithValue("@Question3", string.IsNullOrEmpty(question3) ? (object)DBNull.Value : question3);
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Обновление билета
        public bool UpdateTicket(int ticketId, int ticketNumber, string question1, string question2, string question3)
        {
            string query = @"UPDATE Tickets SET TicketNumber = @TicketNumber, Question1 = @Question1, 
                           Question2 = @Question2, Question3 = @Question3, UpdatedAt = GETDATE() 
                           WHERE TicketID = @TicketID";

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TicketID", ticketId);
                cmd.Parameters.AddWithValue("@TicketNumber", ticketNumber);
                cmd.Parameters.AddWithValue("@Question1", question1);
                cmd.Parameters.AddWithValue("@Question2", question2);
                cmd.Parameters.AddWithValue("@Question3", string.IsNullOrEmpty(question3) ? (object)DBNull.Value : question3);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Удаление билета
        public bool DeleteTicket(int ticketId)
        {
            string deleteResultsQuery = "DELETE FROM ExamResults WHERE TicketID = @TicketID";
            string deleteTicketQuery = "DELETE FROM Tickets WHERE TicketID = @TicketID";

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(deleteResultsQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@TicketID", ticketId);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand(deleteTicketQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@TicketID", ticketId);
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

        // Сохранение результата экзамена
        public bool SaveExamResult(int studentId, int ticketId, string answers, int score)
        {
            string query = @"INSERT INTO ExamResults (StudentID, TicketID, Answers, Score, ExamDate) 
                           VALUES (@StudentID, @TicketID, @Answers, @Score, GETDATE())";

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);
                    cmd.Parameters.AddWithValue("@Answers", answers ?? "");
                    cmd.Parameters.AddWithValue("@Score", score);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения результата: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Получение всех результатов (простой)
        public DataTable GetAllExamResults()
        {
            string query = @"
                SELECT 
                    r.ResultID,
                    u.FullName AS StudentName,
                    t.TicketNumber,
                    r.Score,
                    r.Answers,
                    r.ExamDate
                FROM ExamResults r
                JOIN Users u ON r.StudentID = u.UserID
                JOIN Tickets t ON r.TicketID = t.TicketID
                ORDER BY r.ExamDate DESC";

            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // ============ НОВЫЙ МЕТОД С ВОПРОСАМИ ============

        // Получение всех результатов с вопросами билетов (для преподавателя)
        public DataTable GetAllExamResultsWithQuestions()
        {
            string query = @"
                SELECT 
                    r.ResultID,
                    u.FullName AS StudentName,
                    t.TicketNumber,
                    t.Question1,
                    t.Question2,
                    t.Question3,
                    r.Answers,
                    r.Score,
                    r.ExamDate
                FROM ExamResults r
                JOIN Users u ON r.StudentID = u.UserID
                JOIN Tickets t ON r.TicketID = t.TicketID
                ORDER BY r.ExamDate DESC";

            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Добавляем колонки для разделенных ответов
                dt.Columns.Add("StudentAnswer1", typeof(string));
                dt.Columns.Add("StudentAnswer2", typeof(string));
                dt.Columns.Add("StudentAnswer3", typeof(string));

                // Разбираем ответы
                foreach (DataRow row in dt.Rows)
                {
                    string answers = row["Answers"].ToString();
                    ParseAnswersFromString(answers, row);
                }

                // Удаляем исходную колонку Answers (чтобы не дублировать)
                if (dt.Columns.Contains("Answers"))
                    dt.Columns["Answers"].ColumnName = "FullAnswers";

                return dt;
            }
        }

        // Вспомогательный метод для парсинга ответов
        private void ParseAnswersFromString(string answers, DataRow row)
        {
            string answer1 = "";
            string answer2 = "";
            string answer3 = "";

            if (!string.IsNullOrEmpty(answers))
            {
                // Ищем ответ на вопрос 1
                int start1 = answers.IndexOf("Вопрос 1:", StringComparison.Ordinal);
                int start2 = answers.IndexOf("Вопрос 2:", StringComparison.Ordinal);
                int start3 = answers.IndexOf("Вопрос 3:", StringComparison.Ordinal);

                if (start1 >= 0)
                {
                    int end = start2 > 0 ? start2 : answers.Length;
                    answer1 = answers.Substring(start1 + 9, end - start1 - 9).Trim();
                }

                if (start2 >= 0)
                {
                    int end = start3 > 0 ? start3 : answers.Length;
                    answer2 = answers.Substring(start2 + 9, end - start2 - 9).Trim();
                }

                if (start3 >= 0)
                {
                    answer3 = answers.Substring(start3 + 9).Trim();
                }

                // Если не нашли по формату, пробуем другой способ
                if (string.IsNullOrEmpty(answer1) && string.IsNullOrEmpty(answer2) && string.IsNullOrEmpty(answer3))
                {
                    string[] lines = answers.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    int idx = 0;
                    foreach (string line in lines)
                    {
                        string cleanLine = line.Trim();
                        if (string.IsNullOrEmpty(cleanLine)) continue;
                        if (cleanLine.Contains("Вопрос") || cleanLine.Contains("Ответ"))
                            continue;

                        if (idx == 0) answer1 = cleanLine;
                        else if (idx == 1) answer2 = cleanLine;
                        else if (idx == 2) answer3 = cleanLine;
                        idx++;
                    }
                }
            }

            row["StudentAnswer1"] = answer1;
            row["StudentAnswer2"] = answer2;
            row["StudentAnswer3"] = answer3;
        }

        // Получение результатов по студенту
        public DataTable GetExamResultsByStudent(int studentId)
        {
            string query = @"
                SELECT 
                    r.ResultID,
                    t.TicketNumber,
                    r.Score,
                    r.Answers,
                    r.ExamDate
                FROM ExamResults r
                JOIN Tickets t ON r.TicketID = t.TicketID
                WHERE r.StudentID = @StudentID
                ORDER BY r.ExamDate DESC";

            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@StudentID", studentId);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Обновление оценки результата экзамена
        public bool UpdateExamResultScore(int resultId, int newScore)
        {
            string query = "UPDATE ExamResults SET Score = @Score WHERE ResultID = @ResultID";

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Score", newScore);
                    cmd.Parameters.AddWithValue("@ResultID", resultId);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления оценки: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Получение непроверенных работ
        public DataTable GetUngradedResults()
        {
            string query = @"
                SELECT 
                    r.ResultID,
                    u.FullName AS StudentName,
                    t.TicketNumber,
                    r.Answers,
                    r.ExamDate
                FROM ExamResults r
                JOIN Users u ON r.StudentID = u.UserID
                JOIN Tickets t ON r.TicketID = t.TicketID
                WHERE r.Score = 0 OR r.Score IS NULL
                ORDER BY r.ExamDate DESC";

            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Получение статистики
        public DataTable GetExamStatistics()
        {
            string query = @"
                SELECT 
                    COUNT(*) AS TotalExams,
                    AVG(CAST(Score AS FLOAT)) AS AverageScore,
                    MIN(Score) AS MinScore,
                    MAX(Score) AS MaxScore,
                    COUNT(DISTINCT StudentID) AS TotalStudents,
                    COUNT(DISTINCT TicketID) AS TotalTicketsUsed
                FROM ExamResults
                WHERE Score > 0";

            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}