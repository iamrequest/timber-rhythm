using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImpactNote))]
public class NoteSource : MonoBehaviour {
    [HideInInspector]
    public ImpactNote impactNote;

    public ImpactNoteSoundLibrary soundLibrary;

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

        // Pick a random sound from this instrument's sound library
        impactNote.soundClipIndex = Random.Range(0, soundLibrary.sounds.Count);

        // Calculate the velocity of this note, based on collision velocity
        impactNote.velocity = Mathf.InverseLerp(soundLibrary.minVelocity, soundLibrary.maxVelocity, collision.relativeVelocity.magnitude);
        Debug.Log("Velocity: " + collision.relativeVelocity.magnitude.ToString("f2"));
        

        // Play the local note on this instrument
        impactNote.Play();

        if (LoopMachine.Instance.isRecording) {
            LoopMachine.Instance.recordingInProgress.RecordNote(impactNote, LoopMachine.Instance.t);
        }
    }
}
