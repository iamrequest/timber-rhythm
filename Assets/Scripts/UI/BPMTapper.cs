using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
public class BPMTapper : MonoBehaviour {
    private bool calculatingBPM;
    private float maxBPM = 300f;

    public List<float> timeDeltas;
    public float timeSinceLastT;

    public void AddTap() {
    }

    private void Update() {
        if (calculatingBPM) {
            timeSinceLastT += Time.deltaTime;

            if (timeSinceLastT > maxBPM) {
                calculatingBPM = false;
                timeDeltas.Clear();
            }
        }
    }

    public void DiscardStaleTaps() {
        // Discard any taps older than x
        int i = 0;
        foreach (float timeDelta in timeDeltas) {
            if (timeDelta > 300f) {
                timeDeltas.RemoveAt(i);
            }

            i++;
        }
    }
}
