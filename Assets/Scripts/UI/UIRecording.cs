using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecording : MonoBehaviour {
    [HideInInspector]
    public UIRecordingList recordingsList;
    public LoopRecording recording;

    [HideInInspector]
    public List<UIButton> buttons;

    private void Awake() {
        buttons = new List<UIButton>();
        GetComponentsInChildren(buttons);
    }

    public void Delete() {
        recording.Delete();

        recordingsList.uiRecordings.Remove(this);
        recordingsList.UpdateUIPosition();

        Destroy(gameObject);
    }
}
