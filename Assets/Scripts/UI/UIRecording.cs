using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRecording : MonoBehaviour {
    private List<UIButton> buttons;
    [HideInInspector]
    public TextMeshProUGUI label;

    [HideInInspector]
    public UIRecordingList recordingsList;
    [HideInInspector]
    public int id;

    private void Awake() {
        buttons = new List<UIButton>();
        GetComponentsInChildren(buttons);
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Delete() {
        recordingsList.DeleteRecording(id);
    }

    public void DisableRow() {
        foreach (UIButton button in buttons) {
            button.DisableButton();
        }
        label.enabled = false;
    }
    public void EnableRow() {
        foreach (UIButton button in buttons) {
            button.EnableButton();
        }
        label.enabled = true;
    }
}
