using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

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
        if(!OnGround()) {     //old version
            verSpeed = verSpeed - gravity * Time.deltaTime;
        } else {
            if(Input.GetKeyDown(KeyCode.Space)) {
                verSpeed = jumpForce;
                DataCollector.RecordOneJump();
            }
        }
        Vector3 movement = new Vector3(hor * moveSpeed, verSpeed, ver * moveSpeed);
        rb.velocity = transform.TransformDirection(movement);

    }

    bool OnGround() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red, 1.1f);
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.1f)){
            if(hit.collider.isTrigger) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }

    public void CheckParent() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.1f)) {
            if(hit.collider.gameObject.CompareTag("FloatingBoard") && this.gameObject.transform.parent==null) {
                //stand on the float board, link
                this.gameObject.transform.parent = hit.collider.gameObject.transform;
                print("player on floating board");
            }else if(!hit.collider.gameObject.CompareTag("FloatingBoard")) {
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
