using HurricaneVR.Framework.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HVRPhysicsButton))]
[RequireComponent(typeof(Animator))]
public class UIButton : MonoBehaviour {
    private HVRPhysicsButton button;
    private Animator animator;
    private int isPressedHash;

    private void Awake() {
        button = GetComponent<HVRPhysicsButton>();
        animator = GetComponent<Animator>();
        isPressedHash = Animator.StringToHash("isPressed");
    }
    private void OnEnable() {
        button.ButtonDown.AddListener(OnButtonStateChange);
        button.ButtonUp.AddListener(OnButtonStateChange);
    }

    private void OnDisable() {
        button.ButtonDown.AddListener(OnButtonStateChange);
        button.ButtonUp.AddListener(OnButtonStateChange);
    }

    private void OnButtonStateChange(HVRPhysicsButton arg0) {
        animator.SetBool(isPressedHash, button.IsPressed);
    }
}
