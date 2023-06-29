using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState
{
    Idle,
    hit,
    attack,
    trace,
    die
}

public class EnemyBrain : MonoBehaviour
{
    GameObject Player;
    public EnemyState EnemyCurrentState = EnemyState.Idle;
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private UnityEvent<EnemyState> EnemyAnimationSetter = null;

    [SerializeField] private UnityEvent EnemyIdleEvent;
    [SerializeField] private UnityEvent EnemyTraceEvent;
    [SerializeField] private UnityEvent EnemyHitEvent;
    [SerializeField] private UnityEvent EnemyAttackEvent;
    [SerializeField] private UnityEvent EnemyDieEvent;

    private void Awake()
    {
        Player = GameObject.Find("Player");
    }

    private void DieState()
    {
        EnemyDieEvent?.Invoke();
    }

    private void AttackState()
    {
        EnemyAttackEvent?.Invoke();
    }

    private void HitState()
    {
        EnemyHitEvent?.Invoke();
    }

    private void TraceState()
    {
        EnemyTraceEvent?.Invoke();
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= _enemySO.AttackRange)
        {
            EnemyCurrentState = EnemyState.attack;
        }
    }

    private void IdleState()
    {
        EnemyIdleEvent?.Invoke();
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= _enemySO.DetectDistance)
        {
            EnemyCurrentState = EnemyState.trace;
        }
    }

    private void StateAction()
    {
        switch(EnemyCurrentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.trace:
                TraceState();
                break;
            case EnemyState.hit:
                HitState();
                break;
            case EnemyState.attack:
                AttackState();
                break;
            case EnemyState.die:
                DieState();
                break;
        }
        EnemyAnimationSetter?.Invoke(EnemyCurrentState);
    }

    private void Update()
    {
        StateAction();
    }
}
