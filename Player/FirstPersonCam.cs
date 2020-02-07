using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour {

    public Camera eye;
    public float horSensitivity;
    public float verSensitivity;
    public float maxVert;
    public float minVert;

    private float _rotationX = 0;

    void Update() {
        float _rotationY;
        _rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * horSensitivity;

        _rotationX = _rotationX - Input.GetAxis("Mouse Y") * verSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

        transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        eye.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }
}
