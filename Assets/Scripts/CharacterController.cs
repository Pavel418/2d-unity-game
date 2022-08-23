using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float GravityAcceleration;
    public float JumpAcceleration;
    public float Speed;

    public const string JumpAbleLayerName = "Solid Object";

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _verticalSpeed;


    private float _horizontalSpeed;

    

    private BoxCollider2D _footCollider;


    [SerializeField] private float speed;
    

    
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
        transform.Translate(new Vector3(0, _verticalSpeed * Time.deltaTime));

        transform.Translate(new Vector3(_horizontalSpeed * Time.deltaTime, 0));
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

        //Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        

        //.flipX = dir.x < 0.0f;
    }


    

    void OnMove(InputValue value)
    {
        _horizontalSpeed = speed * value.Get<float>();
    }

}
