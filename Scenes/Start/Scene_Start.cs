using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Start : MonoBehaviour {

    public GameObject player;
    public GameObject startMenu;
    public GameObject firstUITrigger;

    void Start() {

    }

    void Update() {

    }

    public void StartGame() {
        player.GetComponent<Movement>().enabled = true;
        player.GetComponent<FirstPersonCam>().enabled = true;
        player.GetComponent<PlayerUIManager>().enabled = true;
        Destroy(startMenu);
        Cursor.visible = false;
        firstUITrigger.SetActive(true);

    }
}
