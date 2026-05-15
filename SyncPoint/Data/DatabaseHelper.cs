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
        // ── Database file path ───────────────────────────
        private static readonly string DbPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "syncpoint.db");

        private static readonly string ConnectionString =
            $"Data Source={DbPath};" +
            $"Version=3;" +
            $"Foreign Keys=True;"; // enforce FK constraints

        // ════════════════════════════════════════════════
        //  INITIALIZATION
        // ════════════════════════════════════════════════
        public static void InitializeDatabase()
        {
            using (var conn =
                new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                CreateTables(conn);
                SeedData(conn);
            }
        }

        public static SQLiteConnection GetConnection()
        {
            var conn =
                new SQLiteConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        // ════════════════════════════════════════════════
        //  CREATE TABLES
        // ════════════════════════════════════════════════
        private static void CreateTables(
            SQLiteConnection conn)
        {
            string[] tables = {

                @"CREATE TABLE IF NOT EXISTS Roles (
                    RoleID   INTEGER PRIMARY KEY
                             AUTOINCREMENT,
                    RoleName VARCHAR(50) NOT NULL UNIQUE
                );",

                @"CREATE TABLE IF NOT EXISTS Users (
                    UserID   INTEGER PRIMARY KEY
                             AUTOINCREMENT,
                    FullName VARCHAR(100) NOT NULL,
                    Username VARCHAR(50) NOT NULL UNIQUE,
                    Password VARCHAR(256) NOT NULL,
                    RoleID   INTEGER NOT NULL DEFAULT 3,
                    FOREIGN KEY (RoleID)
                        REFERENCES Roles(RoleID)
                );",

                @"CREATE TABLE IF NOT EXISTS Groups (
                    GroupID      INTEGER PRIMARY KEY
                                 AUTOINCREMENT,
                    GroupName    VARCHAR(100) NOT NULL,
                    InstructorID INTEGER NOT NULL,
                    LeaderID     INTEGER,
                    FOREIGN KEY (InstructorID)
                        REFERENCES Users(UserID),
                    FOREIGN KEY (LeaderID)
                        REFERENCES Users(UserID)
                );",

                @"CREATE TABLE IF NOT EXISTS
                  GroupMembers (
                    ID        INTEGER PRIMARY KEY
                              AUTOINCREMENT,
                    GroupID   INTEGER NOT NULL,
                    UserID    INTEGER NOT NULL,
                    GroupRole VARCHAR(50)
                              DEFAULT 'Member',
                    JoinedAt  DATE,
                    UNIQUE(GroupID, UserID),
                    FOREIGN KEY (GroupID)
                        REFERENCES Groups(GroupID),
                    FOREIGN KEY (UserID)
                        REFERENCES Users(UserID)
                );",

                @"CREATE TABLE IF NOT EXISTS TaskStatus (
                    StatusID   INTEGER PRIMARY KEY
                               AUTOINCREMENT,
                    StatusName VARCHAR(50) NOT NULL UNIQUE
                );",

                @"CREATE TABLE IF NOT EXISTS Tasks (
                    TaskID      INTEGER PRIMARY KEY
                                AUTOINCREMENT,
                    GroupID     INTEGER NOT NULL,
                    Title       VARCHAR(200) NOT NULL,
                    Description TEXT,
                    Deadline    DATE NOT NULL,
                    AssignedTo  INTEGER,
                    StatusID    INTEGER NOT NULL DEFAULT 1,
                    CreatedAt   DATE,
                    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
                    FOREIGN KEY (AssignedTo) REFERENCES Users(UserID),
                    FOREIGN KEY (StatusID) REFERENCES TaskStatus(StatusID)
                );",

                @"CREATE TABLE IF NOT EXISTS Scores (
                    ScoreID    INTEGER PRIMARY KEY
                               AUTOINCREMENT,
                    TaskID     INTEGER NOT NULL,
                    UserID     INTEGER NOT NULL,
                    Points     DECIMAL(10,2) DEFAULT 0,
                    Penalty    DECIMAL(10,2) DEFAULT 0,
                    FinalScore DECIMAL(10,2) DEFAULT 0,
                    FOREIGN KEY (TaskID)
                        REFERENCES Tasks(TaskID),
                    FOREIGN KEY (UserID)
                        REFERENCES Users(UserID)
                );",

                @"CREATE TABLE IF NOT EXISTS Reports (
                    ReportID      INTEGER PRIMARY KEY
                                  AUTOINCREMENT,
                    GroupID       INTEGER NOT NULL,
                    GeneratedBy   INTEGER NOT NULL,
                    DateGenerated DATE,
                    FOREIGN KEY (GroupID)
                        REFERENCES Groups(GroupID),
                    FOREIGN KEY (GeneratedBy)
                        REFERENCES Users(UserID)
                );"
            };

            foreach (string sql in tables)
                Execute(conn, sql);
        }

        // ════════════════════════════════════════════════
        //  SEED DATA
        //  Credentials come from instructor.config
        //  — never hardcoded here
        // ════════════════════════════════════════════════
        private static void SeedData(SQLiteConnection conn)
        {
            // ── Roles ────────────────────────────────────
            long roleCount = ScalarLong(conn,
                "SELECT COUNT(*) FROM Roles;");

            if (roleCount == 0)
            {
                ExecuteParam(conn,
                    "INSERT INTO Roles (RoleName) " +
                    "VALUES (@r);",
                    ("@r", "Instructor"));

                ExecuteParam(conn,
                    "INSERT INTO Roles (RoleName) " +
                    "VALUES (@r);",
                    ("@r", "Leader"));

                ExecuteParam(conn,
                    "INSERT INTO Roles (RoleName) " +
                    "VALUES (@r);",
                    ("@r", "Member"));
            }

            // ── Task Statuses ─────────────────────────────
            long statusCount = ScalarLong(conn,
                "SELECT COUNT(*) FROM TaskStatus;");

            if (statusCount == 0)
            {
                foreach (string s in new[] {
                    "Pending", "Accepted",
                    "In Progress", "Completed",
                    "Declined" })
                {
                    ExecuteParam(conn,
                        "INSERT INTO TaskStatus " +
                        "(StatusName) VALUES (@s);",
                        ("@s", s));
                }
            }

            // ── Instructor account ─────────────────────────
            // Credentials are read from instructor.config
            // They are NEVER written directly in this file
            SeedInstructor(conn);
        }

        // ════════════════════════════════════════════════
        //  SEED INSTRUCTOR ACCOUNT
        //  Reads credentials from instructor.config
        //  so they are never visible in source code
        // ════════════════════════════════════════════════
        private static void SeedInstructor(
            SQLiteConnection conn)
        {
            // Read from config file — not hardcoded
            string username =
                AppConfig.Get("username");
            string password =
                AppConfig.Get("password");
            string fullName =
                AppConfig.Get("fullname");

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException(
                    "instructor.config is missing or " +
                    "has empty username/password fields.");
            }

            // Hash the password from config
            string hashed = HashPassword(password);

            // Check if instructor already exists
            long exists = ScalarLongParam(conn,
                "SELECT COUNT(*) FROM Users " +
                "WHERE Username = @u;",
                ("@u", username));

            if (exists == 0)
            {
                // Insert new instructor account
                ExecuteParam(conn,
                    "INSERT INTO Users " +
                    "(FullName, Username, Password, RoleID) " +
                    "VALUES (@fn, @u, @p, 1);",
                    ("@fn", fullName),
                    ("@u", username),
                    ("@p", hashed));
            }
            else
            {
                // Account exists — ensure password
                // is always up to date with config
                ExecuteParam(conn,
                    "UPDATE Users SET Password = @p " +
                    "WHERE Username = @u AND RoleID = 1;",
                    ("@p", hashed),
                    ("@u", username));
            }
        }

        // ════════════════════════════════════════════════
        //  PASSWORD HASHING — SHA256
        //  Passwords are always hashed before storage
        //  The plain text password never touches the DB
        // ════════════════════════════════════════════════
        public static string HashPassword(string plain)
        {
            if (string.IsNullOrEmpty(plain))
                throw new ArgumentException(
                    "Password cannot be empty.");

            using (var sha = SHA256.Create())
            {
                byte[] bytes =
                    sha.ComputeHash(
                        Encoding.UTF8.GetBytes(plain));

                var sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // ════════════════════════════════════════════════
        //  ENCAPSULATED LOGIN
        //  All login logic is in one place.
        //  Returns the user DataRow or null.
        //  No other method handles authentication.
        // ════════════════════════════════════════════════
        public static DataRow ValidateLogin(
            string username, string password)
        {
            // Guard against empty inputs
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return null;

            // Hash the entered password BEFORE
            // it ever touches the database
            string hashed = HashPassword(password);

            using (var conn = GetConnection())
            {
                // Fully parameterized — no string
                // concatenation anywhere in this query
                string sql = @"
                    SELECT
                        u.UserID,
                        u.FullName,
                        u.Username,
                        u.RoleID,
                        r.RoleName
                    FROM   Users u
                    JOIN   Roles r
                           ON u.RoleID = r.RoleID
                    WHERE  u.Username = @user
                    AND    u.Password = @pass;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    // Parameters prevent SQL injection
                    cmd.Parameters.AddWithValue(
                        "@user", username);
                    cmd.Parameters.AddWithValue(
                        "@pass", hashed);

                    using (var da =
                        new SQLiteDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        return dt.Rows.Count > 0
                            ? dt.Rows[0]
                            : null;
                    }
                }
            }
        }

        // ════════════════════════════════════════════════
        //  REGISTER USER
        //  Default role is always Member (RoleID = 3)
        //  Role can only be changed by the Instructor
        // ════════════════════════════════════════════════
        public static bool RegisterUser(
            string fullName,
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                string hashed = HashPassword(password);

                using (var conn = GetConnection())
                {
                    string sql = @"
                        INSERT INTO Users
                            (FullName, Username,
                             Password, RoleID)
                        VALUES
                            (@fn, @user, @pass, 3);";

                    using (var cmd =
                        new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@fn", fullName);
                        cmd.Parameters.AddWithValue(
                            "@user", username);
                        cmd.Parameters.AddWithValue(
                            "@pass", hashed);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SQLiteException ex)
            when (ex.ResultCode ==
                  SQLiteErrorCode.Constraint)
            {
                // Username already taken
                return false;
            }
        }

        // ════════════════════════════════════════════════
        //  GROUP QUERIES — all parameterized
        // ════════════════════════════════════════════════

        public static int CreateGroup(
            string groupName, int instructorID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Groups
                        (GroupName, InstructorID)
                    VALUES (@name, @id);
                    SELECT last_insert_rowid();";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@name", groupName);
                    cmd.Parameters.AddWithValue(
                        "@id", instructorID);
                    return Convert.ToInt32(
                        cmd.ExecuteScalar());
                }
            }
        }

        public static void AddMemberToGroup(
            int groupID, int userID)
        {
            using (var conn = GetConnection())
            {
                // INSERT OR IGNORE prevents duplicate
                // member entries
                string sql = @"
                    INSERT OR IGNORE INTO GroupMembers
                        (GroupID, UserID, JoinedAt)
                    VALUES (@gid, @uid, @date);";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    cmd.Parameters.AddWithValue(
                        "@uid", userID);
                    cmd.Parameters.AddWithValue(
                        "@date",
                        DateTime.Now
                            .ToString("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetGroupMembers(
            int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        u.UserID,
                        u.FullName,
                        u.Username,
                        r.RoleName,
                        gm.JoinedAt
                    FROM   GroupMembers gm
                    JOIN   Users u
                           ON gm.UserID = u.UserID
                    JOIN   Roles r
                           ON u.RoleID = r.RoleID
                    WHERE  gm.GroupID = @gid
                    ORDER  BY u.FullName ASC;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
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
                string sql = @"
                    SELECT
                        g.GroupID,
                        g.GroupName,
                        COALESCE(u.FullName,
                            'Not appointed')
                            AS LeaderName,
                        COUNT(gm.ID) AS MemberCount
                    FROM   Groups g
                    LEFT JOIN Users u
                           ON g.LeaderID = u.UserID
                    LEFT JOIN GroupMembers gm
                           ON g.GroupID = gm.GroupID
                    GROUP  BY g.GroupID
                    ORDER  BY g.GroupName ASC;";

                var da =
                    new SQLiteDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetAllMembers()
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        u.UserID,
                        u.FullName,
                        u.Username
                    FROM   Users u
                    WHERE  u.RoleID = 3
                    ORDER  BY u.FullName ASC;";

                var da =
                    new SQLiteDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int GetUserGroupID(int userID)
        {
            using (var conn = GetConnection())
            {
                // Check if user is a Leader
                string leaderSql = @"
                    SELECT GroupID FROM Groups
                    WHERE  LeaderID = @uid
                    LIMIT  1;";

                using (var cmd =
                    new SQLiteCommand(leaderSql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@uid", userID);
                    var res = cmd.ExecuteScalar();
                    if (res != null &&
                        res != DBNull.Value)
                        return Convert.ToInt32(res);
                }

                // Check if user is a Member
                string memberSql = @"
                    SELECT GroupID FROM GroupMembers
                    WHERE  UserID = @uid
                    LIMIT  1;";

                using (var cmd =
                    new SQLiteCommand(memberSql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@uid", userID);
                    var res = cmd.ExecuteScalar();
                    if (res == null ||
                        res == DBNull.Value)
                        return -1;
                    return Convert.ToInt32(res);
                }
            }
        }

        public static bool GroupHasLeader(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT LeaderID FROM Groups
                    WHERE  GroupID = @gid;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var result = cmd.ExecuteScalar();
                    return result != null &&
                           result != DBNull.Value;
                }
            }
        }

        public static void AppointLeader(
            int groupID, int userID)
        {
            using (var conn = GetConnection())
            {
                // Find previous leader if any
                int prevLeader = -1;
                string findPrev = @"
                    SELECT LeaderID FROM Groups
                    WHERE  GroupID = @gid;";

                using (var cmd =
                    new SQLiteCommand(findPrev, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var res = cmd.ExecuteScalar();
                    if (res != null &&
                        res != DBNull.Value)
                        prevLeader =
                            Convert.ToInt32(res);
                }

                // Update group's LeaderID
                ExecuteParam(conn,
                    "UPDATE Groups " +
                    "SET LeaderID = @uid " +
                    "WHERE GroupID = @gid;",
                    ("@uid", userID),
                    ("@gid", groupID));

                // Promote user to Leader role
                ExecuteParam(conn,
                    "UPDATE Users SET RoleID = 2 " +
                    "WHERE UserID = @uid;",
                    ("@uid", userID));

                // Add to GroupMembers or update role
                long inGroup = ScalarLongParam(conn,
                    "SELECT COUNT(*) " +
                    "FROM GroupMembers " +
                    "WHERE GroupID = @gid " +
                    "AND UserID = @uid;",
                    ("@gid", groupID),
                    ("@uid", userID));

                if (inGroup == 0)
                {
                    ExecuteParam(conn,
                        "INSERT INTO GroupMembers " +
                        "(GroupID, UserID, " +
                        "GroupRole, JoinedAt) " +
                        "VALUES " +
                        "(@gid, @uid, 'Leader', @d);",
                        ("@gid", groupID),
                        ("@uid", userID),
                        ("@d",
                         DateTime.Now
                             .ToString("yyyy-MM-dd")));
                }
                else
                {
                    ExecuteParam(conn,
                        "UPDATE GroupMembers " +
                        "SET GroupRole = 'Leader' " +
                        "WHERE GroupID = @gid " +
                        "AND UserID = @uid;",
                        ("@gid", groupID),
                        ("@uid", userID));
                }

                // Demote previous leader if different
                if (prevLeader > 0 &&
                    prevLeader != userID)
                {
                    ExecuteParam(conn,
                        "UPDATE Users SET RoleID = 3 " +
                        "WHERE UserID = @pid;",
                        ("@pid", prevLeader));

                    ExecuteParam(conn,
                        "UPDATE GroupMembers " +
                        "SET GroupRole = 'Member' " +
                        "WHERE GroupID = @gid " +
                        "AND UserID = @pid;",
                        ("@gid", groupID),
                        ("@pid", prevLeader));
                }
            }
        }

        public static DataTable GetMembersOfGroup(
            int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        u.UserID,
                        u.FullName,
                        u.Username,
                        gm.GroupRole,
                        gm.JoinedAt
                    FROM   GroupMembers gm
                    JOIN   Users u
                           ON gm.UserID = u.UserID
                    WHERE  gm.GroupID = @gid
                    ORDER  BY gm.GroupRole ASC;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable
            GetAvailableMembersForGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT u.UserID,
                           u.FullName,
                           u.Username
                    FROM   Users u
                    WHERE  u.RoleID = 3
                    AND    u.UserID NOT IN (
                        SELECT UserID
                        FROM   GroupMembers
                        WHERE  GroupID = @gid
                    )
                    ORDER  BY u.FullName ASC;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // ════════════════════════════════════════════════
        //  TASK QUERIES — all parameterized
        // ════════════════════════════════════════════════

        public static int CreateTask(
                    int groupID,
                    string title,
                    string description,
                    DateTime deadline, // Changed from string to DateTime
                    int? assignedTo)   // Changed from int to int? (nullable int)
        {
            using (var conn = GetConnection())
            {
                // Note: Modified database columns Priority, Points, TaskType in the INSERT query
                // to match the SQLite table columns created in CreateTables (removing non-existent ones)
                string sql = @"
                    INSERT INTO Tasks
                        (GroupID, Title, Description, Deadline, AssignedTo, StatusID, CreatedAt)
                    VALUES
                        (@gid, @title, @desc, @dl, @assigned, 1, DATE('now'));
                    SELECT last_insert_rowid();";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gid", groupID);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@desc", description);

                    // Format DateTime to the SQLite standard string format (YYYY-MM-DD)
                    cmd.Parameters.AddWithValue("@dl", deadline.ToString("yyyy-MM-dd"));

                    // If assignedTo is null, pass DBNull.Value so SQLite writes NULL to the table
                    if (assignedTo.HasValue)
                        cmd.Parameters.AddWithValue("@assigned", assignedTo.Value);
                    else
                        cmd.Parameters.AddWithValue("@assigned", DBNull.Value);

                    return Convert.ToInt32(
                        cmd.ExecuteScalar());
                }
            }
        }

        public static bool TaskTitleExists(
            int groupID, string title)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT COUNT(*) FROM Tasks
                    WHERE  GroupID = @gid
                    AND    LOWER(Title) = LOWER(@title);";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    cmd.Parameters.AddWithValue(
                        "@title", title);
                    return Convert.ToInt32(
                        cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public static DataTable GetTasksByGroup(int groupID)
        {
            using (var conn = GetConnection())
            {
                // Changed JOIN Users to LEFT JOIN Users
                // This ensures tasks show up even if they aren't assigned to anyone yet.
                string sql = @"
            SELECT
                t.TaskID,
                t.Title,
                t.Description,
                t.Deadline,
                COALESCE(u.FullName, 'Unassigned') AS AssignedTo,
                ts.StatusName AS Status
            FROM   Tasks t
            LEFT JOIN Users u
                   ON t.AssignedTo = u.UserID
            JOIN   TaskStatus ts
                   ON t.StatusID = ts.StatusID
            WHERE  t.GroupID = @gid
            ORDER  BY t.Deadline ASC;";

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

        public static DataTable GetTasksByMember(
            int userID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        t.TaskID,
                        t.Title,
                        t.Description,
                        t.Deadline,
                        ts.StatusName AS Status
                    FROM   Tasks t
                    JOIN   TaskStatus ts
                           ON t.StatusID = ts.StatusID
                    WHERE  t.AssignedTo = @uid
                    ORDER  BY t.Deadline ASC;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@uid", userID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void UpdateTaskStatus(
            int taskID, string statusName)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    UPDATE Tasks
                    SET    StatusID = (
                        SELECT StatusID
                        FROM   TaskStatus
                        WHERE  StatusName = @sname
                    )
                    WHERE  TaskID = @tid;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@sname", statusName);
                    cmd.Parameters.AddWithValue(
                        "@tid", taskID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ════════════════════════════════════════════════
        //  SCORE QUERIES
        // ════════════════════════════════════════════════

        public static void RecordScore(
            int taskID,
            int userID,
            DateTime deadline,
            int difficulty)
        {
            decimal basePoints = difficulty * 10;
            decimal penalty = 0;

            if (DateTime.Now.Date > deadline.Date)
            {
                int daysLate =
                    (DateTime.Now.Date -
                     deadline.Date).Days;
                penalty = daysLate * 5;
            }

            decimal finalScore =
                Math.Max(0, basePoints - penalty);

            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Scores
                        (TaskID, UserID, Points,
                         Penalty, FinalScore)
                    VALUES
                        (@tid, @uid, @pts,
                         @pen, @final);";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@tid", taskID);
                    cmd.Parameters.AddWithValue(
                        "@uid", userID);
                    cmd.Parameters.AddWithValue(
                        "@pts", basePoints);
                    cmd.Parameters.AddWithValue(
                        "@pen", penalty);
                    cmd.Parameters.AddWithValue(
                        "@final", finalScore);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetScoresByGroup(
            int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        u.FullName,
                        COUNT(s.ScoreID)  AS TasksDone,
                        SUM(s.FinalScore) AS TotalScore,
                        AVG(s.FinalScore) AS AvgScore
                    FROM   Scores s
                    JOIN   Users u
                           ON s.UserID = u.UserID
                    JOIN   Tasks t
                           ON s.TaskID = t.TaskID
                    WHERE  t.GroupID = @gid
                    GROUP  BY s.UserID;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // ════════════════════════════════════════════════
        //  REPORT QUERIES
        // ════════════════════════════════════════════════

        public static int GenerateReport(
            int groupID, int generatedBy)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    INSERT INTO Reports
                        (GroupID, GeneratedBy,
                         DateGenerated)
                    VALUES
                        (@gid, @by, DATE('now'));
                    SELECT last_insert_rowid();";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    cmd.Parameters.AddWithValue(
                        "@by", generatedBy);
                    return Convert.ToInt32(
                        cmd.ExecuteScalar());
                }
            }
        }

        public static DataTable GetReportsByGroup(
            int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        r.ReportID,
                        u.FullName AS GeneratedBy,
                        r.DateGenerated
                    FROM   Reports r
                    JOIN   Users u
                           ON r.GeneratedBy = u.UserID
                    WHERE  r.GroupID = @gid
                    ORDER  BY r.DateGenerated DESC;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // ════════════════════════════════════════════════
        //  DASHBOARD QUERIES
        // ════════════════════════════════════════════════

        public static DataTable GetTaskSummary(
            int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        ts.StatusName,
                        COUNT(t.TaskID) AS Count
                    FROM   TaskStatus ts
                    LEFT JOIN Tasks t
                           ON ts.StatusID = t.StatusID
                           AND t.GroupID  = @gid
                    GROUP  BY ts.StatusID;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetMemberProgress(
            int groupID)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT
                        u.FullName,
                        COUNT(t.TaskID) AS Total,
                        SUM(CASE
                            WHEN ts.StatusName =
                                 'Completed'
                            THEN 1 ELSE 0
                            END) AS Done,
                        ROUND(
                            SUM(CASE
                                WHEN ts.StatusName =
                                     'Completed'
                                THEN 1.0 ELSE 0
                                END)
                            / NULLIF(
                                COUNT(t.TaskID), 0)
                            * 100, 1
                        ) AS CompletionRate
                    FROM   GroupMembers gm
                    JOIN   Users u
                           ON gm.UserID = u.UserID
                    LEFT JOIN Tasks t
                           ON t.AssignedTo = u.UserID
                           AND t.GroupID   = @gid
                    LEFT JOIN TaskStatus ts
                           ON t.StatusID = ts.StatusID
                    WHERE  gm.GroupID = @gid
                    GROUP  BY u.UserID
                    ORDER  BY u.FullName ASC;";

                using (var cmd =
                    new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@gid", groupID);
                    var da =
                        new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // ════════════════════════════════════════════════
        //  PRIVATE HELPER METHODS
        //  These keep repetitive code DRY and ensure
        //  all queries are always parameterized
        // ════════════════════════════════════════════════

        // Execute a non-query SQL statement
        public static void Execute(
            SQLiteConnection conn, string sql)
        {
            using (var cmd =
                new SQLiteCommand(sql, conn))
                cmd.ExecuteNonQuery();
        }

        // Execute a parameterized non-query
        private static void ExecuteParam(
            SQLiteConnection conn,
            string sql,
            params (string key, object val)[] parms)
        {
            using (var cmd =
                new SQLiteCommand(sql, conn))
            {
                foreach (var (key, val) in parms)
                    cmd.Parameters.AddWithValue(
                        key, val);
                cmd.ExecuteNonQuery();
            }
        }

        // Get a scalar long value
        private static long ScalarLong(
            SQLiteConnection conn, string sql)
        {
            using (var cmd =
                new SQLiteCommand(sql, conn))
                return (long)cmd.ExecuteScalar();
        }

        // Get a scalar long with parameters
        private static long ScalarLongParam(
            SQLiteConnection conn,
            string sql,
            params (string key, object val)[] parms)
        {
            using (var cmd =
                new SQLiteCommand(sql, conn))
            {
                foreach (var (key, val) in parms)
                    cmd.Parameters.AddWithValue(
                        key, val);
                return (long)cmd.ExecuteScalar();
            }
        }
    }
}