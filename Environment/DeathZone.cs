using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void Start() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            print("Enter water");
            GameManager.player.GetComponent<Movement2>().enabled = false;
            GameManager.player.GetComponent<FirstPersonCam>().enabled = false;
            GameManager.player.GetComponent<Transform>().Rotate(new Vector3(0, 0, 10));
            GameManager.player.GetComponent<CharacterController>().enabled = false;
            GameManager.player.GetComponent<Rigidbody>().useGravity = true;
            GameManager.player.GetComponent<CapsuleCollider>().enabled = true;
            GameManager.player.GetComponent<FirstPersonCam>().enabled = false;
            /*GameManager.player.GetComponent<Rigidbody>().freezeRotation = false;
            GameManager.player.GetComponent<FirstPersonCam>().enabled = false;
            GameManager.player.GetComponent<Movement>().enabled = false;
            GameManager.player.GetComponent<Rigidbody>().useGravity = true;
            GameManager.player.GetComponent<Transform>().Rotate(new Vector3(0, 0, 10));*/
            Invoke("Respawn", 3f);
        }
    }

    private void Respawn() {
        GameObject.Find("GameManager").GetComponent<GameManager>().RespawnPlayer();
    }
}
