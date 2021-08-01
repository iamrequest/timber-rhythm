using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fireflies : MonoBehaviour {
    private VisualEffect vfx;
    private float timeSinceLastEvent = 0f;

    private void Awake() {
        vfx = GetComponent<VisualEffect>();
        vfx.Stop();
    }

    private void Update() {
        timeSinceLastEvent += Time.deltaTime;
        vfx.SetFloat("TimeSinceLastNote", timeSinceLastEvent);
        if (timeSinceLastEvent > 10f) {
            vfx.Stop();
        } 
    }

    public void OnNoteEvent() {
        timeSinceLastEvent = 0f;
        vfx.Play();
    }
}
