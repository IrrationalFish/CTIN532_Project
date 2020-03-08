using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteCamera : MonoBehaviour {

    public float liftHeight;
    public float maxSpeed;
    public float rotateSpeed;
    public GameObject[] rotors;

    private Vector3 destPos;

    // Start is called before the first frame update
    void Start() {
        destPos = transform.position;
        transform.Translate(new Vector3(0, liftHeight, 0));
    }

    // Update is called once per frame
    void Update() {
        if(destPos != null && !transform.position.Equals(destPos)) {
            transform.position = Vector3.MoveTowards(transform.position, destPos, maxSpeed);
        }
        foreach(GameObject rotor in rotors) {
            rotor.transform.Rotate(new Vector3(0, 0, rotateSpeed), Space.Self);
        }
    }
}
