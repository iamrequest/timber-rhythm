﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;
using System;
using HurricaneVR.Framework.Core.Utils;

public class LoopMachine : MonoBehaviour {
    public LoopMachineEventChannel eventChannel;
    public static LoopMachine Instance { get; private set; }
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Multiple LoopManager components detected. This is probably a bug.");
            Destroy(this);
        }
    }
    private void Start() {
        if (activeLoopSection == null) activeLoopSection = CreateNewSection();
    }

    private void OnEnable() {
        eventChannel.onPlay += Play;
        eventChannel.onPause += Pause;
        eventChannel.onStop += Stop;
        eventChannel.onRecordingQueued += QueueRecording;
        eventChannel.onMetronomeSet += SetMetronome;
        eventChannel.onBPMIncrement += IncrementBPM;
        eventChannel.onBPMDecrement += DecrementBPM;
        eventChannel.onBPMSet += SetBPM;
        eventChannel.onTimeSigNumeratorIncrement += IncrementTimeSig;
        eventChannel.onTimeSigNumeratorDecrement += DecrementTimeSig;
    }
    private void OnDisable() {
        eventChannel.onPlay -= Play;
        eventChannel.onPause -= Pause;
        eventChannel.onStop -= Stop;
        eventChannel.onRecordingQueued -= QueueRecording;
        eventChannel.onMetronomeSet -= SetMetronome;
        eventChannel.onBPMIncrement -= IncrementBPM;
        eventChannel.onBPMDecrement -= DecrementBPM;
        eventChannel.onBPMSet -= SetBPM;
        eventChannel.onTimeSigNumeratorIncrement += IncrementTimeSig;
        eventChannel.onTimeSigNumeratorDecrement += DecrementTimeSig;
    }


    public Transform audioOutputTransform;
    public bool isPlaying;
    public bool isRecording, isRecordingQueued;
    public LoopRecording recordingInProgress;

    [Header("Tempo")]
    [Range(1, 300)]
    public int bpm = 120;

    // TODO: This works when the beat unit of the time signature (lower number) is quarter notes (regardless of the number of beats per bar). 
    [Range(1, 12)]
    public int beatsPerMeasure = 4;
    [Range(1, 12)]
    public int measuresPerLoop = 1;

    [Range(0f, 1f)]
    public float t;
    private float previousT;

    private float elapsedTime, measureDuration, loopDuration;

    [Header("Metronome")]
    public bool isMetronomeActive;
    public AudioClip metronomeOnBeatClip, metronomeOffBeatClip;

    [Range(0f, 1f)]
    public float metronomeVolume;

    [Header("Loop Section")]
    public List<LoopSection> loopSections;
    public LoopSection activeLoopSection { get; private set; }

    private void Update() {
        if (isPlaying) {
            // Figure out how much time passed between this frame and the last
            CalculateElapsedTime();

            if (isMetronomeActive) {
                PlayMetronomeSFX(previousT, t);
            }

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
        measureDuration = 60f / ((float)bpm / (float)beatsPerMeasure);
        loopDuration = measureDuration * measuresPerLoop;

        previousT = t;
        if (elapsedTime >= loopDuration) {
            elapsedTime = 0f;

            // If we're getting ready to record next measure, then start recording.
            if (isRecording) SaveRecording();
            if (isRecordingQueued) StartRecording();
        }
        t = elapsedTime / loopDuration;
    }

    private void PlayMetronomeSFX(float previousT, float t) {

        // I can't brain right now, so I'm doing this in a for loop instead of properly figuring out the math
        for (int i = 0; i < beatsPerMeasure; i++) {
            //float firstBeatT = (measureDuration / bpm) * (float)i;
            float beatT = (1f / beatsPerMeasure) * i;

            // If any beat occurred in this frame
            if (beatT >= previousT && beatT <= t) {
                if (i == 0) {
                    SFXPlayer.Instance.PlaySFX(metronomeOnBeatClip, audioOutputTransform.position, 1f, metronomeVolume);
                } else {
                    SFXPlayer.Instance.PlaySFX(metronomeOffBeatClip, audioOutputTransform.position, 1f, metronomeVolume);
                }
            }
        }
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

        if (isRecording) {
            isRecording = false;
            recordingInProgress.Delete();
            recordingInProgress = null;
        }
    }
    public void QueueRecording() {
        isRecordingQueued = true;
    }

    public void SetActiveLoopSection(LoopSection newLoopSection) {
        activeLoopSection = newLoopSection;

        // Never allow for a null active section.
        if (activeLoopSection == null) {
            if (loopSections.Count > 0) {
                // If more exist, pick the first available one
                activeLoopSection = loopSections[0];
            } else {
                // Otherwise, create a new one.
                activeLoopSection = CreateNewSection();
            }
        }

        // TODO: This may change if I allow users to queue up a new section later
        ResetMeasure();
    }

    public LoopSection CreateNewSection() {
        LoopSection newSection = gameObject.AddComponent<LoopSection>();
        loopSections.Add(newSection);
        return newSection;
    }

    private void StartRecording() {
        isRecording = true;
        isRecordingQueued = false;

        recordingInProgress = gameObject.AddComponent<LoopRecording>();
        recordingInProgress.parentLoopSection = activeLoopSection;
        recordingInProgress.id = activeLoopSection.GetNewRecordingID();
    }
    private void SaveRecording() {
        if (recordingInProgress.notes.Count > 0) {
            isRecording = false;

            activeLoopSection.recordings.Add(recordingInProgress);
            eventChannel.RaiseRecordingSaved(recordingInProgress);
            recordingInProgress = null;
        }
    }

    public void IncrementBPM() {
        bpm = Mathfs.Clamp(bpm + 1, 1, 300);
    }
    public void DecrementBPM() {
        bpm = Mathfs.Clamp(bpm - 1, 1, 300);
    }
    public void SetBPM(int bpm) {
        this.bpm = Mathfs.Clamp(bpm - 1, 1, 300);
    }

    public void IncrementTimeSig() {
        beatsPerMeasure = Mathfs.Clamp(beatsPerMeasure + 1, 1, 12);
    }
    public void DecrementTimeSig() {
        beatsPerMeasure = Mathfs.Clamp(beatsPerMeasure - 1, 1, 12);
    }

    public void SetMetronome(bool isMetronomeActive) {
        this.isMetronomeActive = isMetronomeActive;
    }
}
