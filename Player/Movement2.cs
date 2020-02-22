using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour {
    public float moveSpeed;
    public float gravity;
    public float jumpForce;

    private Rigidbody rb;
    private CharacterController cc;
    private Vector3 moveDir = Vector3.zero;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    void Update() {
        UpdateMovement();
        CheckParent();
    }

    void UpdateMovement() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if(cc.isGrounded) {
            moveDir = new Vector3(hor * moveSpeed, 0, ver * moveSpeed );
            moveDir = transform.TransformDirection(moveDir);
            if(Input.GetKeyDown(KeyCode.Space)) {
                moveDir.y = jumpForce;
            }
        }
        moveDir.y = moveDir.y - gravity * Time.deltaTime;
        Vector3 parentVelocity = Vector3.zero;
        if(gameObject.transform.parent != null) {
            GameObject parent = gameObject.transform.parent.gameObject;
            if(parent.GetComponent<Rigidbody>() != null) {
                parentVelocity = parent.GetComponent<Rigidbody>().velocity;
                print("Parent V: " + parentVelocity);
            }
        }
        cc.Move(moveDir * Time.deltaTime + parentVelocity);
    }

    bool OnGround() {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red, 1.1f);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.1f)) {
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
