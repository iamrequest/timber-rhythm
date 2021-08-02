using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioSourceObjectPool))]
public class AudioSourceObjectPoolInspector : Editor {
    GameObject go;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            ObjectPool op = target as ObjectPool;

            if (go == null) {
                if (GUILayout.Button("Fetch", EditorStyles.miniButtonLeft)) {
                    go = op.GetFromPool();
                }
            } else {
                if (GUILayout.Button("Return", EditorStyles.miniButtonLeft)) {
                    op.ReturnToPool(go);
                    go = null;
                } 
            }
        }
    }
}

