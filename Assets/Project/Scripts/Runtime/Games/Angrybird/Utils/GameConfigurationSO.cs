using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configuration/Game", order = 100)]
    public class GameConfigurationSO : ScriptableObject
    {
        //public float Offset = 0;
        public Transform Platform;
    }
}