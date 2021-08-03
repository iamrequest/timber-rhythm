using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Usage: Override with unique component, adding the below code
///     public static [ComponentName] Instance { get; private set; }
///     base.Awake();
///     private void Awake() {
///         if (Instance == null) {
///             Instance = this;
///         } else {
///             Debug.LogError($"Multiple {GetType()} components detected. This is probably a bug.");
///             Destroy(this);
///         }
///     }
/// </summary>
public abstract class ObjectPool : MonoBehaviour {
    public GameObject prefab;
    public int initialPoolSize;
    public HashSet<GameObject> pool;
    private HashSet<GameObject> poolInUse;

    protected virtual void Awake() {
        InitPool();
        Debug.Log(pool);
        Debug.Log(poolInUse);
    }

    private void InitPool() {
        pool = new HashSet<GameObject>();
        poolInUse = new HashSet<GameObject>();
        GameObject newObject;

        for (int i = 0; i < initialPoolSize; i++) {
            newObject = Instantiate(prefab, transform); // Optional: Do not instantiate under this transform, for performance reasons.
            newObject.SetActive(false);
            pool.Add(newObject);
        }
    }

    public GameObject GetFromPool() {
        GameObject obj = GetElementFromPool();
        if (obj != null) {
            obj.SetActive(true);
            pool.Remove(obj);
            poolInUse.Add(obj);
            return obj;
        } else {
            // No objects available in pool
            Debug.LogWarning($"Attempted to fetch an object to this pool ({GetType()}), but no elements were available. Consider increasing the pool size.", obj);
            return null;
        }
    }

    // Can't remove the object from the pool during the foreach loop, so a second fetch method is required
    private GameObject GetElementFromPool() {
        foreach (GameObject obj in pool) {
            if (!obj.activeInHierarchy) {
                return obj;
            }
        }
        return null;
    }

    /// <summary>
    /// Always make sure to set your object reference to null after calling this!
    /// </summary>
    /// <param name="obj"></param>
    public void ReturnToPool(GameObject obj) {
        // If I'm trying to return nothing to the object pool, do nothing
        //  This can happen if I try to take something from an empty object pool, and then return it. 
        if (!obj) {
            Debug.LogWarning($"Attempted to return a null object to this pool ({GetType()})", obj);
        }

        if (poolInUse.Remove(obj)) {
            pool.Add(obj);
            obj.SetActive(false);
            obj = null;
        } else {
            Debug.LogError($"Attempted to return an object to this pool ({GetType()}), but it was not in the original set", obj);
        }
    }
}
