using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecordingList : MonoBehaviour {
    public LoopMachineEventChannel loopMachineEventChannel;
    public GameObject recordingUIPrefab;
    public List<UIRecording> uiRecordings;

    [Tooltip("The vertical spacing between each row")]
    [Range(0f, 1f)]
    public float yOffset;

    private void OnEnable() {
        loopMachineEventChannel.recordingSaved += AddRecording;
    }
    private void OnDisable() {
        loopMachineEventChannel.recordingSaved -= AddRecording;
    }

    private void Update() {
        UpdateUIPosition();
    }

    private void AddRecording(LoopRecording newRecording) {
        Vector3 newPos = transform.position;
        newPos.y = -yOffset * uiRecordings.Count;

        GameObject newRecordingRowGameObject = Instantiate(recordingUIPrefab, transform);
        UIRecording uiRecording = newRecordingRowGameObject.GetComponent<UIRecording>();
        uiRecordings.Add(uiRecording);

        uiRecording.recording = newRecording;
        uiRecording.recordingsList = this;

        UpdateUIPosition();
    }

    public void UpdateUIPosition() {
        Vector3 newPos = Vector3.zero;
        for (int i = 0; i < uiRecordings.Count; i++) {
            newPos.y = -yOffset * i;

            foreach (UIButton button in uiRecordings[i].buttons) {
                button.DisableButton();
            }

            uiRecordings[i].transform.localPosition = newPos;

            foreach (UIButton button in uiRecordings[i].buttons) {
                button.EnableButton();
            }
        }
    }
}
