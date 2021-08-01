using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManager : ImpactNoteEventListener {
    private List<Fireflies> fireFlySpots;

    private void Awake() {
        fireFlySpots = new List<Fireflies>();
        GetComponentsInChildren(fireFlySpots);
    }

    public override void RaiseEvent(ImpactNote impactNote) {
        fireFlySpots[Random.Range(0, fireFlySpots.Count)].OnNoteEvent();
    }
}
