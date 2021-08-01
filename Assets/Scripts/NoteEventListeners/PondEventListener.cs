using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

public class PondEventListener : ImpactNoteEventListener {
    [Range(0f, 1f)]
    public float maxRipple;
    [Range(0f, 1f)]
    public float releaseDuration;
    private float timeSinceLastNote;
    private Renderer m_renderer;
    private float rippleStrength;

    private void Awake() {
        m_renderer = GetComponent<Renderer>();
        timeSinceLastNote = releaseDuration;
    }
    private void Update() {
        timeSinceLastNote += Time.deltaTime;
        rippleStrength = Mathfs.RemapClamped(0f, releaseDuration, maxRipple, 0f, timeSinceLastNote);
        m_renderer.material.SetFloat("_rippleStrength", rippleStrength);
    }
    public override void ProcessEvent(ImpactNote impactNote) {
        timeSinceLastNote = 0f;
    }
}
