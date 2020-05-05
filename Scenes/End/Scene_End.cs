using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_End : MonoBehaviour {

    public string extraSceneName;

    void Start() {
        Cursor.visible = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

    // Update is called once per frame
    void Update() {

    }

    public void LoadExtraLevel() {
        SceneManager.LoadScene(extraSceneName);
        Cursor.visible = false;
        Global.obtainedPhone = false;
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
