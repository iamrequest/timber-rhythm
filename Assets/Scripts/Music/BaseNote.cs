using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNote : MonoBehaviour {
    public NoteSource noteSource;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float playTime, stopTime;

    [Range(0f, 1f)]
    public float velocity;


    public virtual void PlayNote() {}
    public virtual void StopNote() {}

    public void Copy(BaseNote source) {
        noteSource = source.noteSource;
        audioClip = source.audioClip;
        playTime = source.playTime;
        stopTime = source.stopTime;
        velocity = source.velocity;
    }

    public void SetNoteVelocity(BaseSoundLibrary soundLibrary, float collisionMagnitude) {
        velocity = Mathf.InverseLerp(soundLibrary.minVelocity, soundLibrary.maxVelocity, collisionMagnitude);
    }
}
