using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : CombatBase
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

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


        if (_movementController.isGrounded)
            _velocity.y = 0;

        // apply gravity before moving
        _velocity.y += gravity * Time.deltaTime;

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

        Vector3 velocity = new(_movementController.velocity.x, _movementController.velocity.y);
        Vector3 desiredPosition = Vector3.SmoothDamp(transform.position, point.position, ref velocity, Time.deltaTime, runSpeed);
        _movementController.Move(desiredPosition - transform.position);
    }

    protected void Move()
    {

    }

    protected void Jump()
    {

    }
}
