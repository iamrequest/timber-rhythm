﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoopRecording))]
public class LoopRecordingInspector : Editor {
    private float t;
    private float velocity;
    private NoteSource noteSource;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            LoopRecording recording = target as LoopRecording;

            EditorGUILayout.Space();
            GUILayout.Label("Add Notes");

            t = EditorGUILayout.Slider("t", t, 0f, 1f);
            velocity = EditorGUILayout.Slider("velocity", velocity, 0f, 1f);
            noteSource = EditorGUILayout.ObjectField("Note Source", noteSource, typeof(NoteSource), true) as NoteSource;

            if (GUILayout.Button("Add to Recording", EditorStyles.miniButtonLeft)) {
                ImpactNote newNote = recording.gameObject.AddComponent<ImpactNote>();
                newNote.playTime = t;
                newNote.noteSource = noteSource;
                newNote.velocity = velocity;

                recording.notes.Add(newNote);
            }
        }
    }
}

