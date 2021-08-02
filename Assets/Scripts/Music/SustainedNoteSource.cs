using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SustainedNote))]
public class SustainedNoteSource : NoteSource {
    [HideInInspector]
    public SustainedNote note;

    public SustainedNoteSoundLibrary soundLibrary;

    [Tooltip("Not zero indexed.")]
    [Range(1, 5)]
    public int octave = 3;
    public ToneNote noteName;

    [Range(0f, 1f)]
    private float noteStartT;

    private int touchCount;

    private void Awake() {
        note = GetComponent<SustainedNote>();
        note.noteSource = this;
        note.audioClip = soundLibrary.octaves[octave - 1].GetNote(noteName);
    }

    private void OnCollisionEnter(Collision collision) {
        touchCount++;
        // Don't play the same note more than once at the same time
        if (note.isPlaying) return;

        // Fetch the audio clip to use
        if (!soundLibrary.octaves[octave - 1]) {
            Debug.LogError("Sound library isn't initialized properly");
            return;
        }


        // Mark down when we started playing this note, for the purposes of recording
        if (LoopMachine.Instance.isRecording) {
            noteStartT = LoopMachine.Instance.t;
        } else {
            noteStartT = 0f;
        }

        //note.velocity = Mathf.InverseLerp(soundLibrary.minVelocity, soundLibrary.maxVelocity, collision.relativeVelocity.magnitude);
        note.SetNoteVelocity(soundLibrary, collision.relativeVelocity.magnitude);
        note.PlayNote();
    }

    private void OnCollisionExit(Collision collision) {
        touchCount--;
        if (touchCount > 0) return;
        note.StopNote();

        if (LoopMachine.Instance.isRecording) {
            // TODO: Swap this out for some event when the loop machine finishes its loop
            float noteEndT;
            if (LoopMachine.Instance.isRecording) {
                noteEndT = LoopMachine.Instance.t;
            } else {
                noteEndT = 1f;
            }

            LoopMachine.Instance.recordingInProgress.RecordNote(note, noteStartT, noteEndT);
        }
    }

    public AudioClip GetAudioClip() {
        // Fetch the audio clip to use
        if (!soundLibrary.octaves[octave - 1]) {
            Debug.LogError("Sound library isn't initialized properly");
            return null;
        }

        return soundLibrary.octaves[octave - 1].GetNote(noteName);
    }
}
