using System;
using UnityEngine;
using System.Collections;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  public class DependencyEventArgs : EventArgs
  {
    public Projectile Projectile { get; set; }
    public Transform ProjectileLocation { get; set; }

    public Birds Bird { get; set; }
    public Transform BirdLocation { get; set; }
  }
  public class Spawner : MonoBehaviour
  {
    //public static Spawner current;
    
    [SerializeField] private Projectile m_Projectile;
    [SerializeField] private Birds m_Bird;
    [SerializeField] private Transform m_ProjectileLocation;
    [SerializeField] private Transform m_BirdLocation;
    public static event EventHandler<DependencyEventArgs> OnDependencySatisfied;
    
    public GameObject ProjectileRef { get; set; }
    public GameObject SpawnedProjectile { get; set; }
    public GameObject SpawnedBird { get; set; }

    //private void Awake() => current = this;
    private void Awake()
    {
     // current = this;

      var deps = new DependencyEventArgs
      {
        Projectile = m_Projectile,
        Bird = m_Bird,
        ProjectileLocation = m_ProjectileLocation,
        BirdLocation = m_BirdLocation,
      };
      
      OnDependencySatisfied?.Invoke(this, deps);
    }

    public void CoroutineStartProjectile() 
      => StartCoroutine(SpawnProjectile(m_ProjectileLocation));

    public IEnumerator Spawn(GameObject prefab, Transform location)
    {
      SpawnedProjectile = Instantiate(prefab,
        location.position ,Quaternion.identity);
      ProjectileRef = SpawnedProjectile;
      
      yield return null;
    }

    public void SpawnNewProjectile(Transform location)
    {
      StartCoroutine(SpawnProjectile(location));
    }
    private IEnumerator SpawnProjectile(Transform location)
    {
      SpawnedProjectile = Instantiate(m_Projectile.gameObject,
                  location.position ,Quaternion.identity);
      
      yield return null;
    }

    public void SpawnProjectile()
    {
      StartCoroutine(SpawnProjectile(m_ProjectileLocation));
    }

    public void SpawnBirds(Transform location)
    {
      StartCoroutine(SpawnBirdCoroutine(location));
    }
    public IEnumerator SpawnBirdCoroutine(Transform location)
    {
      SpawnedBird = Instantiate(m_Bird.gameObject,
                     location.position ,Quaternion.identity);
      
      yield return null;
    }
  }
}
