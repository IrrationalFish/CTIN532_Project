using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnobtainedPhone : MonoBehaviour {

    public Vector3 rotateSpeed;

    // Update is called once per frame
    void Update() {
        gameObject.transform.Rotate(rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Player>().ObtainPhone();
            Destroy(this.gameObject);
        }
    }
}
