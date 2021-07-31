using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoopMachine))]
public class LoopMachineInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            LoopMachine loopMachine = target as LoopMachine;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play", EditorStyles.miniButtonLeft)) {
                loopMachine.Play();
            }
            if (GUILayout.Button("Pause", EditorStyles.miniButtonMid)) {
                loopMachine.Pause();
            }
            if (GUILayout.Button("Stop", EditorStyles.miniButtonMid)) {
                loopMachine.Stop();
            }
            if (GUILayout.Button("Queue Recording", EditorStyles.miniButtonMid)) {
                loopMachine.QueueRecording();
            }
            GUILayout.EndHorizontal();


            GUILayout.Label("Sections");
            EditorGUI.indentLevel++;
            foreach (LoopSection loopSection in loopMachine.loopSections) {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Set Active", EditorStyles.miniButtonLeft)) {
                    loopMachine.SetActiveLoopSection(loopSection);
                }
                if (GUILayout.Button("Delete", EditorStyles.miniButtonLeft)) {
                    loopSection.Delete();
                    break;
                }
                GUILayout.EndHorizontal();

            }
            EditorGUI.indentLevel--;

            if (GUILayout.Button("+", EditorStyles.miniButtonLeft)) {
                // Need to manually serialize our lists, since we're creating via the inspector
                LoopSection newSection = loopMachine.CreateNewSection();
                newSection.recordings = new List<LoopRecording>();
            }
        }
    }
}

