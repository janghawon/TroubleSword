using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState
{
    Idle,
    Hit,
    Attack,
    Trace,
    Die
}

public class EnemyBrain : MonoBehaviour
{
    GameObject Player;
    EnemyAttackChooser _enemyAttackChooser;
    public EnemyState EnemyCurrentState = EnemyState.Idle;
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private UnityEvent<EnemyState> EnemyAnimationSetter = null;

    [SerializeField] private UnityEvent EnemyIdleEvent;
    [SerializeField] private UnityEvent<Vector3> EnemyTraceEvent;
    [SerializeField] private UnityEvent EnemyHitEvent;
    [SerializeField] private UnityEvent EnemyAttackBefore;
    [SerializeField] private UnityEvent<float> EnemyAttackEvent;
    [SerializeField] private UnityEvent EnemyDieEvent;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        _enemyAttackChooser = GetComponent<EnemyAttackChooser>();
    }

    private void Start()
    {
        _enemyAttackChooser.SetBulletCount(_enemySO.bulletCount);
    }

    private void DieState()
    {
        EnemyDieEvent?.Invoke();
    }

    private void AttackState()
    {
        EnemyAttackBefore?.Invoke();
        EnemyAttackEvent?.Invoke(_enemySO.AttackValue);
    }

    private void HitState()
    {
        EnemyHitEvent?.Invoke();
    }

    private void TraceState()
    {
        EnemyTraceEvent?.Invoke(Player.transform.position);
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= _enemySO.AttackRange)
        {
            EnemyCurrentState = EnemyState.Attack;
        }
    }

    private void IdleState()
    {
        EnemyIdleEvent?.Invoke();
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= _enemySO.DetectDistance)
        {
            EnemyCurrentState = EnemyState.Trace;
        }
    }

    private void StateAction()
    {
        switch(EnemyCurrentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;
            case EnemyState.Trace:
                TraceState();
                break;
            case EnemyState.Hit:
                HitState();
                break;
            case EnemyState.Attack:
                AttackState();
                break;
            case EnemyState.Die:
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
