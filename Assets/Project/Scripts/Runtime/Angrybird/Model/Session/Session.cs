using System;
using Project.Scripts.Runtime.Angrybird.Utils;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class Session
    {
        public DateTime LoginTime { get; set; }
        public User User { get; set; }

        public GameSaveData UserGameData { get; set; }

        public GameConfigurationSO GameConfiguration { get; set; }

        public Session(User user, DateTime dateTime)
        {
            User = user;
            LoginTime = dateTime;
        }
    }
}