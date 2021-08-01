using HurricaneVR.Framework.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(HVRPhysicsButton))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class UIButton : MonoBehaviour {
    private HVRPhysicsButton button;
    private Collider m_collider;
    private Animator animator;
    private int isPressedHash;

    private List<TextMeshProUGUI> texts;
    private List<Image> images;
    public List<Image> icons;

    private void Awake() {
        button = GetComponent<HVRPhysicsButton>();
        animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();
        isPressedHash = Animator.StringToHash("isPressed");
    }
    private void OnEnable() {
        button.ButtonDown.AddListener(OnButtonStateChange);
        button.ButtonUp.AddListener(OnButtonStateChange);
    }

    private void OnDisable() {
        button.ButtonDown.AddListener(OnButtonStateChange);
        button.ButtonUp.AddListener(OnButtonStateChange);
        button.Rigidbody.isKinematic = true;
        button.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Start() {
        texts = new List<TextMeshProUGUI>();
        images = new List<Image>();
        GetComponentsInChildren(texts);
        GetComponentsInChildren(images);
    }

    private void OnButtonStateChange(HVRPhysicsButton arg0) {
        animator.SetBool(isPressedHash, button.IsPressed);
        button.transform.localPosition = button.StartPosition;
    }

    /// <summary>
    /// This is necessary to prevent a weird physics/animator bug when enabling/disabling a physics button. Something to do with joints probably
    /// </summary>
    public void DisableButton() {
        m_collider.enabled = false;
        button.Rigidbody.isKinematic = true;

        if (texts != null) {
            foreach (TextMeshProUGUI text in texts) {
                text.enabled = false;
            }
        }
        if (images != null) {
            foreach (Image image in images) {
                image.enabled = false;
            }
        }
    }

    /// <summary>
    /// This is necessary to prevent a weird physics/animator bug when enabling/disabling a physics button. Something to do with joints probably
    /// </summary>
    public void EnableButton() {
        m_collider.enabled = true;
        button.Rigidbody.isKinematic = false;

        if (texts != null) {
            foreach (TextMeshProUGUI text in texts) {
                text.enabled = true;
            }
        }
        if (images != null) {
            foreach (Image image in images) {
                image.enabled = true;
            }
        }
    }

    public void SetSprite(Sprite sprite) {
        if (icons.Count == 0) {
            Debug.LogWarning("Attempted to set the sprite of this icon, but no icons are set.", this);
        }
        foreach (Image i in icons) {
            i.sprite = sprite;
        }
    }
}
