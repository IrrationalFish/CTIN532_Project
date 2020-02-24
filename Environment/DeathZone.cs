using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public bool isPlayer2;

    private void Start() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            print("Enter water");
            if(isPlayer2) {
                GameManager.player.gameObject.GetComponent<Movement2>().enabled = false;
                GameManager.player.gameObject.GetComponent<FirstPersonCam>().enabled = false;
                GameManager.player.gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 10));
                GameManager.player.gameObject.GetComponent<CharacterController>().enabled = false;
                GameManager.player.gameObject.GetComponent<Rigidbody>().useGravity = true;
                GameManager.player.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                GameManager.player.gameObject.GetComponent<FirstPersonCam>().enabled = false;
            } else {
                GameManager.player.GetComponent<Rigidbody>().freezeRotation = false;
                GameManager.player.GetComponent<FirstPersonCam>().enabled = false;
                GameManager.player.GetComponent<Movement>().enabled = false;
                GameManager.player.GetComponent<Rigidbody>().useGravity = true;
                GameManager.player.GetComponent<Transform>().Rotate(new Vector3(0, 0, 10));
            }
            Invoke("Respawn", 3f);
        }
    }

    private void Respawn() {
        GameObject.Find("GameManager").GetComponent<GameManager>().RespawnPlayer();
    }
}
