using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimCross : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 position = Mouse.current.position.ReadValue();
        position.z = 20;

        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}
