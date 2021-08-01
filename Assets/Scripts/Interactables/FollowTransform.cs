using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cheap hack to semi-parent a noise source to a drum face. Can't have multiple child rigidbodies, so this will have to do
/// </summary>
public class FollowTransform : MonoBehaviour {
    public Transform target;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rb.position = target.position;
        rb.rotation = target.rotation;
    }
}
