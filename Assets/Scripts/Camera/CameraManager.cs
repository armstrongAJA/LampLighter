using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;

    [Header("Controls for lerping camera damping during player fall/jump")]
    [SerializeField] private float fallPanAmount = 0.25f;
    [SerializeField] private float fallPanTime = 0.25f;
    public float fallSpeedDampingChangeThreshold = -15f;

    public bool isLerpingYDamping { get; private set; }
    public bool lerpedFromPlayerFalling { get; set; }
    private Coroutine LerpYPanCoroutine;
    private Coroutine panCameraCoroutine;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private float normalYPanAmount;

    private Vector2 startingTrackedObjectOffset;


    private void Awake()
    {
        if (instance == null)//check if no singleton instance, if so change instance to this one instead
        {
            instance = this;
        }
        for (int i = 0; i < allVirtualCameras.Length; i++)//loop over all the virtual cameras in the list
        {
            if (allVirtualCameras[i].enabled)//find the camera(s) which is(are) enabled
            {
                currentCamera = allVirtualCameras[i];//set current camera
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();//set the framing transposer

            }
        }
        normalYPanAmount = framingTransposer.m_YDamping;//set normal y damping
        //set the starting position of the tracked object offset
        startingTrackedObjectOffset = framingTransposer.m_TrackedObjectOffset;
    }
    #region Lerp the Y damping
    public void LerpYDamping(bool isPlayerFalling)
    {
        LerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));

    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        isLerpingYDamping = true;
        //grab the starting damping amount
        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;
        //determine the end damping amount
        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmount;
            lerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normalYPanAmount;
        }
        isLerpingYDamping = false;
        //lerp the pan amount
        float elapsedTime = 0f;
        while (elapsedTime < fallPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime / fallPanTime);
            framingTransposer.m_YDamping = lerpedPanAmount;
            yield return null;
        }
    }


    #endregion
    #region Pan Camera
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }
    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        //handle pan from trigger
        if (!panToStartingPos)
        {
            //set the direction and distance
            switch(panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.left;
                    break;
                case PanDirection.Right:
                    break;
                default:
                    break;
            }
            endPos *= panDistance;
            startingPos *= startingTrackedObjectOffset;
            endPos += startingPos;
        }
        //handle the pan back to starting position
        else
        {
            startingPos = framingTransposer.m_TrackedObjectOffset;
            endPos = startingTrackedObjectOffset;
        }
        //handle the actual panning of the camera
        float elapsedTime = 0f;
        while(elapsedTime<panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            framingTransposer.m_TrackedObjectOffset = panLerp;
            yield return null;
        }
    }
    #endregion
    #region Swap Cameras
    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        //if current camera is on left and exit direction is on the right
        if (currentCamera == cameraFromLeft && triggerExitDirection.x > 0f)
        {
            //activate new camera
            cameraFromRight.enabled = true;
            //deactivate old camera
            cameraFromLeft.enabled = false;
            //set new camera as current camera
            currentCamera = cameraFromRight;
            //update framing transposer variable
            framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        //if current camera is on left and exit direction is on the right
        else if (currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            //activate new camera
            cameraFromLeft.enabled = true;
            //deactivate old camera
            cameraFromRight.enabled = false;
            //set new camera as current camera
            currentCamera = cameraFromLeft;
            //update framing transposer variable
            framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    #endregion
}
