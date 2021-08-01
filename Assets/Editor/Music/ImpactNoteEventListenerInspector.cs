using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImpactNoteEventListener))]
public class ImpactNoteEventListenerInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            ImpactNoteEventListener impactNoteEventListener = target as ImpactNoteEventListener;

            if (GUILayout.Button("Invoke", EditorStyles.miniButtonLeft)) {
                impactNoteEventListener.RaiseEvent(null);
            }
        }
    }
}

