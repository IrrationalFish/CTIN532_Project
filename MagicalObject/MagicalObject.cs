using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalObject : MonoBehaviour {

    public bool Triggered;
    public List<Mechanism> linkedMechanism = new List<Mechanism>();
    public void InsideView() {
        if(Triggered == false) {
            Triggered = true;
            print(this.gameObject.name + " turns on");
            foreach(Mechanism m in linkedMechanism) {
                m.TurnOn();
            }
        }
    }

    public void OutsideView() {
        if(Triggered == true) {
            Triggered = false;
            print(this.gameObject.name + " turns off");
            foreach(Mechanism m in linkedMechanism) {
                m.TurnOff();
            }
        }
    }

}
