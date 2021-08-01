using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HVRGrabbable))]
public class DrumStick : MonoBehaviour {
    private HVRGrabbable grabbable;
    public Transform tip;
    public Vector3 v;

    [Range(0f, 1000f)]
    public float maxForce;
    public ForceMode forceMode;
    public bool applyForceToHand;

    private void Awake() {
        grabbable = GetComponent<HVRGrabbable>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (grabbable.IsBeingHeld) {
            v = tip.position - grabbable.HandGrabbers[0].transform.position;

            if (applyForceToHand) {
                grabbable.HandGrabbers[0].Rigidbody.AddTorque(v.normalized * maxForce, forceMode);
            } else {
                grabbable.Rigidbody.AddTorque(v.normalized * maxForce, forceMode);
            }
        } else {
            Debug.Log("Not grabbed");
        }
    }
}
