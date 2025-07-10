using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils
{
  public class GameContext
  {
    private Spawner m_Spawner;
    private Pointer m_Pointer;

    private Projectile m_Projectile;
    private Birds m_Bird;

    private Transform m_ProjectileSpawnPosition;
    private Transform m_BirdSpawnPosition;

    public Spawner Spawner => m_Spawner;
    public Pointer Pointer => m_Pointer;
    public Projectile Projectile => m_Projectile;
    public Birds Bird => m_Bird;
    public Transform ProjectileSpawnPosition => m_ProjectileSpawnPosition;
    public Transform BirdSpawnPosition => m_BirdSpawnPosition;

    public GameContext
      (
       Spawner spawner,
       Pointer pointer,
       Projectile projectile, Birds bird,
       Transform projectileSpawnPosition, Transform birdSpawnPosition
      )
      {
       m_Spawner = spawner;
       m_Pointer = pointer;

       m_Projectile = projectile;
       m_Bird = bird;

       m_ProjectileSpawnPosition = projectileSpawnPosition;
       m_BirdSpawnPosition = birdSpawnPosition;
      }

  }
}
