using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Mechanism {

    public GameObject doorBoard;
    public float maxSpeed;
    public bool on;
    public Transform onPos;
    public Transform offPos;

    private Transform destination;

    private void Start() {

    }

    private void Update() {
        if(destination!=null && !doorBoard.transform.position.Equals(destination.position)) {
            doorBoard.transform.position = Vector3.MoveTowards(doorBoard.transform.position, destination.position, maxSpeed);
        }
    }
    public override void TurnOn() {
        destination = onPos;
        on = true;
    }
    public override void TurnOff() {
        destination = offPos;
        on = false;
    }
}
