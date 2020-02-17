using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Mechanism {

    public GameObject doorBoard;
    public float maxSpeed;
    public Transform onPos;
    public Transform offPos;
    [SerializeField] private bool on;

    private Transform destination;

    private void Start() {
        if(on == true) {
            destination = onPos;
        } else {
            destination = offPos;
        }
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
