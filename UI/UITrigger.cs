using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour {
    
    public enum uiType {
        Tutorial,
        Sub
    }

    public uiType type;
    public string words;
    public float time;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            if(type == uiType.Tutorial) {
                other.gameObject.GetComponent<PlayerUIManager>().SetTutorialText(words, time);
                Destroy(this.gameObject);
            }
        }
    }
}
