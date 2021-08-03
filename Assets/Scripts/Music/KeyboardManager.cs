using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This component switches out the active keyboard, by simply activating/deactivating gameobjects.
///     This could be more efficient by actually switching out the sound libraries on each SustainedNoteSource, 
///     but the performance benefit isn't really needed right now.
/// </summary>
public class KeyboardManager : MonoBehaviour {
    public TextMeshProUGUI keyboardNameLabel;
    public List<Keyboard> keyboards;

    [Tooltip("This is also the initial keyboard")]
    public int currentKeyboard;

    private void Start() {
        for (int i = 0; i < keyboards.Count; i++) {
            keyboards[i].gameObject.SetActive(false);
        }
        keyboards[currentKeyboard].gameObject.SetActive(true);
        UpdateGUI();
    }

    public void NextKeyboard() {
        keyboards[currentKeyboard].gameObject.SetActive(false);
        currentKeyboard = (currentKeyboard + 1) % keyboards.Count;
        keyboards[currentKeyboard].gameObject.SetActive(true);
        UpdateGUI();
    }
    public void PreviousKeyboard() {
        keyboards[currentKeyboard].gameObject.SetActive(false);
        currentKeyboard = (currentKeyboard + keyboards.Count - 1) % keyboards.Count;
        keyboards[currentKeyboard].gameObject.SetActive(true);
        UpdateGUI();
    }


    public void UpdateGUI() {
        keyboardNameLabel.text = keyboards[currentKeyboard].keyboardName;
    }
}
