using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    NavMeshAgent _na;
    Animator _animator;
    EnemyAttackChooser _aec;

    float currentHP;

    private void Awake()
    {
        _na = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _aec = GetComponent<EnemyAttackChooser>();
    }

    public void SetHP(float _hp)
    {
        currentHP = _hp;
    }

    public void EnemyHitAction(float _damage)
    {
        _na.enabled = false;
        _aec.canAttack = false;
        _animator.SetBool("isHit", true);
        currentHP -= _damage;
        if(currentHP <= 0)
        {
            StartCoroutine(EnemyDieCo());
        }
        else
            StartCoroutine(HitCo());
    }

    IEnumerator EnemyDieCo()
    {

    }

    IEnumerator HitCo()
    {
        yield return new WaitForSeconds(EnemyManager.Instance.RobotEnemyClipList[3].length * 0.7f + 0.1f);
        _animator.SetBool("isHit", false);
        _na.enabled = true;
        _aec.canAttack = true;
    }
}
