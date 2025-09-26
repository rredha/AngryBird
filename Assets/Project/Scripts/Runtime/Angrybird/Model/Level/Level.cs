using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Model.Level
{
    public class LevelData
    {
        public int Id { get; private set; }
        public int Birds { get; private set; }
        public int Projectiles { get; private set; }
        public Transform ProjectileLocation { get; private set; }
        public List<Transform> BirdsLocations { get; private set; }
        public Transform[] Stages { get; private set; }

        public LevelData(int id,
            int birds, int projectiles,
            Transform projectileLocation,  List<Transform> birdsLocations,
            Transform[] stages)
        {
            Id = id;
            Birds = birds;
            Projectiles = projectiles;

            ProjectileLocation = projectileLocation;
            BirdsLocations = birdsLocations;

            Stages = stages;
        }

    }

    public enum ESelectionTask
    {
        Overlap,
        Click,
        SquarePop,
        HexagonPop
    }
    public class Level
    {
        public int Id { get; set; }
        public LevelData LevelData { get; set; }
        public ESelectionTask SelectionTask;
    }
}
