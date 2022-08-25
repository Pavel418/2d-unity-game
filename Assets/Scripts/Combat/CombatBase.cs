using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatBase : MonoBehaviour
{
    public Collider2D visionCollider;
    public List<Collider2D> PatrolPoints = new();
    public int Speed;

    [SerializeField]
    private int _currentPatrolPoint = 0;
    [SerializeField]
    private EnemyState _currentState;
    private CharacterStats _stats;
    private Collider2D _characterCollider;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _stats = GetComponent<CharacterStats>();
        _currentState = EnemyState.Idle;
        _characterCollider = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
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
    }

    public virtual void ChangeState(EnemyState newState)
    {
        if (newState == _currentState)
            return;
        OnStateChange(newState, _currentState);
        _currentState = newState;
    }

    protected virtual void OnStateChange(EnemyState newState, EnemyState oldState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrolling:
                break;
            case EnemyState.Chasing:
                break;
            case EnemyState.Attacking:
                break;
            default:
                break;
        }
    }

    protected virtual void Patrol()
    {
        var point = PatrolPoints[_currentPatrolPoint];

        if (point.IsTouching(_characterCollider))
        {
            _currentPatrolPoint++;
            if (_currentPatrolPoint >= PatrolPoints.Count)
                _currentPatrolPoint = 0;
            return;
        }

        Move(point.transform);
    }

    protected virtual void Move(Transform point)
    {
        transform.position = Vector2.Lerp(transform.position, point.position, Speed * Time.deltaTime);
    }
}

public enum EnemyState
{
    Idle,
    Patrolling,
    Chasing,
    Attacking
}