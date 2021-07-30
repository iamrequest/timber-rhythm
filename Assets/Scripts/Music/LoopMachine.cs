using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMachine : MonoBehaviour {
    public bool isPlaying;

    [Range(1, 300)]
    public int bpm = 120;

    // TODO: This works when the beat unit of the time signature (lower number) is quarter notes (regardless of the number of beats per bar). 
    [Range(1, 12)]
    public int beatsPerBar = 4;

    [Range(0f, 1f)]
    public float t;

    private float elapsedTime, measureDuration;


    private void Update() {
        if (isPlaying) {
            CalculateElapsedTime();
        }
    }

    /// <summary>
    /// Process the elapsed time from this frame, updating the internal state of the metronome.
    /// </summary>
    private void CalculateElapsedTime() {
        elapsedTime += Time.deltaTime;

        // Calculate the duration of the whole bar, just in case the BPM updates.
        // TODO: Probably can move this somewhere else, so I'm not calculating it every frame
        measureDuration = 60f / ((float)bpm / (float)beatsPerBar);

        if (elapsedTime >= measureDuration) {
            elapsedTime = 0f;
        }
        t = elapsedTime / measureDuration;
    }

    public void Play() {
        elapsedTime = 0f;
        isPlaying = true;
    }
    public void Pause() {
        elapsedTime = 0f;
        isPlaying = false;
    }
}
