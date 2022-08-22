using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float GravityForce;
    public float JumpForce;
    public float Speed;

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _notGroundedTime;
    [SerializeField]
    private float _verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position.Set(position.x, position.y + _verticalSpeed, position.z);

        if(!_isGrounded)
            _notGroundedTime += Time.deltaTime;

        //_verticalSpeed _= 
    }

    void OnJump()
    {
        if (_isGrounded)
            _verticalSpeed = JumpForce;
    }
}
