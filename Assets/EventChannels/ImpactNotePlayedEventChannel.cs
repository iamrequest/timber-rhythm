using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Impact Note Played Event Channel")]
public class ImpactNotePlayedEventChannel : ScriptableObject {
    public UnityAction<ImpactNote> onNotePlayed;
    public void RaiseOnPlay(ImpactNote impactNote) {
        if (onNotePlayed != null) onNotePlayed.Invoke(impactNote);
    }
}
