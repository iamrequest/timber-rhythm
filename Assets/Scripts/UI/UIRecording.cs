using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRecording : MonoBehaviour {
    private UIRecordingList recordingsList;
    [HideInInspector]
    public TextMeshProUGUI label;

    [HideInInspector]
    public int id;
    public UIButton deleteButton, muteButton;

    private void Awake() {
        label = GetComponentInChildren<TextMeshProUGUI>();
        recordingsList = GetComponentInParent<UIRecordingList>();
    }

    public void Delete() {
        recordingsList.DeleteRecording(id);
    }

    public void DisableRow() {
        deleteButton.DisableButton();
        muteButton.DisableButton();
        label.enabled = false;
    }
    public void EnableRow() {
        deleteButton.EnableButton();
        muteButton.EnableButton();
        label.enabled = true;
    }
    public void ToggleMuteRecording() {
        recordingsList.ToggleMuteRecording(id);
    }
}
