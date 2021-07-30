using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRecording : MonoBehaviour {
    public bool isMuted;
    public LoopSection parentLoopSection;
    public List<ImpactNote> notes;

    public void PlayNotes(float startT, float endT) {
        foreach (ImpactNote note in notes) {
            if (note.playTime > startT && note.playTime <= endT) {
                note.Play();
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

            ImpactNote note = notes[notes.Count - 1];
            notes.Remove(note);
            Destroy(note);
        }

        Destroy(this);
    }
}
