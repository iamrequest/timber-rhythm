using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImpactNoteEventListener : MonoBehaviour {
    public ImpactNotePlayedEventChannel eventChannel;
    public UnityEvent onEventPlayed;

    private void OnEnable() {
        eventChannel.onNotePlayed += RaiseEvent;
    }
    private void OnDisable() {
        eventChannel.onNotePlayed -= RaiseEvent;
    }

    public void RaiseEvent(ImpactNote impactNote) {
        // Delegate the event to a local UnityEvent
        onEventPlayed.Invoke();
    }
}
