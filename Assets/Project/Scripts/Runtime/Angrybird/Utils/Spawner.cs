using System.Collections;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Utils
{
  public class Spawner : MonoBehaviour
  {
    public GameObject SpawnedRef { get; private set; }
    public void Spawn(GameObject prefab) => StartCoroutine(SpawnCoroutine(prefab));
    public void SpawnAt(GameObject prefab, Transform location)
    {
      StartCoroutine(SpawnAtCoroutine(prefab, location));
    }

    #region Coroutines
    private IEnumerator SpawnCoroutine(GameObject prefab)
    {
      SpawnedRef = Instantiate(prefab);
      yield return null;
    }

    private IEnumerator SpawnAtCoroutine(GameObject prefab, Transform location)
    {
      SpawnedRef = Instantiate(prefab,
        location.position, Quaternion.identity);
      yield return null;
    }
    
    #endregion
  }
}
