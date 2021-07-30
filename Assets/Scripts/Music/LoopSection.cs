using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSection : MonoBehaviour {
    public List<LoopRecording> recordings;

    private void Awake() {
        if (recordings == null) recordings = new List<LoopRecording>();
    }

    /// <summary>
    /// Play the notes that take place between the supplied time percentages, for all loop recordings in this section
    /// </summary>
    public void PlayNotes(float startT, float endT) {
        foreach (LoopRecording recording in recordings) {
            if (!recording.isMuted) {
                recording.PlayNotes(startT, endT);
            }
        }
    }

    public void Delete() {
        // -- Cleanup the loop maching
        LoopMachine.Instance.loopSections.Remove(this);

        if (LoopMachine.Instance.activeLoopSection == this) {
            LoopMachine.Instance.SetActiveLoopSection(null);
        }


        // -- Destroy all recordings under this section
        // Gross for loop here, since I was getting stack overflow issues in LoopRecordings. This should be refactored later.
        int tmp = 0;
        int breakCount = recordings.Count + 1;
        for (int i = 0; i < recordings.Count; ) {
            tmp++;
            if (tmp > breakCount) {
                Debug.LogError("Failed to destroy everything, got caught in a stack overflow", this);
                return;
            }

            recordings[recordings.Count - 1].Delete();
        }

        Destroy(this);
    }
}
