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
    public UnityAction onPlay;
    public UnityAction onPause;
    public UnityAction onStop;
    //public UnityAction onRecordingQueued;
    //public UnityAction onRecordingStarted;
    //public UnityAction onRecordingStopped;

    public void RaiseOnPlay() {
        if (onPlay != null) onPlay.Invoke();
    }

    public void RaiseOnPause() {
        if (onPause != null) onPause.Invoke();
    }

    public void RaiseOnStop() {
        if (onStop != null) onStop.Invoke();
    }
}
