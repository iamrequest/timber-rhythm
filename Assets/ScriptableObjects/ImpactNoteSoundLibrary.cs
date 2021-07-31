using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Music/Impact Note Sound Library")]
public class ImpactNoteSoundLibrary : ScriptableObject {
    public List<AudioClip> sounds;

    [Range(0f, 5f)]
    [Tooltip("The minimum collision velocitiy required to make a noise.")]
    public float minVelocity;

    [Range(0f, 10f)]
    [Tooltip("The collision velocities that will create the loudest noise from this note source.")]
    public float maxVelocity;
}
