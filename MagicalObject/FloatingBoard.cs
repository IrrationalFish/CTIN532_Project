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


    private Transform destination;

    private void Start() {
        if(on == true) {
            destination = onPos;
        } else {
            destination = offPos;
        }
    }

    private void Update() {
        if(destination != null && !boardModel.transform.position.Equals(destination.position)) {
            boardModel.transform.position = Vector3.MoveTowards(boardModel.transform.position, destination.position, maxSpeed);
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
