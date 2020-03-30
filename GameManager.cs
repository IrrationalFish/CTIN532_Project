using System;
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
        //Cursor.visible = false;
        PlaceElevator();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject == null) {
            RespawnPlayer();
        } else {
            player = playerObject.GetComponent<Player>();
        }
        player.gameObject.transform.parent = null;
        if(Global.obtainedPhone) {
            player.ObtainPhone();
        }
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
        /*player.gameObject.transform.position = reswapnPoint.transform.position;
        player.gameObject.transform.rotation = reswapnPoint.transform.rotation;
        return;*/
        if(player != null) {
            if(Global.obtainedPhone&& player.GetComponent<RemCamController>().remoteCamera!=null)
                Destroy(player.GetComponent<RemCamController>().remoteCamera.gameObject);
            Destroy(player.gameObject);
        }
        player = Instantiate(playerPrefab, reswapnPoint.transform.position, reswapnPoint.transform.rotation).GetComponent<Player>();
        if(Global.obtainedPhone) {
            player.ObtainPhone();
        }
    }

    public void PlaceElevator() {   //delete placeholder elevator and place arrive elevator
        Transform placeholderPos = null;
        GameObject placeholderElevator = null;
        GameObject arriveElevator = null;
        GameObject[] Elevators = GameObject.FindGameObjectsWithTag("Elevator");
        foreach(GameObject e in Elevators) {
            Elevator.ElevatorType type = e.GetComponent<Elevator>().type;
            if(type == Elevator.ElevatorType.Placeholder) {
                placeholderPos = e.transform;
                placeholderElevator = e;
            }else if(type == Elevator.ElevatorType.Arrive) {
                arriveElevator = e;
            }
        }

        if(arriveElevator == null && placeholderElevator != null) {
            print("no arrive lift and there is placeholder in the scene, open its door");
            //used for convience, in developping scene, only placeholder and start directly.
            placeholderElevator.GetComponent<Elevator>().door.TurnOn();
        } else if(arriveElevator != null) {
            print("get arrive lift, open its door");
            //arriveElevator.GetComponent<Elevator>().door.TurnOn();
            arriveElevator.GetComponent<Elevator>().ArriveLevel();
            if(placeholderElevator != null) {
                print("get placeholder, destory it");
                Destroy(placeholderElevator);
                arriveElevator.transform.position = placeholderPos.position;
                arriveElevator.transform.rotation = placeholderPos.rotation;
            }
        }
    }

    public static IEnumerator DelayToInvoke(Action action, float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

}
