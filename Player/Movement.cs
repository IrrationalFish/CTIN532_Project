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
        if(!OnGround()) {
            verSpeed = verSpeed - gravity * Time.deltaTime;
        } else {
            if(Input.GetKeyDown(KeyCode.Space)) {
                verSpeed = jumpForce;
            }
        }
        Vector3 movement = new Vector3(hor * Time.deltaTime * moveSpeed, verSpeed, ver * Time.deltaTime * moveSpeed);
        rb.velocity = transform.TransformDirection(movement);

    }

    bool OnGround() {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red, 1.1f);
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1.1f)){
            return true;
        } else {
            return false;
        }
    }

    public void CheckParent() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.1f)) {
            if(hit.collider.gameObject.CompareTag("FloatingBoard") && GameManager.player.transform.parent==null) {
                this.gameObject.transform.parent = hit.collider.gameObject.transform;
                //hit.collider.gameObject.transform.parent.gameObject.GetComponent<FloatingBoard>().AttachPlayer();
                print("player on floating board");
            }else if(!hit.collider.gameObject.CompareTag("FloatingBoard")) {
                //this.gameObject.transform.parent = null;
            }
        } else {    //not stand on sth
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
