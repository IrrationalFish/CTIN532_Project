using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDUITrigger : MonoBehaviour {

    private Animator ac;

    void Start() {
        ac = GetComponent<Animator>();
    }

    void Update() {
        transform.LookAt(GameManager.player.transform);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            if(ac.enabled) {
                ac.SetBool("ShowUI", true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            if(ac.enabled) {
                ac.SetBool("ShowUI", false);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 0, 0, 0.2f);
        BoxCollider bc = GetComponent<BoxCollider>();
        if(bc.enabled) {
            Gizmos.DrawCube(transform.position + bc.center, bc.size);
        }
    }
}
