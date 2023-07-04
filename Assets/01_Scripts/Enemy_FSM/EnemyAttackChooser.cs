using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackChooser : MonoBehaviour
{
    protected GameObject Player;
    protected Animator _animator;
    [SerializeField] protected AnimatorOverrideController _controller;
    protected GameObject _enemyAttackBank;

    protected int _currentBullet;
    protected int _maxBullet;
    protected float _atkCool;
    public bool canAttack;

    public abstract void AttackChoose(float damage);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        Debug.Log(_controller);
        _enemyAttackBank = transform.Find("AttackBank").gameObject;
        canAttack = true;
    }

    public void SetBulletCount(int bc, float cool)
    {
        _maxBullet = _currentBullet = bc;
        _atkCool = cool;
    }
}
