using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// TODO: This doesn't listen for recording deleted events. Not a problem right now, since this is the only interface to delete recordings atm
public class UIRecordingList : MonoBehaviour {
    private bool queueUpdateGUI;
    public LoopMachineEventChannel loopMachineEventChannel;
    public SettingsMenuEventChannel settingsMenuEventChannel;
    public List<UIRecording> uiRecordings;

    public TextMeshProUGUI recordingCountLabel;
    public TextMeshProUGUI pageCountLabel;

    [Range(0, 5)]
    public int currentPage = 0;
    [Range(1, 5)]
    public int maxPages = 3;

    public Sprite muteSprite, unmuteSprite;


    private void Awake() {
        for (int i = 0; i < uiRecordings.Count; i++) {
            uiRecordings[i].id = i;
        }
    }

    private void OnEnable() {
        loopMachineEventChannel.recordingSaved += OnRecordingSaved;
        settingsMenuEventChannel.onMenuOpened += OnSettingsMenuOpened;
    }
    private void OnDisable() {
        loopMachineEventChannel.recordingSaved -= OnRecordingSaved;
        settingsMenuEventChannel.onMenuOpened -= OnSettingsMenuOpened;
    }

    private void LateUpdate() {
        // This fixes a race condition with UITab
        if (queueUpdateGUI) {
            UpdateGUI();
            queueUpdateGUI = false;
        }
    }
    public void UpdateGUI() {
        recordingCountLabel.text = $"Recordings:{LoopMachine.Instance.activeLoopSection.recordings.Count}";
        pageCountLabel.text = $"Page:{currentPage + 1}/{maxPages}";

        for (int i = 0; i < uiRecordings.Count; i++) {
            int recordingIndex = uiRecordings.Count * currentPage + i;
            if (recordingIndex < LoopMachine.Instance.activeLoopSection.recordings.Count) {
                uiRecordings[i].EnableRow();
                uiRecordings[i].label.text = LoopMachine.Instance.activeLoopSection.recordings[recordingIndex].id;

                if (LoopMachine.Instance.activeLoopSection.recordings[recordingIndex].isMuted) {
                    uiRecordings[i].muteButton.SetSprite(unmuteSprite);
                } else {
                    uiRecordings[i].muteButton.SetSprite(muteSprite);
                }
            } else {
                uiRecordings[i].DisableRow();
            }
        }
    }



    private void OnSettingsMenuOpened(bool isOpened) {
        if (isOpened) {
            //UpdateGUI();
            queueUpdateGUI = true;
        } else {
            foreach (UIRecording uiRecording in uiRecordings) {
                uiRecording.DisableRow();
            }
        }
    }

    private void OnRecordingSaved(LoopRecording newRecording) {
        UpdateGUI();
    }
    public void NextPage() {
        currentPage = Mathf.Clamp(currentPage + 1, 0, maxPages);
        UpdateGUI();
    }
    public void PreviousPage() {
        currentPage = Mathf.Clamp(currentPage - 1, 0, maxPages - 1);
        UpdateGUI();
    }


    public void DeleteRecording(int id) {
        int recordingIndex = uiRecordings.Count * currentPage + id;
        if (recordingIndex < LoopMachine.Instance.activeLoopSection.recordings.Count) {
            LoopMachine.Instance.activeLoopSection.recordings[recordingIndex].Delete();
            UpdateGUI();
        } else {
            Debug.LogError($"Invalid ID - cannot delete this recording ({id})");
        }
    }


    public void ToggleMuteRecording(int id) {
        int recordingIndex = uiRecordings.Count * currentPage + id;
        if (recordingIndex < LoopMachine.Instance.activeLoopSection.recordings.Count) {
            LoopMachine.Instance.activeLoopSection.recordings[recordingIndex].isMuted = !LoopMachine.Instance.activeLoopSection.recordings[recordingIndex].isMuted;

            UpdateGUI();
        } else {
            Debug.LogError($"Invalid ID - cannot mute this recording ({id})");
        }
    }
}
