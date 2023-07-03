using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotEnemyAttackChooser : EnemyAttackChooser
{
    NavMeshAgent _nv;
    [SerializeField] private float grendadeAttackDistance;
    EnemyShootAttack _enemyShootAttack;
    EnemyGrenadeAttack _enemyGrenadeAttack;

    [SerializeField] private GameObject _firePos;

    bool isShoot;

    private void Start()
    {
        _nv = GetComponent<NavMeshAgent>();
        _enemyShootAttack = _enemyAttackBank.GetComponent<EnemyShootAttack>();
        _enemyGrenadeAttack = _enemyAttackBank.GetComponent<EnemyGrenadeAttack>();
    }

    public void StartAttackCool()
    {
        _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[8];
        StartCoroutine(AttackCoolCounter());
    }

    IEnumerator AttackCoolCounter()
    {
        yield return new WaitForSeconds(_atkCool + _controller["Attack"].length);
        canAttack = true;
    }

    public void FireBullet()
    {
        if(_currentBullet > 0)
        {
            if (isShoot)
            {
                _enemyShootAttack.AttackEvent(_firePos);
                _currentBullet--;
            }
            else
            {
                _enemyGrenadeAttack.AttackEvent(_firePos);
                _currentBullet -= 5;
            }
        }
        else
        {
            canAttack = false;
            _animator.SetBool("isReload", true);
        }    
    }

    public void ReloadComplete()
    {
        _currentBullet = _maxBullet;
        _animator.SetBool("isReload", false);
        canAttack = true;
    }

    public override void AttackChoose()
    {
        if(canAttack)
        {
            _nv.enabled = false;
            
            if (Vector3.Distance(transform.position, Player.transform.position) <= grendadeAttackDistance)
            {
                isShoot = false;
                _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[0];
                _enemyShootAttack.EulerEnemyToPlayer(Player.transform);
            }
            else if (Vector3.Distance(transform.position, Player.transform.position) > grendadeAttackDistance)
            {
                isShoot = true;
                _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[1];
                _enemyGrenadeAttack.EulerEnemyToPlayer(Player.transform);
            }
            canAttack = false;
        }
    }
}
