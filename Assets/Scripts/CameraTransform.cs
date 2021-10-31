using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CameraTransform : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private VariableJoystick joystick;
    
    [SerializeField] private AnimationCurve cameraSizeChangingCurve;
    [SerializeField] private float height;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float duration = 0.5f;

    private Coroutine data;
    private void Start()
    {
        gameObject.transform.position += new Vector3(0, 2, 0);

        joystick.IsTouchIn.AddListener(CameraResize);
        joystick.IsTouchOut.AddListener(CameraResize);
        
        GetComponent<Camera>().orthographicSize = 5;
    }

    private void FixedUpdate()
    {
        if (!PrototypePlayerMovement.IsPlayerMove) return;
        var o = gameObject;
        o.transform.position = new Vector3(player.transform.position.x, 2, o.transform.position.z);
    }

    private IEnumerator MainCameraSizeChangeEnumerator(float duration)
    {
        
        float expiriedTime = 0;
        float progress = 0;

        if (mainCamera.orthographicSize < 6)
        {
            while (progress < 1)
            {
                expiriedTime += Time.fixedDeltaTime * 0.1f;
            
                progress = expiriedTime / duration;
                mainCamera.orthographicSize += cameraSizeChangingCurve.Evaluate(progress) * height;

                yield return null;
            } 
        }
        else if (mainCamera.orthographicSize >= 6)
        {
            while (progress < 1)
            {
                expiriedTime += Time.fixedDeltaTime * 0.1f;
            
                progress = expiriedTime / duration;
                mainCamera.orthographicSize -= cameraSizeChangingCurve.Evaluate(progress) * height;

                yield return null;
            }
        }

        data = null;
    }

    private void CameraResize()
    {
        if (mainCamera.orthographicSize < 6)
        { 
            if (data == null) data = StartCoroutine(MainCameraSizeChangeEnumerator(duration));
            /*
            else
            {
                StopCoroutine(data);
                data = null;
            }*/
        }
        else if (mainCamera.orthographicSize >= 6)
        {
            if (data == null) data = StartCoroutine(MainCameraSizeChangeEnumerator(duration));
            /*
            else
            {
                StopCoroutine(data);
                data = null;
            }*/
        }
    }
}
