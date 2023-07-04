using System;
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
    EnemyHealth _enemyHealth;
    public EnemyState EnemyCurrentState = EnemyState.Idle;
    public bool isDie;
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
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyAttackChooser = GetComponent<EnemyAttackChooser>();
    }

    private void Start()
    {
        _enemyHealth.SetHP(_enemySO.EnemyHP);
        _enemyAttackChooser.SetBulletCount(_enemySO.bulletCount, _enemySO.AttackSpeedValue);
    }

    private void DieState()
    {
        EnemyDieEvent?.Invoke();
    }

    private void AttackState()
    {
        EnemyAttackBefore?.Invoke();
        EnemyAttackEvent?.Invoke(_enemySO.AttackValue);
        if (Vector3.Distance(Player.transform.position, this.transform.position) > _enemySO.AttackRange)
        {
            EnemyCurrentState = EnemyState.Trace;
        }
    }

    private void HitState()
    {
        EnemyHitEvent?.Invoke();
    }

    private void TraceState()
    {
        if(!isDie)
        {
            EnemyTraceEvent?.Invoke(Player.transform.position);
            if (Vector3.Distance(Player.transform.position, this.transform.position) <= _enemySO.AttackRange)
            {
                EnemyCurrentState = EnemyState.Attack;
            }
            if (Vector3.Distance(Player.transform.position, this.transform.position) > _enemySO.DetectDistance)
            {
                EnemyCurrentState = EnemyState.Idle;
            }
        }
    }

    private void IdleState()
    {
        EnemyIdleEvent?.Invoke();
        if (!isDie && Vector3.Distance(Player.transform.position, this.transform.position) <= _enemySO.DetectDistance)
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

    public void HitDetect(float _damage)
    {
        EnemyCurrentState = EnemyState.Hit;
    }
}
