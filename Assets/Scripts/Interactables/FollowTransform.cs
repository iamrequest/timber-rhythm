using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cheap hack to semi-parent a noise source to a drum face. Can't have multiple child rigidbodies, so this will have to do
/// </summary>
public class FollowTransform : MonoBehaviour {
    public Transform target;
    private Rigidbody rb;
    public bool moveTargetHereOnAwake;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        if (moveTargetHereOnAwake) {
            target.transform.position = transform.position;
            target.transform.rotation = transform.rotation;
        }
    }

    private void FixedUpdate() {
        if (rb == null) {
            transform.position = target.position;
            transform.rotation = target.rotation;
        } else {
            rb.position = target.position;
            rb.rotation = target.rotation;
        }
    }
}
