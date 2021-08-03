using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImpactNote))]
public class ImpactNoteSource : NoteSource {
    private HVRGrabbable grabbable; 

    [HideInInspector]
    public ImpactNote note;
    public ImpactNoteSoundLibrary soundLibrary;

    [Range(0f, 1f)]
    public float cooldown;
    private float elapsedCooldown;

    private void Awake() {
        grabbable = GetComponent<HVRGrabbable>();
        note = GetComponent<ImpactNote>();
        note.noteSource = this;
    }

    private void Update() {
        elapsedCooldown = Mathf.Clamp(elapsedCooldown + Time.deltaTime, 0f, cooldown);
    }

    /// <summary>
    /// Useful for playing the note via button input
    /// </summary>
    /// <param name="velocity"></param>
    public void PlayNoteManually(float velocity) {
        note.velocity = velocity;
        DoPlayNote();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!DoesCollisonPlayNote(collision.collider.gameObject.layer)) return;

        // This prevents noise spam when moving an instrument
        if (grabbable.IsBeingHeld) return;

        // Calculate the velocity of this note, based on collision velocity
        note.SetNoteVelocity(soundLibrary, collision.relativeVelocity.magnitude);
        //Debug.Log("Velocity: " + collision.relativeVelocity.magnitude.ToString("f2"));

        DoPlayNote();
    }

    private void DoPlayNote() {
        if (elapsedCooldown < cooldown) return;
        elapsedCooldown = 0f;

        // Pick a random sound from this instrument's sound library
        note.audioClip = soundLibrary.sounds[Random.Range(0, soundLibrary.sounds.Count)];

        // Play the local note on this instrument
        note.PlayNote();

        if (LoopMachine.Instance.isRecording) {
            LoopMachine.Instance.recordingInProgress.RecordNote(note, LoopMachine.Instance.t, 0f);
        }
    }
}
