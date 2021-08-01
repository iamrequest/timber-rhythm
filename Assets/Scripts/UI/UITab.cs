using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITab : MonoBehaviour {
    public SettingsMenuEventChannel settingsMenuEventChannel;

    private List<UIButton> uiButtons;
    private List<TextMeshProUGUI> miscTexts;
    public List<GameObject> disableWithTab; // For manually-added stuff

    private void Start() {
        uiButtons = new List<UIButton>();
        GetComponentsInChildren(uiButtons);

        miscTexts = new List<TextMeshProUGUI>();
        foreach(Transform t in transform) {
            if (t.TryGetComponent(out TextMeshProUGUI text)) {
                miscTexts.Add(text);
            }
        }
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

        foreach (TextMeshProUGUI text in miscTexts) {
            text.enabled = true;
        }

        foreach (GameObject go in disableWithTab) {
            go.SetActive(true);
        }
    }

    public void DisableButtons() {
        foreach (UIButton uiButton in uiButtons) {
            uiButton.DisableButton();
        }

        foreach (TextMeshProUGUI text in miscTexts) {
            text.enabled = false;
        }

        foreach (GameObject go in disableWithTab) {
            go.SetActive(false);
        }
    }
}
