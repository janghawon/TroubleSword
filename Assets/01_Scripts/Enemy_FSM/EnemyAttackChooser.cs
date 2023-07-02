using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackChooser : MonoBehaviour
{
    protected GameObject Player;
    [SerializeField] protected AnimatorOverrideController _controller;
    protected GameObject _enemyAttackBank;

    protected int _currentBullet;
    protected int _maxBullet;
    protected float _atkCool;
    protected bool canAttack;

    public abstract void AttackChoose();

    private void Awake()
    {
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
