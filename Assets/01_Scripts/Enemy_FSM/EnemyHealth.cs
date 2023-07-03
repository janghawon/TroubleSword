using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    EnemyBrain _eb;
    CapsuleCollider _col;
    NavMeshAgent _na;
    Animator _animator;
    EnemyAttackChooser _aec;

    float currentHP;

    private void Awake()
    {
        _eb = GetComponent<EnemyBrain>();
        _col = GetComponent<CapsuleCollider>();
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
            Die();
        }
        else
            StartCoroutine(HitCo());
    }

    void Die()
    {
        _animator.SetBool("isHit", false);
        _animator.SetTrigger("isDie");
        _col.enabled = false;
        _na.enabled = false;
        _aec.canAttack = false;
    }

    public void StopDie()
    {
        
    }

    IEnumerator HitCo()
    {
        _eb.EnemyCurrentState = EnemyState.Hit;
        _animator.SetBool("isAttack", false);
        _animator.SetBool("isReload", false);
        yield return new WaitForSeconds(EnemyManager.Instance.RobotEnemyClipList[3].length);
        _animator.SetBool("isHit", false);
        _na.enabled = true;
        _aec.canAttack = true;
        _eb.EnemyCurrentState = EnemyState.Idle;
    }
}
