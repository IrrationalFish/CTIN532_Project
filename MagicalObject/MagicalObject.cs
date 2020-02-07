using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalObject : MonoBehaviour {

    public bool Triggered;

    public void TurnOn() {
        if(Triggered == false) {
            Triggered = true;
            print(this.gameObject.name + " turns on");
        }
    }

    public void TurnOff() {
        if(Triggered == true) {
            Triggered = false;
            print(this.gameObject.name + " turns off");
        }
    }

}
