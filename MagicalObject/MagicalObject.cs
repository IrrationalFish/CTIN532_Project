using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalObject : MonoBehaviour {

    public bool Triggered;
    public List<Mechanism> linkedMechanism = new List<Mechanism>();

    public ParticleSystem turnOnParticle;
    public ParticleSystem pointerParticle;
    public Transform pointerTarget;
    public particleAttractorMove pointerScript;

    public GameObject core;
    public Vector3 rotateSpeed;

    private void Start() {
        pointerScript.target = pointerTarget;
    }

    private void Update() {
        core.transform.Rotate(rotateSpeed * Time.deltaTime);
    }

    public void InsideView() {
        if(Triggered == false) {
            Triggered = true;
            Invoke("PlayTurnOnParticle", 0.5f);
            print(this.gameObject.name + " turns on");
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
            print(this.gameObject.name + " turns off");
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
        turnOnParticle.Play();
    }

    private void StopTurnOnParticle() {
        turnOnParticle.Stop();
    }

}
