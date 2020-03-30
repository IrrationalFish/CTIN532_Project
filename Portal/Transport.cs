using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    private Transform In;
    public Transform Out;
    private GameObject player;
    private Vector3 PreviousPosition;
    // Start is called before the first frame update
    void Start()
    {
        In = this.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        PreviousPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 destinationFlipRotation = Matrix4x4.TRS(
            MathUtil.ZeroV3, Quaternion.AngleAxis(180.0f, Vector3.up), MathUtil.OneV3);
        Matrix4x4 InInvMat = destinationFlipRotation * In.worldToLocalMatrix;

        Vector3 vecToCurrentPosition = player.transform.position - In.transform.position;
        Vector3 vecToPreviousPosition = PreviousPosition - In.transform.position;

        // Rough distance thresholds we must be within to teleport
        float sideDistance = Vector3.Dot(In.transform.right, vecToCurrentPosition);
        float frontDistance = Vector3.Dot(In.transform.forward, vecToCurrentPosition);
        float heightDistance = Vector3.Dot(In.transform.up, vecToCurrentPosition);
        float previousFrontDistance = Vector3.Dot(In.transform.forward, vecToPreviousPosition);
        PreviousPosition = player.transform.position;
        // Have we just crossed the portal threshold
        if (frontDistance < 0.0f
            && previousFrontDistance >= 0.0f
            && Mathf.Abs(sideDistance) < /*approx door_width*/ 1.0f
            && Mathf.Abs(heightDistance) < /*approx door_height*/ 1.2f)
        {
            // If so, transform the CamController to Out transform space

            Vector3 playerPositionInLocalSpace =
                MathUtil.ToV3(InInvMat * MathUtil.PosToV4(player.transform.position));
            Vector3 newPlayerPositionWorldSpace = Out.TransformPoint(playerPositionInLocalSpace);

            player.transform.position = newPlayerPositionWorldSpace;

            Quaternion cameraRotationInSourceSpace =
                MathUtil.QuaternionFromMatrix(InInvMat) * player.transform.rotation;

            player.transform.rotation = Out.rotation * cameraRotationInSourceSpace;
            print("teleport");
        }
    }
}
