using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemCamController : MonoBehaviour {

    public enum PhoneMode {
        Seeking,
        Watching,
    }

    public PhoneMode phoneMode;

    public GameObject remCamPrefab;
    public Transform remCamRespawnTran;
    public Camera remoteCamera;

    public GameObject screen;
    public TextMeshProUGUI screenText;
    public float modeTextSpeed;
    public GameObject signalText;
    public TextMeshProUGUI buttonText;

    public Material camTex;
    public Material remCamTex;

    [Header("For hide and show")]
    public bool isHiden = false;
    public GameObject phoneModel;
    public float maxSpeed;
    public Transform showPos;
    public Transform hidePos;
    private Transform dest;

    [Header("For new control")]
    public bool applyNewControl;
    public float requiredHoldTime;
    public float currentHoldTime;
    public Slider droneSlider;

    private GameManager gm;
    private int seekingColorDir = -1;

    void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dest = showPos;
        SetToSeeking();
    }


    void Update() {
        if(dest != null && !phoneModel.transform.localPosition.Equals(dest.localPosition)) {
            phoneModel.transform.localPosition = Vector3.MoveTowards(phoneModel.transform.localPosition, dest.localPosition, maxSpeed);
        }
        if(phoneMode == PhoneMode.Watching) {
            if(remoteCamera == null) {
                if(!signalText.activeSelf) {
                    signalText.SetActive(true);
                }
            } else {
                if(signalText.activeSelf) {
                    signalText.SetActive(false);
                }
            }
        } else {    //in seeking mode
            if(signalText.activeSelf) {
                signalText.SetActive(false);
            }
            if(screenText.color.a <= 0) {
                seekingColorDir = 1;
            } else if(screenText.color.a >= 1) {
                seekingColorDir = -1;
            }
            screenText.color = new Color(1, 1, 1, screenText.color.a + (seekingColorDir * modeTextSpeed * Time.deltaTime));
        }
        if(Input.GetKeyDown(KeyCode.Tab)) {
            if(isHiden) {
                Show();
            } else {
                Hide();
            }
        }
        if(!applyNewControl) {
            if(!isHiden && Input.GetKeyDown(KeyCode.Q) && screen.GetComponent<MeshRenderer>().material.name == "CameraTex (Instance)") {
                if(remoteCamera != null) {
                    Destroy(remoteCamera.gameObject);
                }
                remoteCamera = (Instantiate(remCamPrefab, remCamRespawnTran.position, remCamRespawnTran.rotation) as GameObject).GetComponent<Camera>();
                SwitchMode();
            }
            if(Input.GetKeyDown(KeyCode.E)) {
                SwitchMode();
            }
        } else {
            droneSlider.value = currentHoldTime / requiredHoldTime;
            if(!isHiden) {
                if(Input.GetKey(KeyCode.E)) {
                    currentHoldTime += Time.deltaTime;
                }
                if(Input.GetKeyUp(KeyCode.E)) {
                    if(currentHoldTime >= requiredHoldTime) {
                        //place drone
                        if(remoteCamera != null) {
                            Destroy(remoteCamera.gameObject);
                        }
                        remoteCamera = (Instantiate(remCamPrefab, remCamRespawnTran.position, remCamRespawnTran.rotation) as GameObject).GetComponent<Camera>();
                        SetToWatching();
                    } else {
                        SwitchMode();
                    }
                    currentHoldTime = 0;
                }
            }
        }
    }

    public bool IsInRemoteCameraView(Vector3 worldPos) {
        if(remoteCamera == null || phoneMode == PhoneMode.Seeking || isHiden) {
            return false;
        }
        Transform camTransform = remoteCamera.transform;
        Vector2 viewPos = remoteCamera.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面

        if(dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) {
            int layermask = 0 << 8;
            layermask = ~layermask;
            Debug.DrawLine(remoteCamera.transform.position, worldPos);
            if(Physics.Linecast(remoteCamera.transform.position, worldPos, layermask)) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }

    public void SwitchMode() {
        if(phoneMode == PhoneMode.Seeking) {
            SetToWatching();
        } else {
            SetToSeeking();
        }
    }

    public void SetToSeeking() {
        screen.GetComponent<MeshRenderer>().material = camTex;
        phoneMode = PhoneMode.Seeking;
        screenText.text = "Seeking...";
        buttonText.text = "Press Q to set monitor";
        screenText.color = new Color(1, 1, 1, 1);
        seekingColorDir = -1;
    }

    public void SetToWatching() {
        screen.GetComponent<MeshRenderer>().material = remCamTex;
        phoneMode = PhoneMode.Watching;
        screenText.text = "Watching...";
        buttonText.text = "Press E to switch mode";
        screenText.color = new Color(1, 1, 1, 1);
        seekingColorDir = 0;
    }

    private void Hide() {
        currentHoldTime = 0;
        dest = hidePos;
        isHiden = true;
    }

    private void Show() {
        currentHoldTime = 0;
        dest = showPos;
        isHiden = false;
    }

}
