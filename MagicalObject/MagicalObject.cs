using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalObject : MonoBehaviour {

    public bool Triggered;
    public List<Mechanism> linkedMechanism = new List<Mechanism>();

    public ParticleSystem turnOnParticle;
    public AudioSource turnOnSound;
    public ParticleSystem pointerParticle;
    public particleAttractorMove pointerScript;

    public GameObject core;
    public Vector3 rotateSpeed;

    private void Start() {
        turnOnSound = GetComponent<AudioSource>();
        if(linkedMechanism.Count != 0) {
            pointerScript.target = linkedMechanism[0].laserTarget;
        }
    }

    private void Update() {
        core.transform.Rotate(rotateSpeed * Time.deltaTime);
    }

    public void InsideView() {
        if(Triggered == false) {
            Triggered = true;
            Invoke("PlayTurnOnParticle", 0.5f);
            //print(this.gameObject.name + " turns on");
            foreach(Mechanism m in linkedMechanism) {
                m.TurnOn();
            }
            pointerParticle.Play();
        }
    }

    public void OutsideView() {
        if(Triggered == true) {
            Triggered = false;
            Invoke("StopTurnOnParticle", 0.5f);
            //print(this.gameObject.name + " turns off");
            foreach(Mechanism m in linkedMechanism) {
                m.TurnOff();
            }
            pointerParticle.Stop();
        }
    }

    private void OnDrawGizmos() {
        foreach(Mechanism m in linkedMechanism) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, m.transform.position);
        }
    }

    private void PlayTurnOnParticle() {
        turnOnSound.loop = true;
        turnOnSound.Play();
        turnOnParticle.Play();
    }

    private void StopTurnOnParticle() {
        turnOnSound.loop = false;
        turnOnParticle.Stop();
    }

}
