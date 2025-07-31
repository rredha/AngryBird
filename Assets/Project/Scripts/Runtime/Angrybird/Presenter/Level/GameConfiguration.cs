using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class GameConfiguration
    {
        public Transform Platform => _platform;
        private Transform _platform;

        public GameConfiguration(Transform platform)
        {
            _platform = platform;
        }
    }
}