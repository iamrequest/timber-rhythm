using HurricaneVR.Framework.Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A one-shot note that plays at a specfied time T.
/// </summary>
public class ImpactNote : MonoBehaviour {
    //[HideInInspector]
    public int soundClipIndex;

    [Range(0f, 1f)]
    public float velocity = 0.5f;

    [Range(0f, 1f)]
    public float playTime;

    public NoteSource noteSource;


    public void Play() {
        // TODO: Consider switching this to PlaySFXCooldown(). Cooldown should be configurable based on tempo
        SFXPlayer.Instance.PlaySFX(noteSource.soundLibrary.sounds[soundClipIndex],
            noteSource.transform.position,
            1f,
            velocity);
    }
}
