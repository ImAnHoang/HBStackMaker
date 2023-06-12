using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float Value;

    private void LateUpdate()
    {
        Vector3 pos = Target.position + Offset;

        transform.position = Vector3.Lerp(transform.position, pos, Value);
    }
}
