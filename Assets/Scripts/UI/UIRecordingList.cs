using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRecordingList : MonoBehaviour {
    public LoopMachineEventChannel loopMachineEventChannel;
    public List<UIRecording> uiRecordings;

    [Range(0, 5)]
    public int currentPage = 0;
    [Range(1, 5)]
    public int maxPages = 3;

    public TextMeshProUGUI recordingCountLabel;
    public TextMeshProUGUI pageCountLabel;

    private void Awake() {
        for (int i = 0; i < uiRecordings.Count; i++) {
            uiRecordings[i].id = i;
        }
    }

    private void OnEnable() {
        loopMachineEventChannel.recordingSaved += AddRecording;
    }
    private void OnDisable() {
        loopMachineEventChannel.recordingSaved -= AddRecording;
    }

    private void Update() {
        UpdateGUI();
    }

    private void AddRecording(LoopRecording newRecording) {
        UpdateGUI();
    }

    public void DeleteRecording(int id) {
    }

    public void UpdateGUI() {
        recordingCountLabel.text = $"Recordings:{LoopMachine.Instance.activeLoopSection.recordings.Count}";
        pageCountLabel.text = $"Page:{currentPage + 1}/{maxPages}";

        for (int i = 0; i < uiRecordings.Count; i++) {
            int recordingIndex = uiRecordings.Count * currentPage + i;
            if (LoopMachine.Instance.activeLoopSection.recordings.Count < recordingIndex) {
                uiRecordings[i].EnableRow();
                uiRecordings[i].label.text = LoopMachine.Instance.activeLoopSection.recordings[recordingIndex].id;
            } else {
                uiRecordings[i].DisableRow();
            }
        }
    }
}
