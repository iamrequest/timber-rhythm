using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRecording : MonoBehaviour {
    public bool isMuted;
    public LoopSection parentLoopSection;
    public List<BaseNote> notes;
    public string id;

    private void Awake() {
        if (notes == null) notes = new List<BaseNote>();
    }

    public void RecordNote(BaseNote originalNote, float startT, float endT) {
        // Take a shallow copy of this note
        // There's probably a better OOP way of doing this
        ImpactNote originalImpactNote = originalNote as ImpactNote;
        if (originalImpactNote) {
            ImpactNote newNote = gameObject.AddComponent<ImpactNote>();
            newNote.Copy(originalImpactNote);

            newNote.sourcePositionOverride = LoopMachine.Instance.audioOutputTransform;
            newNote.playTime = startT;
            newNote.stopTime = endT;

            notes.Add(newNote);
        } else {
            SustainedNote originalSustainedNote = originalNote as SustainedNote;
            SustainedNote newNote = gameObject.AddComponent<SustainedNote>();
            newNote.Copy(originalSustainedNote);

            newNote.playTime = startT;
            newNote.stopTime = endT;

            notes.Add(newNote);
        }
    }

    public void PlayNotes(float startT, float endT) {
        foreach (BaseNote note in notes) {
            if (note.playTime > startT && note.playTime <= endT) {
                note.PlayNote();
            }

            if (note.stopTime > startT && note.stopTime <= endT) {
                note.StopNote();
            }
        }
    }

    public void Delete() {
        parentLoopSection.recordings.Remove(this);

        // -- Destroy all notes under this section
        // Gross for loop here, since I was getting stack overflow issues (forgot to remove items from the list). This should be refactored later.
        int tmp = 0;
        int breakCount = notes.Count + 1;
        for (int i = 0; i < notes.Count;) {
            tmp++;
            if (tmp > breakCount) {
                Debug.LogError("Failed to destroy everything, got caught in a stack overflow", this);
                return;
            }

            BaseNote note = notes[notes.Count - 1];
            notes.Remove(note);
            note.Delete();

            //Destroy(note);
        }

        Destroy(this);
    }

    public void SetMuted(bool isMuted) {
        this.isMuted = isMuted;
        foreach (BaseNote note in notes) {
            note.StopNote();
        }
    }
}
