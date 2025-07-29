using System.Collections;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Utils.Interfaces
{
    public interface ISpawnableItem
    {
      public IEnumerator Spawn(Transform location, Transform parent);
      public IEnumerator Spawn(Transform location, Transform parent, bool isStatic);
      public IEnumerator Spawn(Transform location, Transform parent, float delay);
      public IEnumerator Spawn(Transform location, Transform parent, bool isStatic, float delay);
    }
}
