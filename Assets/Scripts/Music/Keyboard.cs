using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour {
    private List<SustainedNoteSource> sustainedNoteSources;
    public string keyboardName;

    private void Awake() {
        sustainedNoteSources = new List<SustainedNoteSource>();
        GetComponentsInChildren(sustainedNoteSources);
    }

    private void OnDisable() {
        foreach (SustainedNoteSource noteSource in sustainedNoteSources) {
            noteSource.note.StopNoteImmediately();
        }
    }
}
