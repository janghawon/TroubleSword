using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    CapsuleCollider _col;
    NavMeshAgent _na;
    Animator _animator;
    EnemyAttackChooser _aec;

    float currentHP;

    private void Awake()
    {
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
        yield return new WaitForSeconds(EnemyManager.Instance.RobotEnemyClipList[3].length * 0.7f + 0.1f);
        _animator.SetBool("isHit", false);
        _na.enabled = true;
        _aec.canAttack = true;
    }
}
