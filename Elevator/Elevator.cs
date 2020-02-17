using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

    public enum ElevatorType {
        Placeholder,
        Leave,
        Loading,
        Arrive
    }

    public ElevatorType type;
    public int destSceneIndex;
    public float cacheTime = 2f;

    public Door door;

    private void Start() {
        if(type == ElevatorType.Placeholder) {
            //Destroy(this.gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        door = this.gameObject.GetComponentInChildren<Door>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.buildIndex == 1) {     //load the loading scene
            if(type == ElevatorType.Leave) {
                print("Enter loading scene");
                type = ElevatorType.Loading;
                StartCoroutine(LoadAsyneScene());
            } else if(type == ElevatorType.Arrive) {
                Destroy(this.gameObject);
            }
        } else {
            if(type == ElevatorType.Loading) {
                type = ElevatorType.Arrive;
            }
        }
    }

    IEnumerator LoadAsyneScene() {
        yield return null;
        //float cacheTime = 2f;
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
        if(type == ElevatorType.Leave) {
            GameObject player;
            if(other.gameObject.CompareTag("Player")) {
                player = other.gameObject;
                player.transform.parent = this.gameObject.transform;
                print("player attaches to elevator");
                this.gameObject.GetComponent<BoxCollider>().enabled = false;

                door.TurnOff();

                Invoke("LoadLoadingScene", 1.5f);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(type == ElevatorType.Leave) {
            GameObject player;
            if(other.gameObject.CompareTag("Player")) {
                
                
            }
        }
    }

    public void LoadLoadingScene() {
        SceneManager.LoadScene(1);  //to loading scene
        print("Enter leave elevator");
    }
}
