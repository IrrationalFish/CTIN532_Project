using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static Player player;
    public GameObject arriveElevator;
    public GameObject playerPrefab;
    public Transform reswapnPoint;
    public List<GameObject> magicalObjectList;

    void Start() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject == null) {
            RespawnPlayer();
        } else {
            player = playerObject.GetComponent<Player>();
        }
        /*player.gameObject.transform.position = reswapnPoint.position;
        player.gameObject.transform.rotation = reswapnPoint.rotation;*/
        magicalObjectList.AddRange(GameObject.FindGameObjectsWithTag("MagicalObject"));
    }

    void Update() {
        foreach(GameObject obj in magicalObjectList) {
            if(player.IsInView(obj.transform.position)) {
                obj.GetComponent<MagicalObject>().InsideView();
            } else {
                obj.GetComponent<MagicalObject>().OutsideView();
            }
        }
    }

    public void RespawnPlayer() {
        if(player != null) {
            Destroy(player.gameObject);
        }
        player = Instantiate(playerPrefab, reswapnPoint.transform.position, reswapnPoint.transform.rotation).GetComponent<Player>();
    }

}
