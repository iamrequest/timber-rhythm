using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invoke RaiseEvent() whenever some condition is met. This approach decouples any settings interfaces from targets.
/// GameObjects can even exist between different scenes in this approach.
/// https://www.youtube.com/watch?v=WLDgtRNK2VE
/// </summary>
[CreateAssetMenu(menuName = "Event Channels/Settings Menu Event Channel")]
public class SettingsMenuEventChannel : ScriptableObject {
    public UnityAction<bool> onMenuOpened;

    public void RaiseOnMenuOpened(bool isMenuOpen) {
        if (onMenuOpened != null) onMenuOpened.Invoke(isMenuOpen);
    }
}
