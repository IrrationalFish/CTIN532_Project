using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorEffect : MonoBehaviour
{
    public Camera playerCamera;
    public Camera mirrorCamera;
    public Player playerBody;
    static public ArrayList mirrors = new ArrayList();
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().eye;
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mirrorCamera = GetComponentInChildren<Camera>();
        mirrors.Add(this);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 mirror = new Vector3(1, 1, -1);

        mirrorCamera.transform.localPosition = Vector3.Scale(transform.InverseTransformPoint(playerCamera.transform.position), mirror);

        Vector3 lookat = new Vector3(playerCamera.transform.localEulerAngles.x, this.transform.eulerAngles.y+180 - playerBody.transform.eulerAngles.y, 0);

        mirrorCamera.transform.localEulerAngles = lookat;


        Vector4 clipPlaneWorldSpace =
            new Vector4(
                this.transform.forward.x,
                this.transform.forward.y,
                this.transform.forward.z,
                Vector3.Dot(this.transform.position, -this.transform.forward));

        Vector4 clipPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(mirrorCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;

        
        mirrorCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);

    }
    public bool IsInMirrorCameraView(Vector3 worldPos)
    {
        //if (remoteCamera == null || phoneMode == PhoneMode.Seeking)
        //{
        //    return false;
        //}
        Transform camTransform = mirrorCamera.transform;
        Vector2 viewPos = mirrorCamera.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            int layermask = 5 << 9;
            //int other = 0 << 10;
            //layermask = ~layermask - ~other;
            Debug.DrawLine(mirrorCamera.transform.position, worldPos);
            if (Physics.Linecast(mirrorCamera.transform.position, worldPos, layermask))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    public static bool isInMirrorsView(Vector3 worldPos)
    {
        
        foreach(mirrorEffect m in mirrorEffect.mirrors)
        {
            
            if (m.IsInMirrorCameraView(worldPos))
                return true;
        }
        return false;
    }
}
