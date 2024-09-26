using System.Collections;
using UnityEngine;

public class HorseCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private float followSmoothTime = 0.3f;
    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private Vector3 rotationOffset;

    private Vector3 currentVelocity = Vector3.zero;
    [SerializeField]
    private Vector3 finishPositionOffset;
    [SerializeField]
    private Vector3 finishRotationOffset;
   

    public bool isFinished = false;

    private void LateUpdate()
    {
      
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    [ContextMenu("Sw")]
    public void SwitchToFinishPosition()
    {
        isFinished = true;
        transform.position = finishPositionOffset;
        transform.rotation = Quaternion.Euler(finishRotationOffset);
        Time.timeScale = 0.05f;
        
    }


    private void FollowTarget()
    {
        if (isFinished == false)
        {
            Vector3 desiredPosition = CalculateDesiredPosition();
            UpdateCameraPosition(desiredPosition);
            UpdateCameraRotation();
        }
    }

    private Vector3 CalculateDesiredPosition()
    {
        return targetObject.position + targetObject.rotation * positionOffset;
    }

    private void UpdateCameraPosition(Vector3 desiredPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, followSmoothTime);
    }

    private void UpdateCameraRotation()
    {
        Quaternion desiredRotation = targetObject.rotation * Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, followSmoothTime);
    }


    private void Start()
    {
        ResetCameraPosition();
    }
    [ContextMenu("RS")]
    public void ResetCameraPosition()
    {
        isFinished = false;
        transform.position = positionOffset;
        transform.rotation = Quaternion.Euler(rotationOffset);
        Time.timeScale = 1f;
    }
}
