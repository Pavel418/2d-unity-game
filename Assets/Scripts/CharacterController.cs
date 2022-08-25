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
    public int WaitFramesToShootBullet;

    public const string JumpAbleLayerName = "Solid Object";
    #endregion

    #region Private Fields
    [SerializeField]
    private bool _isGrounded;
    private float _verticalSpeed;
    [SerializeField]
    private float _desiredHorizontalSpeed;
    private float _currentHorizontalSpeed;
    [SerializeField]
    private bool _isShooting;
    [SerializeField]
    private bool _inputShootResult;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _shootPoint;

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

        if (_currentHorizontalSpeed > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_currentHorizontalSpeed < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        //Animation Handling
        if (_isShooting)
        {
            return;
        }

        string animation = (_isGrounded, _currentHorizontalSpeed) switch
        {
            { _isGrounded: true, _currentHorizontalSpeed: var x} when x != 0 => AnimatorController.RUN,
            (_, _) => AnimatorController.IDLE
        };
        _animator.PlayAnimation(animation);
    }
    
    private void FixedUpdate()
    {
        _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, _desiredHorizontalSpeed, HorizontalAcceleration);
    }
    #endregion

    #region Input Events
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

    void OnShoot(InputValue value)
    {
        _inputShootResult = value.isPressed;
        StartCoroutine(nameof(Shooting));
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

    IEnumerator Shooting()
    {
        while (_inputShootResult)
        {
            _isShooting = true;

            string animation = _currentHorizontalSpeed switch
            {
                0 => AnimatorController.SHOOT,
                _ => AnimatorController.RUN_SHOOT
            };

            float animationTime = _animator.PlayAnimation(animation);

            int frames = WaitFramesToShootBullet;
            while (frames > 0)
            {
                frames--;
                yield return new WaitForEndOfFrame();
            }

            GameObject bullet = Instantiate(_bullet, _shootPoint.transform.position, transform.rotation);
            bullet.GetComponent<FlyBullet>();
            bullet.SetActive(true);

            yield return new WaitForSeconds(animationTime);
            _isShooting = false;
        }
    }
    #endregion
}
