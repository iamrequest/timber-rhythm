using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDockingManager : MonoBehaviour {
    public Camera cam;
    private Animator animator;
    private int isSettingsMenuOpenHash;
    private float viewAngle;

    public Transform dockingTransform;

    [Range(0f, 1f)]
    public float lerpSpeed = 0.4f;

    // This should probably be done via dot product instead
    [Range(0f, 180f)]
    public float settingsOpenAngle = 30f, settingsCloseAngle = 45f;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        isSettingsMenuOpenHash = Animator.StringToHash("isSettingsMenuOpen");
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, dockingTransform.position, lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, dockingTransform.rotation, lerpSpeed);

        viewAngle = Vector3.Angle(cam.transform.forward, -transform.forward);
        if (animator.GetBool(isSettingsMenuOpenHash)) {
            if (viewAngle > settingsCloseAngle) {
                animator.SetBool(isSettingsMenuOpenHash, false);
            }
        } else {
            if (viewAngle < settingsOpenAngle) {
                animator.SetBool(isSettingsMenuOpenHash, true);
            }
        }
    }
}
