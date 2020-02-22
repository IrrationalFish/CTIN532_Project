using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBoard : Mechanism {

    public GameObject boardModel;
    public float maxSpeed;
    public bool on = false;
    public Transform onPos;
    public Transform offPos;
    public bool transportingPlayer;

    public GameObject[] lightObjects;
    public Light[] lights;

    private Transform destination;

    private void Start() {
        if(on == true) {
            TurnOn();
        } else {
            TurnOff();
        }
    }

    private void Update() {
        if(destination != null && !boardModel.transform.position.Equals(destination.position)) {
            boardModel.transform.position = Vector3.MoveTowards(boardModel.transform.position, destination.position, maxSpeed);
        }
    }
    public override void TurnOn() {
        foreach(GameObject o in lightObjects) {
            o.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
        }
        foreach(Light l in lights) {
            l.enabled = true;
        }
        destination = onPos;
        on = true;
    }
    public override void TurnOff() {
        foreach(GameObject o in lightObjects) {
            o.GetComponent<Renderer>().materials[1].DisableKeyword("_EMISSION");
        }
        foreach(Light l in lights) {
            l.enabled = false;
        }
        destination = offPos;
        on = false;
    }
}