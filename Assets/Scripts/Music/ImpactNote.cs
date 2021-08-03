using HurricaneVR.Framework.Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A one-shot note that plays at a specfied time T.
/// No unique behaviour on StopNote(), since this is a short burst of noise.
/// </summary>
public class ImpactNote : BaseNote {
    public Transform sourcePositionOverride;

    public override void PlayNote() {
        ImpactNoteSource impactNoteSource = noteSource as ImpactNoteSource;
        if (impactNoteSource) {
            impactNoteSource.soundLibrary.notePlayedEventChannel.RaiseOnPlay(this);
        }

        // TODO: Consider switching this to PlaySFXCooldown(). Cooldown should be configurable based on tempo
        if (sourcePositionOverride == null) {
            SFXPlayer.Instance.PlaySFX(audioClip,
                noteSource.transform.position,
                1f,
                velocity);
        } else {
            SFXPlayer.Instance.PlaySFX(audioClip,
                sourcePositionOverride.position,
                1f,
                velocity);
        }
    }
}
