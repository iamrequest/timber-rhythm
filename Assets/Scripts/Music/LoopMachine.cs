using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMachine : MonoBehaviour {
    public static LoopMachine Instance { get; private set; }
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Multiple LoopManager components detected. This is probably a bug.");
            Destroy(this);
        }
    }


    public bool isPlaying;

    [Header("Tempo")]
    [Range(1, 300)]
    public int bpm = 120;

    // TODO: This works when the beat unit of the time signature (lower number) is quarter notes (regardless of the number of beats per bar). 
    [Range(1, 12)]
    public int beatsPerBar = 4;

    [Range(0f, 1f)]
    public float t;
    private float previousT;

    private float elapsedTime, measureDuration;


    [Header("Loop Section")]
    public List<LoopSection> loopSections;
    public LoopSection activeLoopSection;

    private void Update() {
        if (isPlaying) {
            // Figure out how much time passed between this frame and the last
            CalculateElapsedTime();

            // Play all notes for the active section, that exist between the supplied time values
            if (activeLoopSection) {
                activeLoopSection.PlayNotes(previousT, t);
            }
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

        previousT = t;
        if (elapsedTime >= measureDuration) {
            elapsedTime = 0f;
        }
        t = elapsedTime / measureDuration;
    }

    public void ResetMeasure() {
        elapsedTime = 0f;
        t = 0f;
    }

    public void Play() {
        isPlaying = true;
    }
    public void Pause() {
        isPlaying = false;
    }
    public void Stop() {
        isPlaying = false;
        ResetMeasure();
    }

    public void SetActiveLoopSection(LoopSection newLoopSection) {
        activeLoopSection = newLoopSection;
        ResetMeasure();
    }

    public LoopSection CreateNewSection() {
        LoopSection newSection = gameObject.AddComponent<LoopSection>();
        loopSections.Add(newSection);
        return newSection;
    }
}
