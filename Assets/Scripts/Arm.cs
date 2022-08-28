using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arm : MonoBehaviour
{
    public GameObject Cross;
    public float MaxAngle;
    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 crossPosition = Cross.transform.position;

        Vector2 local = crossPosition - transform.position;
        float angle = Mathf.Acos(local.normalized.x) / Mathf.PI * 180;
        if (angle > MaxAngle)
            angle = MaxAngle;

        if (transform.position.y > crossPosition.y)
            angle *= -1;

        currentRotation.z = angle;
        transform.eulerAngles = currentRotation;
    }
}