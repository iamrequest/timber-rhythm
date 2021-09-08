using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Player;

public class HandKickbackTest : MonoBehaviour {
    HVRJointHand jointHand;

    public Transform tip;
    public Transform target;
    [Range(0f, 1f)]
    public float maxForce;
    public ForceMode forceMode;
    public bool applyForceToHand;

    private void Awake() {
        jointHand = GetComponent<HVRJointHand>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 v = tip.position - transform.position;
        v = Vector3.Cross(v, target.up);

        if (Input.GetKeyDown(KeyCode.K)) {
            if (applyForceToHand) {
                jointHand.RigidBody.AddTorque(v.normalized * maxForce, forceMode);
            } else {
                jointHand.ParentRigidBody.AddTorque(v.normalized * maxForce, forceMode);
            }
        }
    }
}
