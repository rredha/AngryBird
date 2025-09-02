using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Utils
{
    [CreateAssetMenu(fileName = "Level", menuName = "Configuration/Level", order = 100)]
    public class LevelSO : ScriptableObject
    {
        public int LevelIndex;
        public GameObject BirdPrefab;
        public GameObject ProjectilePrefab;
        
        public int Birds;
        public List<Transform> BirdsLocations;
        public int Projectiles;

        public Transform[] Stages;
    }
}