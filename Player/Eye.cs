using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

    public Transform eyePos;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void LateUpdate() {
        transform.position = eyePos.position;
        //transform.rotation = eyePos.rotation;
    }
}
