using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invoke RaiseEvent() whenever some condition is met. This approach decouples any settings interfaces from targets.
/// GameObjects can even exist between different scenes in this approach.
/// https://www.youtube.com/watch?v=WLDgtRNK2VE
/// </summary>
[CreateAssetMenu(menuName = "Event Channels/Loop Machine Event Channel")]
public class LoopMachineEventChannel : ScriptableObject {
    // -- These trigger the actual action
    public UnityAction onPlay;
    public UnityAction onPause;
    public UnityAction onStop;
    public UnityAction onRecordingQueued;
    public UnityAction onBPMIncrement, onBPMDecrement;
    public UnityAction<int> onBPMSet;
    public UnityAction onTimeSigNumeratorIncrement, onTimeSigNumeratorDecrement;
    //public UnityAction onRecordingStopped;

    // -- These trigger in response to the actual action
    public UnityAction<LoopRecording> recordingSaved;
    public UnityAction<LoopRecording> recordingDeleted;
    //public UnityAction onRecordingStarted;

    public void RaiseOnPlay() {
        if (onPlay != null) onPlay.Invoke();
    }

    public void RaiseOnPause() {
        if (onPause != null) onPause.Invoke();
    }

    public void RaiseOnStop() {
        if (onStop != null) onStop.Invoke();
    }
    public void RaiseOnRecordingQueued() {
        if (onRecordingQueued != null) onRecordingQueued.Invoke();
    }

    public void RaiseRecordingSaved(LoopRecording savedRecording) {
        if (recordingSaved != null) recordingSaved.Invoke(savedRecording);
    }
    public void RaiseRecordingDeleted(LoopRecording savedRecording) {
        if (recordingDeleted != null) recordingDeleted.Invoke(savedRecording);
    }
    public void RaiseOnBPMIncrement() {
        if (onBPMIncrement != null) onBPMIncrement.Invoke();
    }
    public void RaiseOnBPMDecrement() {
        if (onBPMDecrement != null) onBPMDecrement.Invoke();
    }
    public void RaiseOnBPMSet(int newBPM) {
        if (onBPMSet != null) onBPMSet.Invoke(newBPM);
    }
    public void RaiseOnTimeSigNumeratorIncrement() {
        if (onTimeSigNumeratorIncrement != null) onTimeSigNumeratorIncrement.Invoke();
    }
    public void RaiseOnTimeSigNumeratorDecrement() {
        if (onTimeSigNumeratorDecrement != null) onTimeSigNumeratorDecrement.Invoke();
    }
}
