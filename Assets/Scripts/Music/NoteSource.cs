using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImpactNote))]
public class NoteSource : MonoBehaviour {
    [HideInInspector]
    public ImpactNote impactNote;

    // The set of layers that will cause this gameobject to play a note on collision
    public LayerMask noiseMakingLayers;

    private void Awake() {
        impactNote = GetComponent<ImpactNote>();
        impactNote.noteSource = this;
    }

    private void OnCollisionEnter(Collision collision) {
        // If not in the layer mask...
        // Source: https://www.codegrepper.com/code-examples/csharp/unity+how+to+use+layermask+and+compare+with+collision+layer
        if ((noiseMakingLayers.value & (1 << collision.collider.gameObject.layer)) <= 0) {
            return;
        }

        // Play the local note on this instrument
        impactNote.Play();

        if (LoopMachine.Instance.isRecording) {
            LoopMachine.Instance.recordingInProgress.AddNote(this, LoopMachine.Instance.t);
        }
    }
}
