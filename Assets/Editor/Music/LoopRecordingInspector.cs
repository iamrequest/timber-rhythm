using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoopRecording))]
public class LoopRecordingInspector : Editor {
    float t;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            LoopRecording recording = target as LoopRecording;

            GUILayout.Label("Add Notes");
            t = EditorGUILayout.Slider(t, 0f, 1f);

            if (GUILayout.Button("+", EditorStyles.miniButtonLeft)) {
                ImpactNote newNote = recording.gameObject.AddComponent<ImpactNote>();
                newNote.playTime = t;

                recording.notes.Add(newNote);
            }
        }
    }
}

