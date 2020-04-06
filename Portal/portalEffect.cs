using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEffect : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Camera portalCamera;
    private Camera playerCamera;
    public Player playerBody;
    private RenderTexture renderTexture;
    public RenderTexture baseTexture;
    private Material material;
    public GameObject portal;
    public GameObject selfQuad;
    //public RenderTexture test;
    void Start()
    {
        //playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().eye;
        //playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //renderTexture = new RenderTexture(720, 720, 16, RenderTextureFormat.ARGB32);
        renderTexture = new RenderTexture(baseTexture);
        renderTexture.Create();
        portalCamera.targetTexture = renderTexture;
        material = new Material(Shader.Find("Custom/PortalShader"));
        //material.SetTexture(Shader.PropertyToID("Custom/PortalShader"), test);
        material.mainTexture = renderTexture;
        selfQuad.GetComponent<MeshRenderer>().material = material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerBody == null||playerCamera == null )
        {
            playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().eye;
            playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
       if(Vector3.Distance(playerBody.transform.position, this.transform.position) < 7)
        {
            portalCamera.cullingMask = 1;
        }else if (Vector3.Dot(playerBody.transform.forward, this.transform.forward) > 0)
        {
            portalCamera.cullingMask = 0;
        }
        else
        {
            portalCamera.cullingMask = 1;

        }
        Vector3 portalChange = new Vector3(-1, 1, -1);

        portalCamera.transform.localPosition = Vector3.Scale(transform.InverseTransformPoint(playerCamera.transform.position), portalChange);
        ////////////////////////////////////////////////////////////////
        Matrix4x4 destinationFlipRotation =
               Matrix4x4.TRS(MathUtil.ZeroV3, Quaternion.AngleAxis(180.0f, Vector3.up), MathUtil.OneV3);
        Matrix4x4 sourceInvMat = destinationFlipRotation * this.transform.worldToLocalMatrix;

        Quaternion cameraRotationInSourceSpace =
                MathUtil.QuaternionFromMatrix(sourceInvMat) * playerBody.transform.rotation;

        //Quaternion Protation = portal.transform.rotation * cameraRotationInSourceSpace;


        //Vector3 lookat = new Vector3(playerCamera.transform.localEulerAngles.x, portal.transform.eulerAngles.y - Mathf.Abs(this.transform.eulerAngles.y - playerBody.transform.eulerAngles.y), 0);
        Vector3 lookat = new Vector3(playerCamera.transform.localEulerAngles.x, cameraRotationInSourceSpace.eulerAngles.y, 0);

        portalCamera.transform.localEulerAngles = lookat;


        Vector4 clipPlaneWorldSpace =
            new Vector4(
                portal.transform.forward.x,
                portal.transform.forward.y,
                portal.transform.forward.z,
                Vector3.Dot(portal.transform.position, -portal.transform.forward));

        Vector4 clipPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;


        portalCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);

    }
}
