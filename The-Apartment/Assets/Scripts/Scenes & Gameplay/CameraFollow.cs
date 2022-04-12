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
    bool a = false;
    #endregion

    private void LateUpdate() 
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor* Time.fixedDeltaTime );
        transform.position = targetPosition;
    }
}
