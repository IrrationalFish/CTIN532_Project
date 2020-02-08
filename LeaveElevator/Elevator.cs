using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

    public enum ElevatorType {
        Leave,
        Arrive
    }

    public ElevatorType type;
    public int destIndex;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(destIndex);
            print("Enter leave elevator");
        }
    }
}
