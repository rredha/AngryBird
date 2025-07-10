using System.Collections.Generic;
using UnityEngine;
using Arcade.Project.Core.StateMachine;
using UnityEngine.Serialization;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils.GameState
{
  public class GameManager : MonoBehaviour
  {
    private GameContext m_GameContext;
    public static GameManager current;

    public Spawner Spawner { get; private set; }
    [SerializeField] private Pointer m_Pointer;

    [SerializeField] private Projectile m_Projectile;
    [SerializeField] private Birds m_Bird;

    [SerializeField] private Transform m_ProjectileSpawnLocation;
    [SerializeField] private Transform m_BirdSpawnPosition;

    public GameObject ProjectileRef { get; private set; }

    private void Awake()
    {
      current = this; // singleton
      
      Spawner = GetComponent<Spawner>();
      Spawner.Bird = m_Bird;
      Spawner.Projectile = m_Projectile;
      Spawner.ProjectileLocation = m_ProjectileSpawnLocation;
      Spawner.BirdLocation = m_BirdSpawnPosition;
      
      m_GameContext = new GameContext
       (
         Spawner,
         m_Pointer,
         m_Projectile, m_Bird,
         m_ProjectileSpawnLocation, m_BirdSpawnPosition
       );
    }

    private void Start()
    {
      StartCoroutine(Spawner.Spawn(m_Bird.gameObject, m_BirdSpawnPosition));
      StartCoroutine(Spawner.Spawn(m_Projectile.gameObject, m_ProjectileSpawnLocation));

      ProjectileRef = Spawner.ProjectileRef;
    }
  }
}
