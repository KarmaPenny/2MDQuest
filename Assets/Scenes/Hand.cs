using UnityEngine;

public class Hand : MonoBehaviour {
    public enum HandType { left, right, editor }
    public HandType hand;
    Pickupable ball;
    OVRInput.Controller controller;
    [HideInInspector] public Pickupable contents;

    void Start() {
        controller = (hand == HandType.left) ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        ball = GameObject.FindGameObjectWithTag("ball").GetComponent<Pickupable>();
    }

    void Update() {
        float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
        if (hand == HandType.editor) {
            trigger = (Input.GetKey(KeyCode.Space)) ? 1.0f : 0.0f;
        }
        if (trigger > 0.5f) {
            if (contents == null) {
                contents = ball;
                contents.PickUp(this);
            }
        } else if (contents != null) {
            if (hand == HandType.editor) {
                contents.Throw(new Vector3(0, 0.5f, 1) * 9, Vector3.zero);
            } else {
                contents.Throw(OVRInput.GetLocalControllerVelocity(controller), OVRInput.GetLocalControllerAngularVelocity(controller));
            }
            contents = null;
        }
    }
}