using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

/// <summary>
/// A sustained note. Right now, there is only support for attack/sustain/release (ie: no decay).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SustainedNote: BaseNote {
    // TODO: Replace this with some pooling situation. I don't think I can hook it up to HVR's SFXManager, since I don't have access to its AudioSources
    private AudioSource audioSource;

    // Used to calculate volume between attack/release
    private float timer;
    private float preReleaseVolume;
    private Coroutine stopPlayingAudioCoroutine;

    public bool isPlaying { get; private set; }

    public float attackDuration, releaseDuration;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Update() {
        if (audioSource.isPlaying) {
            timer += Time.deltaTime;

            if (isPlaying) {
                audioSource.volume = Mathfs.RemapClamped(0f, attackDuration, 0f, velocity, timer);
            } else {
                audioSource.volume = Mathfs.RemapClamped(0f, attackDuration, 0f, preReleaseVolume, releaseDuration - timer);
            }
        }
    }

    public override void PlayNote () {
        timer = 0f;
        isPlaying = true;

        if (stopPlayingAudioCoroutine != null) {
            StopCoroutine(stopPlayingAudioCoroutine);
        }

        audioSource.clip = audioClip;
        audioSource.volume = 0f; // Setting this to prevent weird clicking noise at the beginning of the audio
        audioSource.Play();
    }

    public override void StopNote() {
        if (!isPlaying) return;

        timer = 0f;
        isPlaying = false;
        preReleaseVolume = audioSource.volume;

        // Make the audio source stop playing after some duration
        stopPlayingAudioCoroutine = StartCoroutine(DoStopAudioSource(releaseDuration));
    }

    private IEnumerator DoStopAudioSource(float delay) {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
    }

    public void Copy(SustainedNote sustainedNote) {
        base.Copy(sustainedNote);

        attackDuration = sustainedNote.attackDuration;
        releaseDuration = sustainedNote.releaseDuration;
    }
}
