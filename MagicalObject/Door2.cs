using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : Mechanism {

    public bool openAtStart = false;
    public Light doorLight;
    public GameObject lightObject;
    public float soundDelay = 0.2f;

    private AudioSource doorMoveSound;
    private Animator anim;

    private void Start() {
        doorMoveSound = this.gameObject.GetComponent<AudioSource>();
        anim = this.gameObject.GetComponent<Animator>();
        if(openAtStart) {
            TurnOn();
        } else {
            TurnOff();
        }
    }

    public override void TurnOn() {
        if(doorMoveSound == null || anim == null) {
            doorMoveSound = this.gameObject.GetComponent<AudioSource>();
            anim = this.gameObject.GetComponent<Animator>();
        }
        doorMoveSound.PlayDelayed(soundDelay);
        lightObject.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
        //doorLight.enabled = true;
        anim.SetBool("open", true);
    }

    public override void TurnOff() {
        if(doorMoveSound == null || anim == null) {
            doorMoveSound = this.gameObject.GetComponent<AudioSource>();
            anim = this.gameObject.GetComponent<Animator>();
        }
        doorMoveSound.PlayDelayed(soundDelay);
        lightObject.GetComponent<Renderer>().materials[1].DisableKeyword("_EMISSION");
        //doorLight.enabled = false;
        anim.SetBool("open", false);
    }

}
