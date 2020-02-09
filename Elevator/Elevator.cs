using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

    public enum ElevatorType {
        Leave,
        Arrive
    }

    public ElevatorType type;
    public int destSceneIndex;

    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.buildIndex == 0) {     //load the loading scene
            print("Enter loading scene");
            StartCoroutine(LoadAsyneScene());
        }
    }

    IEnumerator LoadAsyneScene() {
        yield return null;
        float cacheTime = 2f;
        float timeAfterDone = 0f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(destSceneIndex);
        asyncLoad.allowSceneActivation = false;     
        while(!asyncLoad.isDone) {
            if(asyncLoad.progress >= 0.9f) {    // Check if the load has finished
                if(timeAfterDone < cacheTime) {
                    timeAfterDone += Time.deltaTime;
                } else {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SceneManager.LoadScene(0);  //to loading scene
            print("Enter leave elevator");
        }
    }
}
