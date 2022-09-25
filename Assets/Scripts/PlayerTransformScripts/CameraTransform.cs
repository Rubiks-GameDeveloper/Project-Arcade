using System;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private VariableJoystick joystick;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float xLimit = 0f;
    [SerializeField] private float yLimit = 0f;
    
    [SerializeField] private AnimationCurve cameraSizeChangingCurve;
    [SerializeField] private float height;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float duration = 0.5f;

    [SerializeField] private float velocity = 0.3f;

    private void Start()
    {
        gameObject.transform.position += new Vector3(0, 2, 0);

        joystick.IsTouchIn.AddListener(CameraSizeIncrease);
        joystick.IsTouchOut.AddListener(CameraSizeDecrease);
        
        GetComponent<Camera>().orthographicSize = 5.872686f;
    }

    private void FixedUpdate()
    {
        var cameraPos = gameObject.transform.position;
        var playerPos = player.transform.position;
        var cameraXPos = Mathf.Clamp(playerPos.x + 1 + offset.x, yLimit , float.MaxValue);
        var cameraYPos = Mathf.Clamp(playerPos.y + 3.5f + offset.y, xLimit, float.MaxValue);
        var endCameraPos = new Vector3(cameraXPos, cameraYPos, cameraPos.z);
        gameObject.transform.position = Vector3.Lerp(cameraPos, endCameraPos, Time.fixedDeltaTime * velocity);
    }

    private IEnumerator MainCameraSizeChangeEnumerator(float sizeChangeDuration, bool isCameraIncrease)
    {
        float expiredTime = 0;
        float progress = 0;

        const float expiredTimeIncrease = 0.5f;
        
        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * expiredTimeIncrease;
        
            progress = expiredTime / sizeChangeDuration;
            mainCamera.orthographicSize += CameraAnimCurve(isCameraIncrease, progress) * height;
            
            yield return null;
        }
    }
    private float CameraAnimCurve(bool isCameraIncrease, float progress)
    {
        if (isCameraIncrease) return cameraSizeChangingCurve.Evaluate(progress);
        return -cameraSizeChangingCurve.Evaluate(progress);
    }
    private void CameraSizeIncrease()
    {
        StartCoroutine(MainCameraSizeChangeEnumerator(duration, true));
    }

    private void CameraSizeDecrease()
    {
        StartCoroutine(MainCameraSizeChangeEnumerator(duration, false));
    }
}
