using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        UpdateMovement();
    }

    void UpdateMovement() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hor * Time.deltaTime * moveSpeed, 0, ver * Time.deltaTime * moveSpeed);
        rb.velocity = transform.TransformDirection(movement);
    }
}
