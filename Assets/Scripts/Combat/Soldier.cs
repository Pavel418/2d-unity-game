using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : CombatBase
{
	// movement config
	public float Gravity = -25f;
	public float RunSpeed = 5f;
    public float Acceleration = 30f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float JumpStrength = 3f;

    protected float _normalizedHorizontalSpeed = 0;
    protected RaycastHit2D _lastControllerColliderHit;
    protected Vector3 _velocity;

    public List<Transform> PatrolPoints = new();
	public float reachDistance;
	[SerializeField]
	protected int _currentPatrolPoint = 0;
	protected bool _executionOrder = true;

    public override void Update()
    {
        base.Update();
        if (_movementController.isGrounded)
            _velocity.y = 0;

        switch (_currentState)
        {
            case EnemyState.Idle:
                ChangeState(EnemyState.Patrolling);
                break;
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                break;
            case EnemyState.Attacking:
                break;
            default:
                break;
        }

        // apply gravity before moving
        _velocity.y += Gravity * Time.deltaTime;

        _movementController.Move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _movementController.velocity;
    }

    protected virtual void Patrol()
    {
        var point = PatrolPoints[_currentPatrolPoint];

        if (reachDistance > Vector2.Distance(transform.position, point.position))
        {
            if (_executionOrder)
                _currentPatrolPoint++;
            else
                _currentPatrolPoint--;

            if (_currentPatrolPoint == 0)
                _executionOrder = true;

            if (_currentPatrolPoint >= PatrolPoints.Count - 1)
                _executionOrder = false;

            return;
        }

        //Vector3 velocity = new(_movementController.velocity.x, _movementController.velocity.y);
        //Vector3 desiredPosition = Vector3.SmoothDamp(transform.position, point.position, ref velocity, Time.deltaTime, RunSpeed);
        //_movementController.Move(desiredPosition - transform.position);
        Move(point.position);
    }

    protected void Move(Vector3 destination)
    {
        float x = transform.position.x;
        if (destination.x == x)
            return;

        bool movingRight = destination.x > x;

        bool checkCollision = movingRight ? _movementController.collisionState.right : _movementController.collisionState.left;

        if (checkCollision)
            Jump();

        float desiredSpeed = RunSpeed;
        if (!movingRight)
            desiredSpeed *= -1;
        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredSpeed, Time.deltaTime * Acceleration);
    }

    protected void Jump()
    {
        _velocity.y = JumpStrength;
    }
}
