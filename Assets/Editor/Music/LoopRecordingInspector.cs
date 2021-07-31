using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoopRecording))]
public class LoopRecordingInspector : Editor {
    private float startT = 0.25f, endT = 0.75f;
    private float velocity = 0.75f;
    private ImpactNoteSource impactNoteSource;
    private SustainedNoteSource sustainedNoteSource;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            LoopRecording recording = target as LoopRecording;

            EditorGUILayout.Space();
            GUILayout.Label("Add Notes");

            EditorGUILayout.MinMaxSlider("t", ref startT, ref endT, 0f, 1f);
            velocity = EditorGUILayout.Slider("velocity", velocity, 0f, 1f);

            GUILayout.BeginHorizontal();
            impactNoteSource = EditorGUILayout.ObjectField("Impact Note Source", impactNoteSource, typeof(ImpactNoteSource), true) as ImpactNoteSource;

            if (GUILayout.Button("Add to recording", EditorStyles.miniButtonLeft)) {
                recording.RecordNote(impactNoteSource.note, startT, endT);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            sustainedNoteSource = EditorGUILayout.ObjectField("Sustained Note Source", sustainedNoteSource, typeof(SustainedNoteSource), true) as SustainedNoteSource;

            if (GUILayout.Button("Add to recording", EditorStyles.miniButtonRight)) {
                recording.RecordNote(sustainedNoteSource.note, startT, endT);
            }
            GUILayout.EndHorizontal();
        }
    }
}

