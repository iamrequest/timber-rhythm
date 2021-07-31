using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SustainedNote))]
public class SustainedNoteInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            SustainedNote sustainedNote = target as SustainedNote;

            EditorGUILayout.Space();
            GUILayout.Label("Manage Note");

            if (GUILayout.Button("Start Note", EditorStyles.miniButtonLeft)) {
                sustainedNote.StartNote();
            } 

            if (GUILayout.Button("Stop Note", EditorStyles.miniButtonLeft)) {
                sustainedNote.StopNote();
            } 
        }
    }
}

