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
        PlaceElevator();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject == null) {
            RespawnPlayer();
        } else {
            player = playerObject.GetComponent<Player>();
        }
        player.transform.parent = null;
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
            //used for convience, in developping scene, only placeholder and start directly.
            placeholderElevator.GetComponent<Elevator>().door.TurnOn();
        } else if(arriveElevator != null) {
            arriveElevator.GetComponent<Elevator>().door.TurnOn();
            if(placeholderElevator != null) {
                Destroy(placeholderElevator);
                arriveElevator.transform.position = placeholderPos.position;
                arriveElevator.transform.rotation = placeholderPos.rotation;
            }
        }
        /*if(placeholderElevator != null) {
            Destroy(placeholderElevator);
            arriveElevator.transform.position = placeholderPos.position;
            arriveElevator.transform.rotation = placeholderPos.rotation;
        }

        if(arriveElevator != null) {
            arriveElevator.GetComponent<Elevator>().door.TurnOn();

        }*/
    }

}
