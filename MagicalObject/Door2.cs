using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : Mechanism {

    public bool openAtStart = false;
    public Light doorLight;
    public GameObject lightObject;

    private Animator anim;

    private void Start() {
        anim = this.gameObject.GetComponent<Animator>();
        if(openAtStart) {
            TurnOn();
        } else {
            TurnOff();
        }
    }

    public override void TurnOff() {
        lightObject.GetComponent<Renderer>().materials[1].DisableKeyword("_EMISSION");
        //doorLight.enabled = false;
        anim.SetBool("open", false);
    }

    public override void TurnOn() {
        lightObject.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
        //doorLight.enabled = true;
        anim.SetBool("open", true);
    }

}
