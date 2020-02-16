using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBoardModel : MonoBehaviour {

    public FloatingBoard parent;

    private void OnCollisionEnter(Collision collision) {
        /*if(collision.gameObject.CompareTag("Player")) {
            print("Board catches player");
            parent.transportingPlayer = true;
            GameManager.player.transform.SetParent(gameObject.transform);
        }*/
    }

    private void OnCollisionExit(Collision collision) {
        /*if(collision.gameObject.CompareTag("Player")) {
            print("Board loses player");
            parent.transportingPlayer = false;
            GameManager.player.transform.SetParent(null);
        }*/
    }
}
