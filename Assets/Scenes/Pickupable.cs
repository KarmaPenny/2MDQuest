using UnityEngine;

public class Pickupable : MonoBehaviour {
    Hand hand;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (hand != null) {
            transform.position = hand.transform.position;
            transform.rotation = hand.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
    }

    public void PickUp(Hand target) {
        Debug.Log(gameObject.name + " picked up by " + target.gameObject.name);
        if (hand != null) {
            hand.contents = null;
        }
        hand = target;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Throw(Vector3 velocity, Vector3 angularVelocity) {
        Debug.Log(gameObject.name + " thrown by " + hand.gameObject.name + " with velocity " + velocity);
        hand = null;
        rb.isKinematic = false;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }
}