using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSoundLibrary : ScriptableObject {
    [Range(0f, 5f)]
    [Tooltip("The minimum collision velocitiy required to make a noise.")]
    public float minVelocity = 0f;

    [Range(0f, 10f)]
    [Tooltip("The collision velocities that will create the loudest noise from this note source.")]
    public float maxVelocity = 1f;
}
