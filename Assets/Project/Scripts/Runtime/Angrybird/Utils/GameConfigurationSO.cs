using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Utils
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configuration/Game", order = 100)]
    public class GameConfigurationSO : ScriptableObject
    {
        //public float Offset = 0;
        public Transform Platform;
    }
}