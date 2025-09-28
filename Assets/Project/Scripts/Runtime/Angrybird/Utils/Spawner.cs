using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic
{
    private readonly MonoBehaviour _runner;
    private readonly Transform _parent; // dont need these i think, lets check that later.
    private readonly Transform _transform;

    public event Action<GameObject> OnObjectSpawned;
    public event Action<GameObject> OnObjectDestroyed;

    private List<GameObject> _spawnedObjects = new ();
    public IReadOnlyList<GameObject> SpawnedObjects => _spawnedObjects.AsReadOnly();
    public int SpawnedCount => _spawnedObjects.Count;

    public SpawnerLogic(MonoBehaviour runner, Transform parent, Transform transform)
    {
        _runner = runner;
        _parent = parent;
        _transform = transform;
    }
    /// <summary>
    /// Spawn a prefab at the spawner's position
    /// </summary>
    public GameObject Spawn(GameObject prefab, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot spawn null prefab");
            return null;
        }
        
        return SpawnInternal(prefab, _transform.position, _transform.rotation, parent ?? _parent);
    }
    
    /// <summary>
    /// Spawn a prefab at a specific location
    /// </summary>
    public GameObject SpawnAt(GameObject prefab, Vector3 position, Quaternion rotation = default, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot spawn null prefab");
            return null;
        }
        
        if (rotation == default) rotation = Quaternion.identity;
        return SpawnInternal(prefab, position, rotation, parent ?? _parent);
    }
    
    /// <summary>
    /// Spawn a prefab at a transform's position and rotation
    /// </summary>
    public GameObject SpawnAt(GameObject prefab, Transform location, Transform parent = null)
    {
        if (prefab == null || location == null)
        {
            Debug.LogError("Cannot spawn with null prefab or location");
            return null;
        }
        
        return SpawnInternal(prefab, location.position, location.rotation, parent ?? _parent);
    }
    
    /// <summary>
    /// Spawn with delay using coroutine
    /// </summary>
    public void SpawnDelayed(GameObject prefab, float delay, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot spawn null prefab");
            return;
        }
        
        _runner.StartCoroutine(SpawnDelayedCoroutine(prefab, delay, _transform.position, _transform.rotation, parent ?? _parent));
    }
    
    /// <summary>
    /// Spawn with delay at specific location
    /// </summary>
    public void SpawnDelayedAt(GameObject prefab, float delay, Vector3 position, Quaternion rotation = default, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError("Cannot spawn null prefab");
            return;
        }
        
        if (rotation == default) rotation = Quaternion.identity;
        _runner.StartCoroutine(SpawnDelayedCoroutine(prefab, delay, position, rotation, parent ?? _parent));
    }
    
    /// <summary>
    /// Spawn multiple objects in a batch
    /// </summary>
    public List<GameObject> SpawnBatch(GameObject prefab, int count, Vector3 basePosition, Vector3 offset, Transform parent = null)
    {
        List<GameObject> spawned = new List<GameObject>();
        
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = basePosition + (offset * i);
            GameObject obj = SpawnAt(prefab, spawnPos, Quaternion.identity, parent);
            if (obj != null) spawned.Add(obj);
        }
        
        return spawned;
    }
    
    /// <summary>
    /// Destroy a specific spawned object
    /// </summary>
    public void DestroySpawned(GameObject obj)
    {
        if (obj != null && _spawnedObjects.Contains(obj))
        {
            _spawnedObjects.Remove(obj);
            UnityEngine.Object.Destroy(obj);
            OnObjectDestroyed?.Invoke(obj);
        }
    }
    
    /// <summary>
    /// Destroy all spawned objects
    /// </summary>
    public void DestroyAllSpawned()
    {
        for (int i = _spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (_spawnedObjects[i] != null)
            {
                GameObject obj = _spawnedObjects[i];
                _spawnedObjects.RemoveAt(i);
                UnityEngine.Object.Destroy(obj);
                OnObjectDestroyed?.Invoke(obj);
            }
        }
        _spawnedObjects.Clear();
    }
    
    /// <summary>
    /// Get the most recently spawned object (equivalent to your original SpawnedRef)
    /// </summary>
    public GameObject GetLastSpawned()
    {
        return _spawnedObjects.Count > 0 ? _spawnedObjects[_spawnedObjects.Count - 1] : null;
    }
    
    /// <summary>
    /// Check if a specific object was spawned by this spawner
    /// </summary>
    public bool ContainsSpawnedObject(GameObject obj)
    {
        return _spawnedObjects.Contains(obj);
    }
    
    private GameObject SpawnInternal(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject spawnedObject = UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
        _spawnedObjects.Add(spawnedObject);
        OnObjectSpawned?.Invoke(spawnedObject);
        
        return spawnedObject;
    }
    
    private IEnumerator SpawnDelayedCoroutine(GameObject prefab, float delay, Vector3 position, Quaternion rotation, Transform parent)
    {
        yield return new WaitForSeconds(delay);
        SpawnInternal(prefab, position, rotation, parent);
    }

    public void Dispose()
    {
        OnObjectSpawned = null;
        OnObjectDestroyed = null;
        DestroyAllSpawned();
    }
}
public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform defaultParent;
    [SerializeField] private Transform defaultTransform;
    private SpawnerLogic _spawnerLogic;
    public IReadOnlyList<GameObject> SpawnedObjects => _spawnerLogic?.SpawnedObjects;
    public int SpawnedCount => _spawnerLogic?.SpawnedCount ?? 0;
    private void Awake()
    {
        _spawnerLogic = new SpawnerLogic(runner: this, parent: defaultParent, transform: defaultTransform);
    }
    public event Action<GameObject> OnObjectSpawned
    {
        add => _spawnerLogic.OnObjectSpawned += value;
        remove => _spawnerLogic.OnObjectSpawned -= value;
    }
    public event Action<GameObject> OnObjectDestroyed
    {
        add => _spawnerLogic.OnObjectDestroyed += value;
        remove => _spawnerLogic.OnObjectDestroyed -= value;
    }
    private void OnDestroy() => _spawnerLogic?.Dispose();

    #region Methods
    public void Spawn(GameObject prefab, Transform parent) => _spawnerLogic.Spawn(prefab, parent);
    public GameObject SpawnAt(GameObject prefab, Vector3 position, Quaternion rotation = default,
        Transform parent = null) =>
        _spawnerLogic.SpawnAt(prefab, position, rotation, parent);
    public GameObject SpawnAt(GameObject prefab, Transform location, Transform parent = null) =>
        _spawnerLogic.SpawnAt(prefab, location, parent);
    public void SpawnDelayed(GameObject prefab, float delay, Transform parent = null) =>
        _spawnerLogic.SpawnDelayed(prefab, delay, parent);
    public void SpawnDelayedAt(GameObject prefab, float delay, Vector3 position, Quaternion rotation = default,
        Transform parent = null) => _spawnerLogic.SpawnDelayedAt(prefab, delay, position, rotation, parent);
    public List<GameObject> SpawnBatch(GameObject prefab, int count, Vector3 basePosition, Vector3 offset,
        Transform parent = null) => _spawnerLogic.SpawnBatch(prefab, count, basePosition, offset, parent);
    public void DestroySpawned(GameObject obj) => _spawnerLogic.DestroySpawned(obj);
    public void DestroyAllSpawned() => _spawnerLogic.DestroyAllSpawned();
    public GameObject GetLastSpawned() => _spawnerLogic.GetLastSpawned();
    public bool ContainsSpawnedObject(GameObject obj) => _spawnerLogic.ContainsSpawnedObject(obj);

    #endregion
}
