using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Model.Level;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public enum LevelStatusEnum
    {
       Completed,
       UnCompleted
    };
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevel { get; set; }
        public Projectile Projectile { get; set; }
        public ProjectileHandler ProjectileHandler { get; set; }
        public BirdsHandler BirdsHandler { get; set; }
        public int Attempt => ProjectileHandler.ProjectileLeft;
        public bool IsInitialized;
        private int _numberOfBirds;
        public LevelStatusEnum LevelStatus => BirdsHandler.AllBirdsDestroyed ? LevelStatusEnum.Completed : LevelStatusEnum.UnCompleted;
        public bool OutOfAttempts { get; private set; }

        public void Setup()
        {
            IsInitialized = true;
            if (ProjectileHandler == null)
                Debug.LogError("ProjectileHandler not found");
            ProjectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
        }
        private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
        {
            OutOfAttempts = true;
        }
        public void Proceed()
        {
            ProjectileHandler.GetProjectile();
            Projectile = ProjectileHandler.Current;
        }
        public void UnloadLevel()
        {
            ProjectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
            
            OutOfAttempts = false;
            IsInitialized = false;
        }
    }
}