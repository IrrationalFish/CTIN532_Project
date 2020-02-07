using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Camera eye;

    public bool IsInView(Vector3 worldPos) {
        Transform camTransform = eye.transform;
        Vector2 viewPos = eye.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面

        if(dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) {
            int layermask = 1 << 8;
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
}
