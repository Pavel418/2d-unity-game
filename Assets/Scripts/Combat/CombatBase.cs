using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatBase : MonoBehaviour
{
    public Collider2D visionCollider;

    [SerializeField]
    protected EnemyState _currentState;
    protected CharacterStats _stats;
    protected Collider2D _characterCollider;
    protected MovementController _movementController;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _stats = GetComponent<CharacterStats>();
        _currentState = EnemyState.Idle;
        _characterCollider = GetComponentInChildren<Collider2D>();
        _movementController = GetComponentInChildren<MovementController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

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
}

public enum EnemyState
{
    Idle,
    Patrolling,
    Chasing,
    Attacking
}