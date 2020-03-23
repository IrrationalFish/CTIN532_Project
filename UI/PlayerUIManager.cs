using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {

    public bool MenuOn = false;
    public GameObject MenuObject;

    public Text tutorialText;

    void Start() {
        if(MenuOn) {
            SwitchMenuOnOff();
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SwitchMenuOnOff();
        }
    }

    public void SetTutorialText(string text, float time) {
        tutorialText.text = text.Replace("\\n", "\n");
        StartCoroutine(GameManager.DelayToInvoke(() => {tutorialText.text = "";}, time));
    }

    public void SwitchMenuOnOff() {
        if(MenuOn) {
            MenuObject.SetActive(false);
            GetComponent<Movement>().enabled = true;
            GetComponent<FirstPersonCam>().enabled = true;
            GetComponent<Player>().enabled = true;
            GetComponent<RemCamController>().enabled = true;
            Cursor.visible = false;
            MenuOn = false;
        } else {
            MenuObject.SetActive(true);
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Movement>().enabled = false;
            GetComponent<FirstPersonCam>().enabled = false;
            GetComponent<Player>().enabled = false;
            GetComponent<RemCamController>().enabled = false;
            Cursor.visible = true;
            MenuOn = true;
        }
    }

    public void Restart() {
        GameObject g = GameObject.Find("GameManager");
        if(g != null) {
            g.GetComponent<GameManager>().RespawnPlayer();
        }
        Cursor.visible = false;
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
