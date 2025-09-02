using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Project.Scripts.Runtime.Angrybird.Managers;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance;
        public Session Session { get; set; }
        
        // on Login button start -> dispatch session start event.
        // on application quit -> dispatch session end.
        private const string Path = "/home/redha/angrybird_data/users";
        private List<User> _users;
        private UserListController _userListController = new ();
        public event EventHandler SessionStart;
        public event EventHandler SessionEnd;
        

        private UIDocument _uiDocument;
        [SerializeField] private VisualTreeAsset _listEntryTemplate;
        private List<UserGameData> _usersGameData;

        private void OnEnable()
        {
            PopulateUserMenu();
            
            _uiDocument = GetComponent<UIDocument>();
            
            _userListController.InitializeUserList(_uiDocument.rootVisualElement, _listEntryTemplate, _users);
            
            SessionEnd += OnSessionEnd_SaveData;
            _userListController.SessionStart += OnSessionStart_CreateNewSession;
            _userListController.SessionStart += OnSessionStart_LoadUserData;
            _userListController.SessionStart += OnSessionStart_LoadMainScene;
        }


        private void OnSessionStart_CreateNewSession(object sender, User user)
        {
            Session = new Session(user, DateTime.Now);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void PopulateUserMenu()
        {
            using (var reader = new StreamReader("/home/redha/angrybird_data/users.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _users =  csv.GetRecords<User>().ToList();
            }
        }

        private void OnSessionStart_LoadMainScene(object sender, User user)
        {
            SceneManager.LoadScene("Main");
        }

        private void OnDisable()
        {
            _userListController.SessionStart += OnSessionStart_CreateNewSession;
            _userListController.SessionStart += OnSessionStart_LoadUserData;
            _userListController.SessionStart += OnSessionStart_LoadMainScene;
            SessionEnd -= OnSessionEnd_SaveData;
        }

        private void OnSessionStart_LoadUserData(object sender, User user)
        {
            // get configuration from data.
            // get score + latest level.
            using (var reader = new StreamReader(user.DataPath+"game_data.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _usersGameData =  csv.GetRecords<UserGameData>().ToList();
            }

            Session.UserGameData = _usersGameData.Last();

        }

        private void OnSessionEnd_SaveData(object sender, EventArgs e)
        {
            // get all data from survey that were answered.
            // get all time related data
            // get all score related data
            
            // save latest configuration from in game
            // if ingame config is different from imported config
            // then imported config = in game config.
        }
        private void CreateFolder()
        {
            var orginPath = "/home/redha/";
            var path = Session.LoginTime.ToShortTimeString();
            // proceed to create directory.
            // username / sessionID.
        }
        private void LoadGameConfiguration(string configurationDataCSV)
        {
           /* configuration data in csv is a config file for the session
           / attached the user
           */
           GameConfigurationSO gameConfig = new GameConfigurationSO();
           Session.GameConfiguration = gameConfig;
        }
    }
}