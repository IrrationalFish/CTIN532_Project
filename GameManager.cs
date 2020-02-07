using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static Player player;
    public List<GameObject> magicalObjectList;

    void Start() {
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
}
