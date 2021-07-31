using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LoopProgressListener : MonoBehaviour {
    private Slider loopSlider;
    private Image fillImage;
    public Color playColor, recordingColor;

    private void Awake() {
        loopSlider = GetComponent<Slider>();
        fillImage = loopSlider.fillRect.GetComponent<Image>();
    }

    private void Update() {
        loopSlider.value = LoopMachine.Instance.t;

        if (LoopMachine.Instance.isRecording) {
            fillImage.color = recordingColor;
        } else {
            fillImage.color = playColor;
        }
    }
}
