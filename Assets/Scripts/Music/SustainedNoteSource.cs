using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SustainedNote))]
public class SustainedNoteSource : NoteSource {
    [HideInInspector]
    public SustainedNote note;

    [Range(0f, 1f)]
    private float noteStartT;

    private void Awake() {
        note = GetComponent<SustainedNote>();
        note.noteSource = this;
    }

    private void OnCollisionEnter(Collision collision) {
        // Don't play the same note more than once at the same time
        if (note.isPlaying) return;

        if (LoopMachine.Instance.isRecording) {
            noteStartT = LoopMachine.Instance.t;
        } else {
            noteStartT = 0f;
        }

        note.PlayNote();
    }

    private void OnCollisionExit(Collision collision) {
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
}
