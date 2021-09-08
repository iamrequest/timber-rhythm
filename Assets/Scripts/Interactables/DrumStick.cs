using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;


[RequireComponent(typeof(HVRGrabbable))]
public class DrumStick : MonoBehaviour {
    private HVRGrabbable grabbable;
    public Transform tip;

    [Range(0f, 10)]
    public float maxForce;
    public ForceMode forceMode;

    private void Awake() {
        grabbable = GetComponent<HVRGrabbable>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (grabbable.IsBeingHeld) {
            Vector3 v = tip.position - grabbable.HandGrabbers[0].transform.position;
            v = Vector3.Cross(v, collision.contacts[0].normal);
            float force = Mathfs.LerpClamped(0f, maxForce, collision.relativeVelocity.magnitude);

            grabbable.HandGrabbers[0].Rigidbody.AddTorque(v.normalized * force, forceMode);
        } else {
            Debug.Log("Not grabbed");
        }
    }
}
