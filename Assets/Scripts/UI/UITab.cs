using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITab : MonoBehaviour {
    private List<UIButton> uiButtons;
    public SettingsMenuEventChannel settingsMenuEventChannel;

    private void Start() {
        uiButtons = new List<UIButton>();
        GetComponentsInChildren(uiButtons);
    }

    private void OnEnable() {
        settingsMenuEventChannel.onMenuOpened += ToggleButtons;
    }
    private void OnDisable() {
        settingsMenuEventChannel.onMenuOpened -= ToggleButtons;
    }

    public void ToggleButtons(bool isMenuOpen) {
        if (isMenuOpen) {
            EnableButtons();
        } else {
            DisableButtons();
        }
    }

    public void EnableButtons() {
        foreach (UIButton uiButton in uiButtons) {
            uiButton.EnableButton();
        }
    }

    public void DisableButtons() {
        foreach (UIButton uiButton in uiButtons) {
            uiButton.DisableButton();
        }
    }
}
