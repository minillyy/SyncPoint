namespace SyncPoint.Data
{
    public static class Session
    {
        public static int UserID { get; set; }
        public static string FullName { get; set; }
        public static string Username { get; set; }
        public static int RoleID { get; set; }
        public static string RoleName { get; set; }
        public static int GroupID { get; set; }

        public static bool IsLeader => RoleName == "Leader";
        public static bool IsMember => RoleName == "Member";

        public static void Clear()
        {
            UserID = 0; 
            FullName = string.Empty; 
            Username = string.Empty;
            RoleID = 0;
            RoleName = string.Empty; ; 
            GroupID = -1;
        }
    }
}
