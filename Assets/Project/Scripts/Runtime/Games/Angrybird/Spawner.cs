using UnityEngine;
using System.Collections;
using Arcade.Project.Runtime.Games.AngryBird.Interfaces;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  public class Spawner : MonoBehaviour
  {
    public Projectile Projectile { get; set; }
    public GameObject ProjectileRef { get; set; }
    public Birds Bird { get; set; }

    public Transform ProjectileLocation { get; set; }
    public Transform BirdLocation { get; set; }

    public GameObject SpawnedProjectile { get; set; }

    public void CoroutineStartBird()
    {
      StartCoroutine(SpawnBird(BirdLocation));
    }

    public void CoroutineStartProjectile()
    {
      StartCoroutine(SpawnProjectile(ProjectileLocation));
    }

    public IEnumerator Spawn(GameObject prefab, Transform location)
    {

      SpawnedProjectile = Instantiate(prefab,
        location.position ,Quaternion.identity);
      ProjectileRef = SpawnedProjectile;
      yield return null;
    }
    public IEnumerator SpawnProjectile(Transform location)
    {

      SpawnedProjectile = Instantiate(Projectile.gameObject,
                  location.position ,Quaternion.identity);
      yield return null;
    }
    public IEnumerator SpawnProjectile(Transform location, float delay)
    {
      yield return new WaitForSeconds(delay);
      
      SpawnedProjectile = Instantiate(Projectile.gameObject,
        location.position ,Quaternion.identity);
    }

    public IEnumerator SpawnBird(Transform location)
    {

      Instantiate(Bird.gameObject,
                  location.position ,Quaternion.identity);
      yield return null;
    }
  }
}
