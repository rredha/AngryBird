using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class LoginUI : MonoBehaviour
    {
        private List<User> _users;
        private UserListController _userListController = new ();
        private UIDocument _uiDocument;
        [SerializeField] private VisualTreeAsset _listEntryTemplate;
        private List<GameSaveData> _usersGameData;
        
        private void OnEnable()
        {
            PopulateUserMenu();
            
            _uiDocument = GetComponent<UIDocument>();
            
            _userListController.InitializeUserList(_uiDocument.rootVisualElement, _listEntryTemplate, _users);
            
            _userListController.SessionStart += CreateNewSession;
            _userListController.SessionStart += LoadUserData;
            _userListController.SessionStart += LoadMainScene;
        }

        private void CreateNewSession(object sender, User user)
        {
            var session = new Session(user, DateTime.Now);
            SessionManager.Instance.Session = session;
        }

        public void LoadMainScene(object sender, User user)
        {
            SceneManager.LoadScene("Main");
        }
        private void OnDisable()
        {
            _userListController.SessionStart -= CreateNewSession;
            _userListController.SessionStart -= LoadUserData;
            _userListController.SessionStart -= LoadMainScene;
        }
        private void LoadUserData(object sender, User user)
        {
            // TODO : 
            // Should represent save file to load.
            
            _usersGameData = new List<GameSaveData>();
            using (var reader = new StreamReader(user.DataPath+"game_data.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _usersGameData =  csv.GetRecords<GameSaveData>().ToList();
            }

            SessionManager.Instance.Session.UserGameData = _usersGameData.Last();
        }
        private void PopulateUserMenu()
        {
            // TODO : Tidy up user menu generation.
            using (var reader = new StreamReader("/home/redha/angrybird_data/users.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _users =  csv.GetRecords<User>().ToList();
            }
        }
    }
}