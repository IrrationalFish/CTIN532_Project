using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Camera eye;

    public RemCamController rcm;
    public GameObject phone;

    public bool byStander;

    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        rcm = GetComponent<RemCamController>();
        if(rcm.enabled == false) {
            phone.SetActive(false);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            this.GetComponent<PlayerUIManager>().SwitchByStanderMode();
            DataCollector.RecordOneByStander();
        }
    }
    public bool IsInView(Vector3 worldPos) {
        if(byStander) {
            return false;
        }
        if(IsInPersonView(worldPos)) {
            return true;
        } else {
            if(rcm.enabled && rcm.IsInRemoteCameraView(worldPos)) {
                return true;
            }
            else if (mirrorEffect.isInMirrorsView(worldPos))
            {
                print("is in mirror view");
                return true;
            }
            else {
                return false;
            }
        }
    }

    public bool IsInPersonView(Vector3 worldPos) {
        Transform camTransform = eye.transform;
        Vector2 viewPos = eye.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面

        if(dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) {
            int layermask = 1 << 8 | 1 << 5;
            layermask = ~layermask;
            Debug.DrawLine(eye.transform.position, worldPos);
            if(Physics.Linecast(eye.transform.position, worldPos, layermask)) {
                //print("In view area, hide behind wall");
                return false;
            } else {
                //print("In view area, not hide behind wall");
                return true;
            }
        } else {
            //print("Not in view area");
            return false;
        }
    }

    public void ObtainPhone() {
        if(rcm == null) {
            rcm = GetComponent<RemCamController>();
        }
        rcm.enabled = true;
        phone.SetActive(true);
        Global.obtainedPhone = true;
    }
}
