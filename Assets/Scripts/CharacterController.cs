using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float GravityAcceleration;
    public float JumpAcceleration;
    public float Speed;

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _verticalSpeed;

    private IsGrounded _footScript;

    // Start is called before the first frame update
    void Start()
    {
        _footScript = GetComponentInChildren<IsGrounded>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, _verticalSpeed * Time.deltaTime));

        _isGrounded = _footScript.isGrounded;

        if (_isGrounded)
        {
            _verticalSpeed = 0;
        }
        else
        {
            _verticalSpeed -= Time.deltaTime * GravityAcceleration;
        }
    }

    void OnJump()
    {
        Debug.Log("jump");
        if (_isGrounded)
            _verticalSpeed = JumpAcceleration;
    }
}
