using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackChooser : MonoBehaviour
{
    protected GameObject Player;
    protected AnimatorOverrideController _controller;
    protected GameObject _enemyAttackBank;
    [SerializeField] protected int _atktypeCount;

    protected int _currentBullet;
    protected int _maxBullet;

    public abstract void AttackChoose();

    private void Awake()
    {
        Player = GameObject.Find("Player");
        _controller = GetComponent<AnimatorOverrideController>();
        _enemyAttackBank = transform.Find("AttackBank").gameObject;
    }

    public void SetBulletCount(int bc)
    {
        _maxBullet = _currentBullet = bc;
    }
}
