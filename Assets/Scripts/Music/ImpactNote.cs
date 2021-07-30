using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A one-shot note that plays at a specfied time T.
/// </summary>
public class ImpactNote : MonoBehaviour {
    [Range(0f, 1f)]
    public float playTime;

    public void Play() {
        // TODO
        Debug.Log("Playing note", this);
    }
}
