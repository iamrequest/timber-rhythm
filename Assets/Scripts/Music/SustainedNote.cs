﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

/// <summary>
/// A sustained note. Right now, there is only support for attack/sustain/release (ie: no decay).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SustainedNote: BaseNote {
    // TODO: Replace this with some pooling situation. I don't think I can hook it up to HVR's SFXManager, since I don't have access to its AudioSources
    [HideInInspector]
    public AudioSource audioSource;

    public SustainedNoteSource sustainedNoteSource {
        get {
            return noteSource as SustainedNoteSource;
        }
    }


    // Used to calculate volume between attack/release
    private float timer;
    private float preReleaseVolume;
    private Coroutine stopPlayingAudioCoroutine;

    public bool isPlaying { get; private set; }


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Update() {
        if (audioSource.isPlaying) {
            timer += Time.deltaTime;

            if (isPlaying) {
                audioSource.volume = Mathfs.RemapClamped(0f, 
                    sustainedNoteSource.soundLibrary.attackDuration, 
                    0f, 
                    velocity, 
                    timer);


                // If we want to skip the sustain step (eg: for a xylophone), 
                //  then stop playing this note once the attack phase is complete
                if (sustainedNoteSource.soundLibrary.skipSustain) {
                    if (timer > sustainedNoteSource.soundLibrary.attackDuration) {
                        StopNote();
                    }
                }
            } else {
                audioSource.volume = Mathfs.RemapClamped(0f, 
                    sustainedNoteSource.soundLibrary.attackDuration, 
                    0f, 
                    preReleaseVolume, 
                    sustainedNoteSource.soundLibrary.releaseDuration - timer);
            }
        }
    }

    public override void PlayNote () {
        timer = 0f;
        isPlaying = true;

        // Alert the event channel
        // TODO: Switch this to a sustained note library at some point. For now it just triggers one-shot VFX
        sustainedNoteSource.soundLibrary.notePlayedEventChannel.RaiseOnPlay(null);

        if (stopPlayingAudioCoroutine != null) {
            StopCoroutine(stopPlayingAudioCoroutine);
        }

        if (audioClip == null) {
            // Caching just in case, this should never be null unless I'm doing something in the inspector
            audioClip = sustainedNoteSource.GetAudioClip();
        } else {
            audioSource.clip = audioClip;
        }
        audioSource.volume = 0f; // Setting this to prevent weird clicking noise at the beginning of the audio
        audioSource.Play();
    }

    public override void StopNote() {
        if (!isPlaying) return;

        timer = 0f;
        isPlaying = false;
        preReleaseVolume = audioSource.volume;

        // Make the audio source stop playing after some duration
        stopPlayingAudioCoroutine = StartCoroutine(DoStopAudioSource(sustainedNoteSource.soundLibrary.releaseDuration));
    }

    private IEnumerator DoStopAudioSource(float delay) {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
    }

    public void Copy(SustainedNote sustainedNote) {
        base.Copy(sustainedNote);

        noteSource = sustainedNote.sustainedNoteSource;
        sustainedNoteSource.soundLibrary = sustainedNote.sustainedNoteSource.soundLibrary;
    }

    public override void Delete() {
        // TODO: Probably time to start separating recordings into gameobjects. 
        // I can't delete this audiosource without deleting the note first (because of RequiresComponent).
        //Destroy(audioSource);
        Destroy(this);
    }
}
