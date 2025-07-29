using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Model.Level
{
    public class Level
    {
        public int Birds { get; private set; }
        public List<Transform> BirdsLocations { get; private set; }
        public Transform[] Stages { get; private set; }
    
        public int Projectiles { get; private set; }

        public Level(int birds, int projectiles, List<Transform> birdsLocations, Transform[] stages)
        {
            Birds = birds;
            BirdsLocations = birdsLocations;

            Projectiles = projectiles;

            Stages = stages;
        }
    }
}