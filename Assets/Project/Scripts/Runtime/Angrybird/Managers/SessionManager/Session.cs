using System;
using Project.Scripts.Runtime.Angrybird.Utils;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class Session
    {
        public DateTime LoginTime { get; set; }
        public DateTime SessionBeginTime { get; set; }
        public DateTime SessionEndTime { get; set; }
        public User User { get; set; }

        public UserGameData UserGameData { get; set; }

        public long Id { get; private set; }
        public GameConfigurationSO GameConfiguration { get; set; }

        public Session(User user, DateTime dateTime)
        {
            User = user;
            LoginTime = dateTime;
            Id = LoginTime.ToFileTime();
        }
    }
}