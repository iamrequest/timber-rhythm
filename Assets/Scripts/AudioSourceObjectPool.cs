using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceObjectPool : ObjectPool {
    public static AudioSourceObjectPool Instance { get; private set; }
    protected override void Awake() {
        base.Awake();
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError($"Multiple {GetType()} components detected. This is probably a bug.");
            Destroy(this);
        }

        if (!prefab.TryGetComponent(out AudioSource a)) {
            Debug.LogError($"ObjectPool prefab is missing and AudioSource Component");
        }
    }

    public AudioSource GetAudioSourceFromPool() {
        GameObject tmp = GetFromPool();
        if(tmp) return tmp.GetComponent<AudioSource>();
        return null;
    }
}
