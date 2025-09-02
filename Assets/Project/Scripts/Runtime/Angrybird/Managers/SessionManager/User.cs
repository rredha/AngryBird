namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class UserPersonalData
    {
        public int LastLevel { get; set; }
        public int MaxScore { get; set; }
        
    }
    public class UserGameData
    {
        public int LastLevel { get; set; }
        public int MaxScore { get; set; }
    }
    public class User
    {
        public string Username { get;  set; }
        public string DataPath { get;  set; }
        public string ConfigPath { get;  set; }

    }
}