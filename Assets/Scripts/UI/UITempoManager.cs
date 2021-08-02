using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITempoManager : MonoBehaviour {
    public LoopMachineEventChannel loopMachineEventChannel;
    public TextMeshProUGUI bpmText;
    public TextMeshProUGUI timeSignatureText;

    private void Start() {
        UpdateGUI();
    }

    public void IncrementBPM() {
        loopMachineEventChannel.RaiseOnBPMIncrement();
        UpdateGUI();
    }
    public void DecrementBPM() {
        loopMachineEventChannel.RaiseOnBPMDecrement();
        UpdateGUI();
    }
    public void IncrementTimeSigNumerator() {
        loopMachineEventChannel.RaiseOnTimeSigNumeratorIncrement();
        UpdateGUI();
    }
    public void DecrementTimeSigNumerator() {
        loopMachineEventChannel.RaiseOnTimeSigNumeratorDecrement();
        UpdateGUI();
    }
    public void ToggleMetronome() {
        // This really should be a separate event, it kinda ruins the purpose of event channels
        loopMachineEventChannel.RaiseOnMetronomeSet(!LoopMachine.Instance.isMetronomeActive);
    }


    public void UpdateGUI() {
        bpmText.text = $"BPM:{LoopMachine.Instance.bpm}";
        timeSignatureText.text = $"{LoopMachine.Instance.beatsPerMeasure}/4";
    }
}
