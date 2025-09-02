using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class UserListController
    {
        private VisualTreeAsset _listEntryTemplate;

        private ListView _userListView;
        private Label _userNameLabel;
        private Button _userLoginButton;
        private VisualElement _userPortrait;

        private List<User> _allUsers;
        private User _selectedUser;
        public event EventHandler<User> SessionStart;

        public void InitializeUserList(VisualElement root, VisualTreeAsset listElementTemplate, List<User> userList)
        {
            EnumerateAllUsers(userList);

            _listEntryTemplate = listElementTemplate;
            _userListView = root.Q<ListView>("user-list");
            _userNameLabel = root.Q<Label>("user-name");
            _userPortrait = root.Q<VisualElement>("user-portrait");
            _userLoginButton = root.Q<Button>("user-login");

            FillUserList();
            _userListView.onSelectionChange += OnUserSelected;
            _userLoginButton.clicked += OnUserLoginButtonClicked_CreateSession;
        }

        private void OnUserLoginButtonClicked_CreateSession()
        {
           SessionStart?.Invoke(this, _selectedUser); 
        }

        private void OnUserSelected(IEnumerable<object> selectedItems)
        {
            _selectedUser = _userListView.selectedItem as User;

            if (_selectedUser == null)
            {
                _userNameLabel.text = "";
                _userPortrait.style.backgroundImage = null;
                
                return;
            }

            _userNameLabel.text = _selectedUser.Username;
            // get an image from session manager, ou ui controller.
            // _userPortrait.style.backgroundImage = new StyleBackground(_selectedUser.PortraitImage); 

        }

        private void FillUserList()
        {
            _userListView.makeItem = () =>
            {
                var newListEntry = _listEntryTemplate.Instantiate();
                var newListEntryController = new UserListEntryController();
                newListEntry.userData = newListEntryController;
                newListEntryController.SetVisualElement(newListEntry);
                return newListEntry;
            };
            _userListView.bindItem = (item, index) =>
            {
                (item.userData as UserListEntryController)?.SetUserData(_allUsers[index]);
            };

            _userListView.fixedItemHeight = 45;
            _userListView.itemsSource = _allUsers;
        }

        private void EnumerateAllUsers(List<User> userList)
        {
            _allUsers = userList;
        }
    }
    public class UserListEntryController
    {
        private Label _nameLabel;

        public void SetVisualElement(VisualElement visualElement)
        {
            _nameLabel = visualElement.Q<Label>("user-name");
        }

        public void SetUserData(User userData)
        {
            _nameLabel.text = userData.Username;
        }
    }
}