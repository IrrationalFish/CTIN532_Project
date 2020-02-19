using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : Mechanism {

    private Animator anim;

    private void Start() {
        anim = this.gameObject.GetComponent<Animator>();
    }

    public override void TurnOff() {
        anim.SetBool("character_nearby", false);
    }

    public override void TurnOn() {
        anim.SetBool("character_nearby", true);
    }

}
