using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraRotatePivot;
    [SerializeField] private Transform cameraPosPivot;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 positionOffset;


    private void Start()
    {
        cameraPosPivot.localPosition = positionOffset;
    }
    private void LateUpdate()
    {
        transform.position = playerTransform.position;
        cameraRotatePivot.rotation = playerTransform.rotation;
    }

}
