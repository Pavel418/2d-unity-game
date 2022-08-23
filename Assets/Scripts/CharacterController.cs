using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float GravityAcceleration;
    public float JumpAcceleration;
    public float Speed;
    public float HorizontalAcceleration;

    public const string JumpAbleLayerName = "Solid Object";

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _verticalSpeed;
    [SerializeField]
    private float _horizontalSpeed;
    [SerializeField]
    private float _horizontalAcceleration;

    private BoxCollider2D _footCollider;
    
    void Start()
    {
        _footCollider = GetComponentInChildren<BoxCollider2D>();
    }

    
    void Update()
    {
        StartCoroutine(nameof(UpdateCollider));
    }
    
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(_horizontalSpeed, _verticalSpeed) * Time.deltaTime);
        if (Speed > Mathf.Abs(_horizontalSpeed))
            _horizontalSpeed += _horizontalAcceleration * Time.deltaTime;
        else
            _horizontalSpeed = Speed;
    }

    void OnJump()
    {
        if (_isGrounded)
            _verticalSpeed = JumpAcceleration;
    }

    public void CheckGround()
    {
        _isGrounded = false;

        List<Collider2D> results = new();
        ContactFilter2D filter2D = new();
        Physics2D.OverlapCollider(_footCollider, filter2D.NoFilter(), results);

        foreach (var collider in results)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer(JumpAbleLayerName))
            {
                _isGrounded = true;
                break;
            }
        }
    }

    IEnumerator UpdateCollider()
    {
        yield return new WaitForFixedUpdate();

        CheckGround();

        if (_isGrounded)
        {
            _verticalSpeed = 0;
        }
        else
        {
            _verticalSpeed -= Time.deltaTime * GravityAcceleration;
        }
    }    

    void OnMove(InputValue value)
    {
        _horizontalAcceleration = HorizontalAcceleration * value.Get<float>();
    }
}
