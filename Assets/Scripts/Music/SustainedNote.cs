using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

/// <summary>
/// A sustained note. Right now, there is only support for attack/sustain/release (ie: no decay).
/// </summary>
public class SustainedNote: BaseNote {
    private AudioSource audioSource;

    public SustainedNoteSource sustainedNoteSource {
        get {
            return noteSource as SustainedNoteSource;
        }
    }


    // Used to calculate volume between attack/release
    private float timer;
    private float preReleaseVolume;
    private Coroutine stopPlayingAudioCoroutine;

    // This will be false if we're either not playing the note, or we're in the "note release" phase.
    public bool isPlaying { get; private set; }


    private void Update() {
        if (audioSource && audioSource.isPlaying) {
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

        if (!audioSource) {
            // Don't bother fetching a new audio source if we're in the release phase of the note
            audioSource = AudioSourceObjectPool.Instance.GetAudioSourceFromPool();
        }

        if (audioSource) {
            audioSource.loop = true;
            audioSource.volume = 0f; // Setting this to prevent weird clicking noise at the beginning of the audio
            audioSource.clip = audioClip;
            audioSource.transform.position = LoopMachine.Instance.audioOutputTransform.position; // Maybe consider moving this on every frame, for long notes
            audioSource.Play();
        } else {
            // No available audio sources, don't bother playing the note
            isPlaying = false;
        }
    }

    public override void StopNote() {
        if (!isPlaying) return;

        timer = 0f;
        isPlaying = false;
        preReleaseVolume = audioSource.volume;

        // Make the audio source stop playing after some duration
        stopPlayingAudioCoroutine = StartCoroutine(DoStopAudioSource(sustainedNoteSource.soundLibrary.releaseDuration));
    }

    public void StopNoteImmediately() {
        isPlaying = false;

        // Cancel the fade-out 
        if (stopPlayingAudioCoroutine != null) {
            StopCoroutine(stopPlayingAudioCoroutine);
        }

        // Return the audio source to the object pool
        if (audioSource) {
            audioSource.Stop();
            AudioSourceObjectPool.Instance.ReturnToPool(audioSource.gameObject);
            audioSource = null;
        }
    }

    private IEnumerator DoStopAudioSource(float delay) {
        yield return new WaitForSeconds(delay);
        if (audioSource) {
            audioSource.Stop();
            AudioSourceObjectPool.Instance.ReturnToPool(audioSource.gameObject);
            audioSource = null;
        }
    }

    public void Copy(SustainedNote sustainedNote) {
        base.Copy(sustainedNote);

        noteSource = sustainedNote.sustainedNoteSource;
        sustainedNoteSource.soundLibrary = sustainedNote.sustainedNoteSource.soundLibrary;
    }

    public override void Delete() {
        StopNote();
        if (audioSource) {
            audioSource.Stop();
            AudioSourceObjectPool.Instance.ReturnToPool(audioSource.gameObject);
        }
        Destroy(this);
    }
}
