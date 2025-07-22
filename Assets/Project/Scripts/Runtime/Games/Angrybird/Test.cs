/*
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Projectile Projectile;
        [SerializeField] private Transform ProjectileLocation;
        [SerializeField] private List<Transform> BirdsLocations;
        
        private ProjectileHandler m_ProjectileHandler;
        private BirdsHandler m_BirdsHandler;
        
        private GameObject m_ProjectileGameObject;


        private void Awake()
        {
            m_ProjectileHandler = new ProjectileHandler(3);
            m_BirdsHandler = new BirdsHandler(3, BirdsLocations);
            m_ProjectileHandler.OnEmpty += OnStackEmpty_Perform;
            m_BirdsHandler.OnListEmpty += OnListEmpty_Perform;
        }

        private static void OnListEmpty_Perform(object sender, EventArgs e)
        {
            Debug.Log("All birds have died");
        }

        private void OnDisable()
        {
            m_ProjectileHandler.OnEmpty -= OnStackEmpty_Perform;
            m_BirdsHandler.OnListEmpty -= OnListEmpty_Perform;
        }

        private void OnStackEmpty_Perform(object sender, EventArgs e)
        {
            Debug.Log("Stack is empty");
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_ProjectileGameObject = m_ProjectileHandler.Current.gameObject;
            }
            
        }
    }
}
*/