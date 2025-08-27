using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class User
    {
        public string Name { get; private set; }
        public TaskDataRecord TaskData { get; private set; }

        public User(string name)
        {
            Name = name;
        }
    }
    public class SessionManager : MonoBehaviour
    {
        private User _currentUser;
        private void Awake()
        {
            _currentUser = new User("redha");
        }
    }
}