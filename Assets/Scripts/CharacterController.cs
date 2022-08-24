using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    #region Public Fields
    public float GravityAcceleration;
    public float JumpAcceleration;
    public float Speed;
    public float HorizontalAcceleration;

    public const string JumpAbleLayerName = "Solid Object";
    #endregion

    #region Private Fields
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _verticalSpeed;
    [SerializeField]
    private float _desiredHorizontalSpeed;
    [SerializeField]
    private float _currentHorizontalSpeed;

    private BoxCollider2D _footCollider;
    private AnimatorController _animator;
    #endregion

    #region Monobehaviour Events
    void Start()
    {
        _footCollider = GetComponentInChildren<BoxCollider2D>();
        _animator = GetComponentInChildren<AnimatorController>();
    }

    
    void Update()
    {
        StartCoroutine(nameof(UpdateCollider));
        transform.Translate(new Vector3(_currentHorizontalSpeed, _verticalSpeed) * Time.deltaTime, Space.World);

        string animation = "";
        if (_isGrounded)
        {
            if (_currentHorizontalSpeed != 0)
            {
                animation = AnimatorController.RUN;

                int rotation = _currentHorizontalSpeed > 0 ? 0 : 180;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }
            else
                animation = AnimatorController.IDLE;
        }
        else
        {
            //Jump animation
        }
        _animator.PlayAnimation(animation);
    }
    
    private void FixedUpdate()
    {
        _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, _desiredHorizontalSpeed, HorizontalAcceleration);
    }

    void OnJump()
    {
        if (!_isGrounded)
            return;

        _verticalSpeed = JumpAcceleration;
    }
    void OnMove(InputValue value)
    {
        _desiredHorizontalSpeed = Speed * value.Get<float>();
    }
    #endregion

    #region Public Methods
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
    #endregion

    #region Private Methods
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
    #endregion
}
