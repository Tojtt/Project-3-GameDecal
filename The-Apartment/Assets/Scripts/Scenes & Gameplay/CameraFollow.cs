using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Follow_variables
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;
    public bool followEnabled;
    #endregion

    private void Start()
    {
        followEnabled = true;

    }

    private void LateUpdate() 
    {
        if (followEnabled)
            Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor* Time.fixedDeltaTime );
        transform.position = targetPosition;
    }

    public void SwitchToPosition(Vector3 pos)
    {
        Vector3 targetPosition = pos;
        transform.position = targetPosition;
    }
}
