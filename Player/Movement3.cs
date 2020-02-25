using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3 : MonoBehaviour {
    public float moveSpeed;
    public float gravity;
    public float jumpForce;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        UpdateMovement();
        CheckParent();
    }

    void UpdateMovement() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        float verSpeed = rb.velocity.y;
        Vector3 movement;
        if(!OnGround()) {
            verSpeed = verSpeed - gravity * Time.deltaTime;
            movement = new Vector3(rb.velocity.x, verSpeed, rb.velocity.z);
        } else {
            if(Input.GetKeyDown(KeyCode.Space)) {
                verSpeed = jumpForce;
            }
            movement = new Vector3(hor * moveSpeed, verSpeed, ver * moveSpeed);
            movement = transform.TransformDirection(movement);
        }
        rb.velocity = movement;

    }

    bool OnGround() {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red, 1.1f);
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1.1f)) {
            return true;
        } else {
            return false;
        }
    }

    public void CheckParent() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.1f)) {
            if(hit.collider.gameObject.CompareTag("FloatingBoard") && this.gameObject.transform.parent == null) {
                //stand on the float board, link
                this.gameObject.transform.parent = hit.collider.gameObject.transform;
                print("player on floating board");
            } else if(!hit.collider.gameObject.CompareTag("FloatingBoard")) {
                //not stand on the float board
                if(gameObject.transform.parent != null && !gameObject.transform.parent.CompareTag("Elevator")) {
                    //not transporting by elevator
                    this.gameObject.transform.parent = null;
                }
            }
        } else {    //in sky
            Transform parent = this.gameObject.transform.parent;
            if(parent == null) {
                return;
            }
            if(!parent.CompareTag("Elevator")) {
                this.gameObject.transform.parent = null;
                print("player leaves floating board");
            }
        }
    }
}
