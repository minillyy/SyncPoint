using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SyncPoint.Data
{
    public static class DatabaseHelper
    {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncpoint.db");
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;Foreign Keys=True;";

        public static void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                CreateTables(conn);
                SeedData(conn);
            }
        }

        public static SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        private static void CreateTables(SQLiteConnection conn)
        {
            string[] tables = {
                @"CREATE TABLE IF NOT EXISTS Roles (
                    RoleID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoleName VARCHAR(50) NOT NULL UNIQUE
                );",
                @"CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName VARCHAR(100) NOT NULL,
                    Username VARCHAR(50) NOT NULL UNIQUE,
                    Password VARCHAR(256) NOT NULL,
                    RoleID INTEGER NOT NULL DEFAULT 3,
                    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
                );",
                @"CREATE TABLE IF NOT EXISTS Groups (
                    GroupID INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupName VARCHAR(100) NOT NULL,
                    InstructorID INTEGER NOT NULL,
                    LeaderID INTEGER,
                    FOREIGN KEY (InstructorID) REFERENCES Users(UserID),
                    FOREIGN KEY (LeaderID) REFERENCES Users(UserID)
                );",
                @"CREATE TABLE IF NOT EXISTS GroupMembers (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupID INTEGER NOT NULL,
                    UserID INTEGER NOT NULL,
                    GroupRole VARCHAR(50) DEFAULT 'Member',
                    JoinedAt DATE,
                    UNIQUE(GroupID, UserID),
                    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
                    FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );",
                @"CREATE TABLE IF NOT EXISTS TaskStatus (
                    StatusID INTEGER PRIMARY KEY AUTOINCREMENT,
                    StatusName VARCHAR(50) NOT NULL UNIQUE
                );",
                @"CREATE TABLE IF NOT EXISTS Tasks (
                    TaskID INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupID INTEGER NOT NULL,
                    Title VARCHAR(200) NOT NULL,
                    Description TEXT,
                    Deadline DATE NOT NULL,
                    AssignedTo INTEGER,
                    StatusID INTEGER NOT NULL DEFAULT 1,
                    CreatedAt DATE,
                    TaskWeight INTEGER DEFAULT 1,
                    SubmissionLink TEXT,
                    SubmittedAt DATE,
                    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
                    FOREIGN KEY (AssignedTo) REFERENCES Users(UserID),
                    FOREIGN KEY (StatusID) REFERENCES TaskStatus(StatusID)
                );",
                @"CREATE TABLE IF NOT EXISTS Scores (
                    ScoreID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TaskID INTEGER NOT NULL,
                    UserID INTEGER NOT NULL,
                    Points DECIMAL(10,2) DEFAULT 0,
                    Penalty DECIMAL(10,2) DEFAULT 0,
                    FinalScore DECIMAL(10,2) DEFAULT 0,
                    FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID),
                    FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );",
                @"CREATE TABLE IF NOT EXISTS Reports (
                    ReportID INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupID INTEGER NOT NULL,
                    GeneratedBy INTEGER NOT NULL,
                    DateGenerated DATE,
                    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
                    FOREIGN KEY (GeneratedBy) REFERENCES Users(UserID)
                );"
            };

            foreach (string sql in tables)
                Execute(conn, sql);
        }

        private static void SeedData(SQLiteConnection conn)
        {
            long roleCount = ScalarLong(conn, "SELECT COUNT(*) FROM Roles;");
            if (roleCount == 0)
            {
                ExecuteParam(conn, "INSERT INTO Roles (RoleName) VALUES (@r);", ("@r", "Instructor"));
                ExecuteParam(conn, "INSERT INTO Roles (RoleName) VALUES (@r);", ("@r", "Leader"));
                ExecuteParam(conn, "INSERT INTO Roles (RoleName) VALUES (@r);", ("@r", "Member"));
            }

            long statusCount = ScalarLong(conn, "SELECT COUNT(*) FROM TaskStatus;");
            if (statusCount == 0)
            {
                foreach (string s in new[] { "Pending", "Accepted", "In Progress", "Pending Review", "Completed", "Declined" })
                {
                    ExecuteParam(conn, "INSERT INTO TaskStatus (StatusName) VALUES (@s);", ("@s", s));
                }
            }
            SeedInstructor(conn);
        }

        private static void SeedInstructor(SQLiteConnection conn)
        {
            string username = AppConfig.Get("username");
            string password = AppConfig.Get("password");
            string fullName = AppConfig.Get("fullname");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException("instructor.config is missing or has empty fields.");

            string hashed = HashPassword(password);
            long exists = ScalarLongParam(conn, "SELECT COUNT(*) FROM Users WHERE Username = @u;", ("@u", username));

            if (exists == 0)
            {
                ExecuteParam(conn, "INSERT INTO Users (FullName, Username, Password, RoleID) VALUES (@fn, @u, @p, 1);",
                    ("@fn", fullName), ("@u", username), ("@p", hashed));
            }
            else
            {
                ExecuteParam(conn, "UPDATE Users SET Password = @p WHERE Username = @u AND RoleID = 1;",
                    ("@p", hashed), ("@u", username));
            }
        }

        public static string HashPassword(string plain)
        {
            if (string.IsNullOrEmpty(plain)) throw new ArgumentException("Password cannot be empty.");
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
                var sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static DataRow ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;
            string hashed = HashPassword(password);

            using (var conn = GetConnection())
            {
                string sql = @"SELECT u.UserID, u.FullName, u.Username, u.RoleID, r.RoleName
                               FROM Users u JOIN Roles r ON u.RoleID = r.RoleID
                               WHERE u.Username = @user AND u.Password = @pass;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", hashed);
                    using (var da = new SQLiteDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                    }
                }
            }
        }

        public static bool RegisterUser(string fullName, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return false;
            try
            {
                string hashed = HashPassword(password);
                using (var conn = GetConnection())
                {
                    string sql = "INSERT INTO Users (FullName, Username, Password, RoleID) VALUES (@fn, @user, @pass, 3);";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@fn", fullName);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", hashed);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint) { return false; }
        }

        public static int CreateGroup(string groupName, int instructorID)
        {
            using (var conn = GetConnection())
            {
                string sql = "INSERT INTO Groups (GroupName, InstructorID) VALUES (@name, @id); SELECT last_insert_rowid();";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", groupName);
                    cmd.Parameters.AddWithValue("@id", instructorID);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static void AddMemberToGroup(int groupID, int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = "INSERT OR IGNORE INTO GroupMembers (GroupID, UserID, JoinedAt) VALUES (@gid, @uid, @date);";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    cmd.Parameters.AddWithValue("@uid", userID);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetGroupMembers(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT u.UserID, u.FullName, u.Username, r.RoleName, gm.JoinedAt
                               FROM GroupMembers gm JOIN Users u ON gm.UserID = u.UserID
                               JOIN Roles r ON u.RoleID = r.RoleID
                               WHERE gm.GroupID = @gid ORDER BY u.FullName ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetAllGroups()
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT g.GroupID, g.GroupName, COALESCE(u.FullName, 'Not appointed') AS LeaderName, COUNT(gm.ID) AS MemberCount
                               FROM Groups g LEFT JOIN Users u ON g.LeaderID = u.UserID
                               LEFT JOIN GroupMembers gm ON g.GroupID = gm.GroupID
                               GROUP BY g.GroupID ORDER BY g.GroupName ASC;";
                var da = new SQLiteDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetAllMembers()
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT UserID, FullName, Username FROM Users WHERE RoleID = 3 ORDER BY FullName ASC;";
                var da = new SQLiteDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int GetUserGroupID(int userID)
        {
            using (var conn = GetConnection())
            {
                string leaderSql = "SELECT GroupID FROM Groups WHERE LeaderID = @uid LIMIT 1;";
                using (var cmd = new SQLiteCommand(leaderSql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userID);
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) return Convert.ToInt32(res);
                }

                string memberSql = "SELECT GroupID FROM GroupMembers WHERE UserID = @uid LIMIT 1;";
                using (var cmd = new SQLiteCommand(memberSql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userID);
                    var res = cmd.ExecuteScalar();
                    return (res == null || res == DBNull.Value) ? -1 : Convert.ToInt32(res);
                }
            }
        }

        public static bool GroupHasLeader(int groupID)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand("SELECT LeaderID FROM Groups WHERE GroupID = @gid;", conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value;
                }
            }
        }

        public static void AppointLeader(int groupID, int userID)
        {
            using (var conn = GetConnection())
            {
                int prevLeader = -1;
                using (var cmd = new SQLiteCommand("SELECT LeaderID FROM Groups WHERE GroupID = @gid;", conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) prevLeader = Convert.ToInt32(res);
                }

                ExecuteParam(conn, "UPDATE Groups SET LeaderID = @uid WHERE GroupID = @gid;", ("@uid", userID), ("@gid", groupID));
                ExecuteParam(conn, "UPDATE Users SET RoleID = 2 WHERE UserID = @uid;", ("@uid", userID));

                long inGroup = ScalarLongParam(conn, "SELECT COUNT(*) FROM GroupMembers WHERE GroupID = @gid AND UserID = @uid;", ("@gid", groupID), ("@uid", userID));
                if (inGroup == 0)
                {
                    ExecuteParam(conn, "INSERT INTO GroupMembers (GroupID, UserID, GroupRole, JoinedAt) VALUES (@gid, @uid, 'Leader', @d);",
                        ("@gid", groupID), ("@uid", userID), ("@d", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                else
                {
                    ExecuteParam(conn, "UPDATE GroupMembers SET GroupRole = 'Leader' WHERE GroupID = @gid AND UserID = @uid;", ("@gid", groupID), ("@uid", userID));
                }

                if (prevLeader > 0 && prevLeader != userID)
                {
                    ExecuteParam(conn, "UPDATE Users SET RoleID = 3 WHERE UserID = @pid;", ("@pid", prevLeader));
                    ExecuteParam(conn, "UPDATE GroupMembers SET GroupRole = 'Member' WHERE GroupID = @gid AND UserID = @pid;", ("@gid", groupID), ("@pid", prevLeader));
                }
            }
        }

        public static DataTable GetMembersOfGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT u.UserID, u.FullName, u.Username, gm.GroupRole, gm.JoinedAt
                               FROM GroupMembers gm JOIN Users u ON gm.UserID = u.UserID
                               WHERE gm.GroupID = @gid ORDER BY gm.GroupRole ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetAvailableMembersForGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT UserID, FullName, Username FROM Users WHERE RoleID = 3 
                               AND UserID NOT IN (SELECT UserID FROM GroupMembers WHERE GroupID = @gid) ORDER BY FullName ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static int CreateTask(int groupID, string title, string description, DateTime deadline, int? assignedTo, int weight)
        {
            using (var conn = GetConnection())
            {
                string sql = @"INSERT INTO Tasks (GroupID, Title, Description, Deadline, AssignedTo, StatusID, CreatedAt, TaskWeight)
                               VALUES (@gid, @title, @desc, @dl, @assigned, 1, DATE('now'), @weight); SELECT last_insert_rowid();";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@desc", description);
                    cmd.Parameters.AddWithValue("@dl", deadline.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@weight", weight);
                    cmd.Parameters.AddWithValue("@assigned", (object)assignedTo ?? DBNull.Value);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static bool TaskTitleExists(int groupID, string title)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Tasks WHERE GroupID = @gid AND LOWER(Title) = LOWER(@title);", conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    cmd.Parameters.AddWithValue("@title", title);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static DataTable GetTasksByGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT t.TaskID, t.Title, t.Description, t.Deadline, t.TaskWeight, 
                   COALESCE(u.FullName, 'Unassigned') AS AssignedTo, 
                   ts.StatusName AS Status
            FROM Tasks t 
            LEFT JOIN Users u ON t.AssignedTo = u.UserID
            JOIN TaskStatus ts ON t.StatusID = ts.StatusID 
            WHERE t.GroupID = @gid 
            ORDER BY t.Deadline ASC;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetAvailableTasks(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT t.TaskID, t.Title, t.Description, t.Deadline, t.TaskWeight, ts.StatusName AS Status
            FROM Tasks t 
            JOIN TaskStatus ts ON t.StatusID = ts.StatusID 
            WHERE t.GroupID = @gid 
              AND t.AssignedTo IS NULL 
              AND ts.StatusName = 'Pending' 
            ORDER BY t.Deadline ASC;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static bool AssignAndAcceptTask(int taskID, int userID)
        {
            using (var conn = GetConnection())
            {
                using (var cmdCheck = new SQLiteCommand("SELECT ts.StatusName FROM Tasks t JOIN TaskStatus ts ON t.StatusID = ts.StatusID WHERE t.TaskID = @tid", conn))
                {
                    cmdCheck.Parameters.AddWithValue("@tid", taskID);
                    var currentStatus = cmdCheck.ExecuteScalar()?.ToString();
                    if (currentStatus != "Pending") return false;
                }

                string sql = @"UPDATE Tasks SET AssignedTo = @uid, StatusID = (SELECT StatusID FROM TaskStatus WHERE StatusName = 'In Progress') WHERE TaskID = @tid;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userID);
                    cmd.Parameters.AddWithValue("@tid", taskID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static DataTable GetTasksByMember(int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT t.TaskID, t.Title, t.Description, t.Deadline, t.TaskWeight, ts.StatusName AS Status
                               FROM Tasks t JOIN TaskStatus ts ON t.StatusID = ts.StatusID WHERE t.AssignedTo = @uid ORDER BY t.Deadline ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void UpdateTaskStatus(int taskID, string statusName)
        {
            using (var conn = GetConnection())
            {
                string sql = "UPDATE Tasks SET StatusID = (SELECT StatusID FROM TaskStatus WHERE StatusName = @sname) WHERE TaskID = @tid;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sname", statusName);
                    cmd.Parameters.AddWithValue("@tid", taskID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void RecordScore(int taskID, int userID, DateTime deadline, int difficulty)
        {
            decimal basePoints = difficulty * 10;
            decimal penalty = (DateTime.Now.Date > deadline.Date) ? (DateTime.Now.Date - deadline.Date).Days * 5 : 0;
            decimal finalScore = Math.Max(0, basePoints - penalty);

            using (var conn = GetConnection())
            {
                string sql = "INSERT INTO Scores (TaskID, UserID, Points, Penalty, FinalScore) VALUES (@tid, @uid, @pts, @pen, @final);";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", taskID);
                    cmd.Parameters.AddWithValue("@uid", userID);
                    cmd.Parameters.AddWithValue("@pts", basePoints);
                    cmd.Parameters.AddWithValue("@pen", penalty);
                    cmd.Parameters.AddWithValue("@final", finalScore);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetScoresByGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT u.FullName, COUNT(s.ScoreID) AS TasksDone, SUM(s.FinalScore) AS TotalScore, AVG(s.FinalScore) AS AvgScore
                               FROM Scores s JOIN Users u ON s.UserID = u.UserID JOIN Tasks t ON s.TaskID = t.TaskID
                               WHERE t.GroupID = @gid GROUP BY s.UserID;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static bool SubmitTask(int taskID, string submissionLink)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    string sql = @"UPDATE Tasks SET StatusID = (SELECT StatusID FROM TaskStatus WHERE StatusName = 'Pending Review'),
                                   SubmissionLink = @link, SubmittedAt = DATE('now') WHERE TaskID = @tid;";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@link", submissionLink);
                        cmd.Parameters.AddWithValue("@tid", taskID);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch { return false; }
        }

        public static DataTable GetPendingSubmissions(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT t.TaskID, t.AssignedTo AS UserID, u.FullName AS Member, t.Title, t.TaskWeight AS Points,
                               t.SubmissionLink AS [Work Link], t.SubmittedAt AS [Date Submitted]
                               FROM Tasks t JOIN Users u ON t.AssignedTo = u.UserID JOIN TaskStatus ts ON t.StatusID = ts.StatusID
                               WHERE t.GroupID = @gid AND ts.StatusName = 'Pending Review' ORDER BY t.SubmittedAt ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static bool ApproveTask(int taskID, int userID, int points)
        {
            using (var conn = GetConnection())
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SQLiteCommand(conn))
                        {
                            cmd.CommandText = "UPDATE Tasks SET StatusID = (SELECT StatusID FROM TaskStatus WHERE StatusName = 'Completed') WHERE TaskID = @tid;";
                            cmd.Parameters.AddWithValue("@tid", taskID);
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "INSERT INTO Scores (TaskID, UserID, Points, FinalScore) VALUES (@tid, @uid, @pts, @pts);";
                            cmd.Parameters.AddWithValue("@uid", userID);
                            cmd.Parameters.AddWithValue("@pts", points);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch { transaction.Rollback(); return false; }
                }
            }
        }

        public static bool ReturnTaskToMember(int taskID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"UPDATE Tasks SET StatusID = (SELECT StatusID FROM TaskStatus WHERE StatusName = 'In Progress'),
                               SubmissionLink = NULL, SubmittedAt = NULL WHERE TaskID = @tid;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", taskID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static DataTable GetLeaderboard(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT u.FullName AS Member, IFNULL(SUM(s.FinalScore), 0) AS [Total Points], COUNT(s.ScoreID) AS [Tasks Completed]
                               FROM GroupMembers gm JOIN Users u ON gm.UserID = u.UserID LEFT JOIN Scores s ON u.UserID = s.UserID
                               LEFT JOIN Tasks t ON s.TaskID = t.TaskID WHERE gm.GroupID = @gid 
                               AND (t.GroupID = @gid OR s.ScoreID IS NULL) AND LOWER(u.FullName) != 'instructor'
                               GROUP BY u.UserID, u.FullName ORDER BY [Total Points] DESC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetTaskDistribution(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT TaskWeight AS [Point Value], COUNT(TaskID) AS [Task Count] FROM Tasks WHERE GroupID = @gid GROUP BY TaskWeight ORDER BY TaskWeight ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetTaskSummary(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT ts.StatusName, COUNT(t.TaskID) AS Count FROM TaskStatus ts LEFT JOIN Tasks t ON ts.StatusID = t.StatusID AND t.GroupID = @gid GROUP BY ts.StatusID;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetMemberProgress(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT u.FullName, COUNT(t.TaskID) AS Total, SUM(CASE WHEN ts.StatusName = 'Completed' THEN 1 ELSE 0 END) AS Done,
                               ROUND(SUM(CASE WHEN ts.StatusName = 'Completed' THEN 1.0 ELSE 0 END) / NULLIF(COUNT(t.TaskID), 0) * 100, 1) AS CompletionRate
                               FROM GroupMembers gm JOIN Users u ON gm.UserID = u.UserID LEFT JOIN Tasks t ON t.AssignedTo = u.UserID AND t.GroupID = @gid
                               LEFT JOIN TaskStatus ts ON t.StatusID = ts.StatusID WHERE gm.GroupID = @gid AND LOWER(u.FullName) != 'instructor'
                               GROUP BY u.UserID, u.FullName ORDER BY u.FullName ASC;";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    var da = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void Execute(SQLiteConnection conn, string sql) { using (var cmd = new SQLiteCommand(sql, conn)) cmd.ExecuteNonQuery(); }

        private static void ExecuteParam(SQLiteConnection conn, string sql, params (string key, object val)[] parms)
        {
            using (var cmd = new SQLiteCommand(sql, conn)) { foreach (var (key, val) in parms) cmd.Parameters.AddWithValue(key, val); cmd.ExecuteNonQuery(); }
        }

        private static long ScalarLong(SQLiteConnection conn, string sql) { using (var cmd = new SQLiteCommand(sql, conn)) return (long)cmd.ExecuteScalar(); }

        private static long ScalarLongParam(SQLiteConnection conn, string sql, params (string key, object val)[] parms)
        {
            using (var cmd = new SQLiteCommand(sql, conn)) { foreach (var (key, val) in parms) cmd.Parameters.AddWithValue(key, val); return (long)cmd.ExecuteScalar(); }
        }
    }
}