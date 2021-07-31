using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OctaveNoteCollection))]
public class OctaveNoteCollectionInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        OctaveNoteCollection octaveNoteCollection = target as OctaveNoteCollection;

        EditorGUILayout.Space();
        GUILayout.Label("Manage Notes");

        serializedObject.Update();

        SerializedProperty notes = serializedObject.FindProperty("notes");
        if (notes.arraySize != 12) {
            GUILayout.Label("There must be exactly 12 notes in this octave");
            return;
        }

        for (int i = 0; i < 12; i++) {
            string noteName = System.Enum.GetName(typeof(ToneNote), i);
            EditorGUILayout.PropertyField(notes.GetArrayElementAtIndex(i), new GUIContent(noteName));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

