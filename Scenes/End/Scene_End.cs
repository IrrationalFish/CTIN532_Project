using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_End : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Cursor.visible = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
