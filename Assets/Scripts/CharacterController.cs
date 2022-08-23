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

    private float _horizontalSpeed;

    private IsGrounded _footScript;

    [SerializeField] private float speed;
    

    
    void Start()
    {
        _footScript = GetComponentInChildren<IsGrounded>();
    }

    
    void Update()
    {
        transform.Translate(new Vector3(_horizontalSpeed * Time.deltaTime, 0));
        transform.Translate(new Vector3(0, _verticalSpeed * Time.deltaTime));
        _footScript.CheckGround();

        _isGrounded = _footScript.isGrounded;

        if (_isGrounded)
        {
            _verticalSpeed = 0;
        }
        else
        {
            _verticalSpeed -= Time.deltaTime * GravityAcceleration;
        }

        //Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        

        //.flipX = dir.x < 0.0f;
    }

    void OnJump()
    {
        Debug.Log("jump");
        if (_isGrounded)
            _verticalSpeed = JumpAcceleration;
    }

    void OnMove(InputValue value)
    {
        _horizontalSpeed = speed * value.Get<float>();
    }
}
