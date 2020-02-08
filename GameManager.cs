using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static Player player;
    public List<GameObject> magicalObjectList;

    void Start() {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        magicalObjectList.AddRange(GameObject.FindGameObjectsWithTag("MagicalObject"));
    }

    void Update() {
        foreach(GameObject obj in magicalObjectList) {
            if(player.IsInView(obj.transform.position)){
                obj.GetComponent<MagicalObject>().TurnOn();
            } else {
                obj.GetComponent<MagicalObject>().TurnOff();
            }
        }
    }

    void OnSceneLoaded(Scene scence, LoadSceneMode mod) {

    }
