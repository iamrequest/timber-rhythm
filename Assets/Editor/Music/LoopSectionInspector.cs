using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoopSection))]
public class LoopSectionInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            LoopSection loopSection = target as LoopSection;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Active", EditorStyles.miniButtonLeft)) {
                LoopMachine.Instance.SetActiveLoopSection(loopSection);
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("Recordings");
            EditorGUI.indentLevel++;
            if (loopSection.recordings == null) {
                Debug.LogError("Recordings list is null");
            }

            for (int i = 0; i < loopSection.recordings.Count; i++) {
                GUILayout.BeginHorizontal();

                // Delete
                if (GUILayout.Button("X", EditorStyles.miniButtonLeft)) {
                    loopSection.recordings[i].Delete();
                    break;
                }

                // Mute/unmute
                string muteLabel = loopSection.recordings[i].isMuted ? "Unmute" : "Mute";
                if (GUILayout.Button(muteLabel, EditorStyles.miniButtonLeft)) {
                    loopSection.recordings[i].isMuted = !loopSection.recordings[i].isMuted;
                }

                GUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;

            if (GUILayout.Button("+", EditorStyles.miniButtonLeft)) {
                LoopRecording loopRecording = loopSection.gameObject.AddComponent<LoopRecording>();
                loopRecording.parentLoopSection = loopSection;

                loopSection.recordings.Add(loopRecording);
            }
        }
    }
}

