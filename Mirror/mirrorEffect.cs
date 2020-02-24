using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorEffect : MonoBehaviour
{
    public Camera playerCamera;
    public Camera mirrorCamera;
    public Player playerBody;
    //public RenderTexture mirrorRT;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().eye;
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mirrorCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 mirror = new Vector3(1, 1, -1);

        mirrorCamera.transform.localPosition = Vector3.Scale(transform.InverseTransformPoint(playerCamera.transform.position), mirror);

        Vector3 lookat = new Vector3(playerCamera.transform.localEulerAngles.x, this.transform.eulerAngles.y+180 - playerBody.transform.eulerAngles.y, 0);

        mirrorCamera.transform.localEulerAngles = lookat;

        //mirrorCamera.nearClipPlane = Mathf.Abs(mirrorCamera.transform.localPosition.z);

        //Matrix4x4 destinationFlipRotation =
        //       Matrix4x4.TRS(MathUtil.ZeroV3, Quaternion.AngleAxis(180.0f, Vector3.up), MathUtil.OneV3);

        //Matrix4x4 sourceInvMat = destinationFlipRotation * this.transform.worldToLocalMatrix;

        //// Calculate translation and rotation of MainCamera in Source space
        //Vector3 cameraPositionInSourceSpace =
        //    MathUtil.ToV3(sourceInvMat * MathUtil.PosToV4(playerBody.transform.position));
        //cameraPositionInSourceSpace.x = -cameraPositionInSourceSpace.x;
        //cameraPositionInSourceSpace.y = cameraPositionInSourceSpace.y + 0.7f;

        //Quaternion playerBodyYaixRotationInMirrorSpace =
        //    MathUtil.QuaternionFromMatrix(sourceInvMat) * playerBody.transform.rotation;
        //Quaternion playerCameraXaixRotationInMirrorSpace =
        //    MathUtil.QuaternionFromMatrix(sourceInvMat) * playerCamera.transform.rotation;

        //Quaternion totalrotationInMirrorSpace = playerBodyYaixRotationInMirrorSpace;// new Quaternion();

        //totalrotationInMirrorSpace.y = -totalrotationInMirrorSpace.y;
        //totalrotationInMirrorSpace.x = playerCameraXaixRotationInMirrorSpace.x;
        ////totalrotationInMirrorSpace.z = 0.0f;

        //// Transform Portal Camera to World Space relative to Destination transform,
        //// matching the Main Camera position/orientation
        //mirrorCamera.transform.position = this.transform.TransformPoint(cameraPositionInSourceSpace);
        //mirrorCamera.transform.rotation = this.transform.rotation * totalrotationInMirrorSpace;

        // Calculate clip plane for portal (for culling of objects in-between destination camera and portal)
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
}
