using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImpactNoteEventListener : MonoBehaviour {
    public ImpactNotePlayedEventChannel eventChannel;
    public UnityEvent onEventPlayed;

    [Range(0f, 1f)]
    [Tooltip("The chance that this event listener will process the event")]
    public float eventResponseChance;

    private void OnEnable() {
        eventChannel.onNotePlayed += RaiseEvent;
    }
    private void OnDisable() {
        eventChannel.onNotePlayed -= RaiseEvent;
    }

    /// <summary>
    /// Called on every event
    /// </summary>
    /// <param name="impactNote"></param>
    public virtual void RaiseEvent(ImpactNote impactNote) {
        // Delegate the event to a local UnityEvent, if we pass the random chance check
        if (eventResponseChance > Random.Range(0f, 1f)) {
            onEventPlayed.Invoke();
            ProcessEvent(impactNote);
        }
    }

    /// <summary>
    /// Called when the RNG check succeeds
    /// </summary>
    /// <param name="impactNote"></param>
    public virtual void ProcessEvent(ImpactNote impactNote) { }
}
