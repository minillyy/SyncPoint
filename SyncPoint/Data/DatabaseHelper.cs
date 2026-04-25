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
        private static readonly string DbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncpoint.db");

        private static readonly string ConnectionString =
            $"Data Source={DbPath};Version=3;";

        //  INITIALIZATION
        public static void InitializeDatabase()
        {
            if (!File.Exists(DbPath))
                SQLiteConnection.CreateFile(DbPath);

            using (var conn = GetConnection())
            {
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

        //  CREATE TABLES
        private static void CreateTables(SQLiteConnection conn)
        {
            string[] tables = {

                // Roles
                @"CREATE TABLE IF NOT EXISTS Roles (
                    RoleID    INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoleName  VARCHAR(50) NOT NULL
                );",

                // Users
                @"CREATE TABLE IF NOT EXISTS Users (
                    UserID    INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName  VARCHAR(100) NOT NULL,
                    Username  VARCHAR(50)  NOT NULL UNIQUE,
                    Password  VARCHAR(256) NOT NULL,
                    RoleID    INTEGER,
                    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
                );",

                // Groups
                @"CREATE TABLE IF NOT EXISTS Groups (
                    GroupID       INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupName     VARCHAR(100) NOT NULL,
                    InstructorID  INTEGER NOT NULL,
                    LeaderID      INTEGER,
                    FOREIGN KEY (InstructorID) REFERENCES Users(UserID),
                    FOREIGN KEY (LeaderID)     REFERENCES Users(UserID)
                );",

                // GroupMembers (junction table)
                @"CREATE TABLE IF NOT EXISTS GroupMembers (
                    ID        INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupID   INTEGER NOT NULL,
                    UserID    INTEGER NOT NULL,
                    JoinedAt  DATE,
                    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
                    FOREIGN KEY (UserID)  REFERENCES Users(UserID)
                );",

                // TaskStatus
                @"CREATE TABLE IF NOT EXISTS TaskStatus (
                    StatusID    INTEGER PRIMARY KEY AUTOINCREMENT,
                    StatusName  VARCHAR(50) NOT NULL
                );",

                // Tasks
                @"CREATE TABLE IF NOT EXISTS Tasks (
                    TaskID      INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupID     INTEGER NOT NULL,
                    Title       VARCHAR(200) NOT NULL,
                    Description TEXT,
                    Deadline    DATE,
                    Difficulty  INTEGER DEFAULT 1,
                    TaskType    VARCHAR(20) NOT NULL DEFAULT 'Individual',
                    AssignedTo  INTEGER,
                    StatusID    INTEGER DEFAULT 1,
                    FOREIGN KEY (GroupID)    REFERENCES Groups(GroupID),
                    FOREIGN KEY (AssignedTo) REFERENCES Users(UserID),
                    FOREIGN KEY (StatusID)   REFERENCES TaskStatus(StatusID)
                );",

                // Scores
                @"CREATE TABLE IF NOT EXISTS Scores (
                    ScoreID     INTEGER PRIMARY KEY AUTOINCREMENT,
                    TaskID      INTEGER NOT NULL,
                    UserID      INTEGER NOT NULL,
                    Points      DECIMAL(10,2) DEFAULT 0,
                    Penalty     DECIMAL(10,2) DEFAULT 0,
                    FinalScore  DECIMAL(10,2) DEFAULT 0,
                    FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID),
                    FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );",

                // Reports
                @"CREATE TABLE IF NOT EXISTS Reports (
                    ReportID       INTEGER PRIMARY KEY AUTOINCREMENT,
                    GroupID        INTEGER NOT NULL,
                    GeneratedBy    INTEGER NOT NULL,
                    DateGenerated  DATE,
                    FOREIGN KEY (GroupID)     REFERENCES Groups(GroupID),
                    FOREIGN KEY (GeneratedBy) REFERENCES Users(UserID)
                );"
            };

            foreach (var sql in tables)
            {
                using (var cmd = new SQLiteCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        //  SEED DEFAULT DATA
        private static void SeedData(SQLiteConnection conn)
        {
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Roles;", conn))
            {
                long count = (long)cmd.ExecuteScalar();
                if (count == 0)
                {
                    Execute(conn, "INSERT INTO Roles (RoleName) VALUES ('Instructor');");
                    Execute(conn, "INSERT INTO Roles (RoleName) VALUES ('Leader');");
                    Execute(conn, "INSERT INTO Roles (RoleName) VALUES ('Member');");
                }
            }

            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM TaskStatus;", conn))
            {
                long count = (long)cmd.ExecuteScalar();
                if (count == 0)
                {
                    Execute(conn, "INSERT INTO TaskStatus (StatusName) VALUES ('Pending');");
                    Execute(conn, "INSERT INTO TaskStatus (StatusName) VALUES ('Accepted');");
                    Execute(conn, "INSERT INTO TaskStatus (StatusName) VALUES ('In Progress');");
                    Execute(conn, "INSERT INTO TaskStatus (StatusName) VALUES ('Completed');");
                    Execute(conn, "INSERT INTO TaskStatus (StatusName) VALUES ('Declined');");
                }
            }

            // Seed a default admin/leader account if no users exist
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users;", conn))
            {
                long count = (long)cmd.ExecuteScalar();
                if (count == 0)
                {
                    string hashed = HashPassword("instructor123");
                    Execute(conn,
                        "INSERT INTO Users (FullName, Username, Password, RoleID) " +
                        "VALUES ('Default Instructor', 'instructor', '" + hashed + "', 1);");
                }
            }
        }

        //  PASSWORD HASHING (SHA256)
        public static string HashPassword(string plainText)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        //  HELPER — execute non-query SQL
        public static void Execute(SQLiteConnection conn, string sql)
        {
            using (var cmd = new SQLiteCommand(sql, conn))
                cmd.ExecuteNonQuery();
        }

        //  USER QUERIES

        // Returns a DataTable of all users with their role names.
        public static DataTable GetAllUsers()
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT u.UserID, u.FullName, u.Username, r.RoleName
                    FROM   Users u
                    JOIN   Roles r ON u.RoleID = r.RoleID;";
                var da = new SQLiteDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Validates login. Returns the user row or null.
        public static DataRow ValidateLogin(string username, string password)
        {
            string hashed = HashPassword(password);
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT u.UserID, u.FullName, u.Username, u.RoleID, r.RoleName
                    FROM   Users u
                    JOIN   Roles r ON u.RoleID = r.RoleID
                    WHERE  u.Username = @user AND u.Password = @pass;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", hashed);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        // Registers a new user. Returns true if successful.
        public static bool RegisterUser(string fullName, string username,
                                        string password, string roleName)
        {
            try
            {
                string hashed = HashPassword(password);

                // Get the RoleID for the selected role name
                int roleID = 1;
                using (var conn = GetConnection())
                {
                    var cmd = new SQLiteCommand(
                        "SELECT RoleID FROM Roles WHERE RoleName = @name;", conn);
                    cmd.Parameters.AddWithValue("@name", roleName);
                    var result = cmd.ExecuteScalar();
                    if (result != null) roleID = Convert.ToInt32(result);

                    string sql = @"
                INSERT INTO Users (FullName, Username, Password, RoleID)
                VALUES (@name, @user, @pass, @role);";
                    var insert = new SQLiteCommand(sql, conn);
                    insert.Parameters.AddWithValue("@name", fullName);
                    insert.Parameters.AddWithValue("@user", username);
                    insert.Parameters.AddWithValue("@pass", hashed);
                    insert.Parameters.AddWithValue("@role", roleID);
                    insert.ExecuteNonQuery();
                }
                return true;
            }
            catch (SQLiteException)
            {
                // Username already taken — UNIQUE constraint violation
                return false;
            }
        }

        //  GROUP QUERIES

        // Creates a new group and returns its new GroupID.
        public static int CreateGroup(string groupName, int instructorID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Groups (GroupName, InstructorID)
                    VALUES (@name, @id);
                    SELECT last_insert_rowid();";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", groupName);
                cmd.Parameters.AddWithValue("@id", instructorID);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Adds a user to a group.
        public static void AddMemberToGroup(int groupID, int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO GroupMembers (GroupID, UserID, JoinedAt)
                    VALUES (@gid, @uid, @date);";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                cmd.Parameters.AddWithValue("@uid", userID);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
            }
        }

        // Returns all members of a group
        public static DataTable GetGroupMembers(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT u.UserID, u.FullName, u.Username, r.RoleName, gm.JoinedAt
                    FROM   GroupMembers gm
                    JOIN   Users u  ON gm.UserID  = u.UserID
                    JOIN   Roles r  ON u.RoleID   = r.RoleID
                    WHERE  gm.GroupID = @gid;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Returns the GroupID that a user belongs to. Returns -1 if none
        public static int GetUserGroupID(int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT GroupID FROM GroupMembers WHERE UserID = @uid LIMIT 1;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uid", userID);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        // Appoint a Leader to a group

        public static void AppointLeader(int groupID, int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            UPDATE Groups SET LeaderID = @uid WHERE GroupID = @gid;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uid", userID);
                cmd.Parameters.AddWithValue("@gid", groupID);
                cmd.ExecuteNonQuery();

                string updateRole = @"
            UPDATE Users SET RoleID = 2 WHERE UserID = @uid;";
                var cmd2 = new SQLiteCommand(updateRole, conn);
                cmd2.Parameters.AddWithValue("@uid", userID);
                cmd2.ExecuteNonQuery();

                string updateGM = @"
            UPDATE GroupMembers SET GroupRole = 'Leader'
            WHERE GroupID = @gid AND UserID = @uid;";
                var cmd3 = new SQLiteCommand(updateGM, conn);
                cmd3.Parameters.AddWithValue("@gid", groupID);
                cmd3.Parameters.AddWithValue("@uid", userID);
                cmd3.ExecuteNonQuery();
            }
        }

        // Get all Members (RoleID = 3) not yet in a group
        public static DataTable GetAvailableMembers()
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT u.UserID, u.FullName, u.Username
            FROM   Users u
            WHERE  u.RoleID = 3
            AND    u.UserID NOT IN (
                SELECT UserID FROM GroupMembers
            );";
                var da = new SQLiteDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Get all Members in a group 
        public static DataTable GetAvailableMembersForGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT u.UserID, u.FullName, u.Username
            FROM   Users u
            WHERE  u.RoleID = 3
            AND    u.UserID NOT IN (
                SELECT UserID FROM GroupMembers WHERE GroupID = @gid
            );";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Get all groups managed by an instructor
        public static DataTable GetGroupsByInstructor(int instructorID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT g.GroupID, g.GroupName,
                   u.FullName AS LeaderName,
                   COUNT(gm.ID) AS MemberCount
            FROM   Groups g
            LEFT JOIN Users u  ON g.LeaderID     = u.UserID
            LEFT JOIN GroupMembers gm ON g.GroupID = gm.GroupID
            WHERE  g.InstructorID = @id
            GROUP BY g.GroupID;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", instructorID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Search user by username
        public static DataRow GetUserByUsername(string username)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT u.UserID, u.FullName, u.Username, r.RoleName
            FROM   Users u
            JOIN   Roles r ON u.RoleID = r.RoleID
            WHERE  u.Username = @user;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        //  TASK QUERIES

        // Creates a new task and returns its TaskID
        public static int CreateTask(int groupID, string title, string description,
                                     DateTime deadline, int difficulty,
                                     string taskType, int assignedTo)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Tasks
                        (GroupID, Title, Description, Deadline, Difficulty, TaskType, AssignedTo, StatusID)
                    VALUES
                        (@gid, @title, @desc, @dl, @diff, @type, @assigned, 1);
                    SELECT last_insert_rowid();";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@dl", deadline.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@diff", difficulty);
                cmd.Parameters.AddWithValue("@type", taskType);
                cmd.Parameters.AddWithValue("@assigned", assignedTo);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Returns all tasks for a group with status and assignee info.
        public static DataTable GetTasksByGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT t.TaskID, t.Title, t.Description, t.Deadline,
                           t.Difficulty, t.TaskType,
                           u.FullName  AS AssignedTo,
                           ts.StatusName AS Status
                    FROM   Tasks t
                    JOIN   Users      u  ON t.AssignedTo = u.UserID
                    JOIN   TaskStatus ts ON t.StatusID   = ts.StatusID
                    WHERE  t.GroupID = @gid
                    ORDER BY t.Deadline ASC;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Returns tasks assigned to a specific member.
        public static DataTable GetTasksByMember(int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT t.TaskID, t.Title, t.Description, t.Deadline,
                           t.Difficulty, t.TaskType,
                           ts.StatusName AS Status
                    FROM   Tasks t
                    JOIN   TaskStatus ts ON t.StatusID = ts.StatusID
                    WHERE  t.AssignedTo = @uid
                    ORDER BY t.Deadline ASC;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uid", userID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Updates the status of a task by name (e.g. "In Progress").
        public static void UpdateTaskStatus(int taskID, string statusName)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    UPDATE Tasks
                    SET    StatusID = (
                               SELECT StatusID FROM TaskStatus WHERE StatusName = @sname
                           )
                    WHERE  TaskID = @tid;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sname", statusName);
                cmd.Parameters.AddWithValue("@tid", taskID);
                cmd.ExecuteNonQuery();
            }
        }

        //  SCORE QUERIES
        public static void RecordScore(int taskID, int userID, DateTime deadline, int difficulty)
        {
            decimal basePoints = difficulty * 10;   // e.g. difficulty 5 = 50 pts
            decimal penalty = 0;

            if (DateTime.Now.Date > deadline.Date)
            {
                int daysLate = (DateTime.Now.Date - deadline.Date).Days;
                penalty = daysLate * 5;             // -5 pts per day late
            }

            decimal finalScore = Math.Max(0, basePoints - penalty);

            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Scores (TaskID, UserID, Points, Penalty, FinalScore)
                    VALUES (@tid, @uid, @pts, @pen, @final);";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tid", taskID);
                cmd.Parameters.AddWithValue("@uid", userID);
                cmd.Parameters.AddWithValue("@pts", basePoints);
                cmd.Parameters.AddWithValue("@pen", penalty);
                cmd.Parameters.AddWithValue("@final", finalScore);
                cmd.ExecuteNonQuery();
            }
        }

        // Returns the score summary per member for a group.
        public static DataTable GetScoresByGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT u.FullName,
                           COUNT(s.ScoreID)      AS TasksDone,
                           SUM(s.FinalScore)     AS TotalScore,
                           AVG(s.FinalScore)     AS AvgScore
                    FROM   Scores s
                    JOIN   Users u  ON s.UserID = u.UserID
                    JOIN   Tasks  t ON s.TaskID = t.TaskID
                    WHERE  t.GroupID = @gid
                    GROUP BY s.UserID;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        //  REPORT QUERIES

        // Saves a new report entry and returns its ReportID.
        public static int GenerateReport(int groupID, int generatedBy)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Reports (GroupID, GeneratedBy, DateGenerated)
                    VALUES (@gid, @by, @date);
                    SELECT last_insert_rowid();";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                cmd.Parameters.AddWithValue("@by", generatedBy);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Returns all reports for a group.
        public static DataTable GetReportsByGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT r.ReportID, u.FullName AS GeneratedBy, r.DateGenerated
                    FROM   Reports r
                    JOIN   Users u ON r.GeneratedBy = u.UserID
                    WHERE  r.GroupID = @gid
                    ORDER BY r.DateGenerated DESC;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        //  DASHBOARD SUMMARY

        // Returns task counts by status for a group (for dashboard cards).
        public static DataTable GetTaskSummary(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT ts.StatusName, COUNT(t.TaskID) AS Count
                    FROM   TaskStatus ts
                    LEFT JOIN Tasks t
                           ON ts.StatusID = t.StatusID AND t.GroupID = @gid
                    GROUP BY ts.StatusID;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetMemberProgress(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT u.FullName,
                           COUNT(t.TaskID)                              AS Total,
                           SUM(CASE WHEN ts.StatusName = 'Completed'
                                    THEN 1 ELSE 0 END)                 AS Done,
                           ROUND(
                               SUM(CASE WHEN ts.StatusName = 'Completed'
                                        THEN 1.0 ELSE 0 END)
                               / NULLIF(COUNT(t.TaskID), 0) * 100, 1) AS CompletionRate
                    FROM   GroupMembers gm
                    JOIN   Users      u  ON gm.UserID   = u.UserID
                    LEFT JOIN Tasks   t  ON t.AssignedTo = u.UserID
                                       AND t.GroupID    = @gid
                    LEFT JOIN TaskStatus ts ON t.StatusID = ts.StatusID
                    WHERE  gm.GroupID = @gid
                    GROUP BY u.UserID;";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gid", groupID);
                var da = new SQLiteDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}