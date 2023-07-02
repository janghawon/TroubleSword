using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemyAttackChooser : EnemyAttackChooser
{
    [SerializeField] private float grendadeAttackDistance;
    EnemyShootAttack _enemyShootAttack;
    EnemyGrenadeAttack _enemyGrenadeAttack;

    private void Start()
    {
        _enemyShootAttack = _enemyAttackBank.GetComponent<EnemyShootAttack>();
        _enemyGrenadeAttack = _enemyAttackBank.GetComponent<EnemyGrenadeAttack>();
    }

    IEnumerator AttackCoolCounter()
    {
        canAttack = false;
        yield return new WaitForSeconds(_atkCool);
        canAttack = true;
    }

    public override void AttackChoose()
    {
        
        if(canAttack && Vector3.Distance(transform.position, Player.transform.position) < grendadeAttackDistance)
        {
            _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[0];
            _enemyShootAttack.EulerEnemyToPlayer(Player.transform);
            _enemyShootAttack.AttackEvent();
        }
        else
        {
            _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[1];
            _enemyGrenadeAttack.EulerEnemyToPlayer(Player.transform);
            _enemyGrenadeAttack.AttackEvent();
        }
        StartCoroutine(AttackCoolCounter());
    }
}
