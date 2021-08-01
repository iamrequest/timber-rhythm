using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Calls vfx.Play() for a random VisualEffect, when an impact event happens
/// </summary>
public class OneShotVFXEventListener : ImpactNoteEventListener {
    private List<VisualEffect> vfxList;

    private void Awake() {
        vfxList = new List<VisualEffect>();
        GetComponentsInChildren(vfxList);
    }

    public override void RaiseEvent(ImpactNote impactNote) {
        base.RaiseEvent(impactNote);
        vfxList[Random.Range(0, vfxList.Count)].Play();
    }
}
