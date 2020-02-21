using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanism : MonoBehaviour {

    public Transform laserTarget;

    public abstract void TurnOn();
    public abstract void TurnOff();

}
