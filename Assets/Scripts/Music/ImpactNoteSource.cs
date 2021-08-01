using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImpactNote))]
public class ImpactNoteSource : NoteSource {
    [HideInInspector]
    public ImpactNote note;
    public ImpactNoteSoundLibrary soundLibrary;

    private void Awake() {
        note = GetComponent<ImpactNote>();
        note.noteSource = this;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!DoesCollisonPlayNote(collision.collider.gameObject.layer)) return;

        // Pick a random sound from this instrument's sound library
        note.audioClip = soundLibrary.sounds[Random.Range(0, soundLibrary.sounds.Count)];

        // Calculate the velocity of this note, based on collision velocity
        note.velocity = Mathf.InverseLerp(soundLibrary.minVelocity, soundLibrary.maxVelocity, collision.relativeVelocity.magnitude);
        //Debug.Log("Velocity: " + collision.relativeVelocity.magnitude.ToString("f2"));
        

        // Play the local note on this instrument
        note.PlayNote();

        if (LoopMachine.Instance.isRecording) {
            LoopMachine.Instance.recordingInProgress.RecordNote(note, LoopMachine.Instance.t, 0f);
        }
    }
}
