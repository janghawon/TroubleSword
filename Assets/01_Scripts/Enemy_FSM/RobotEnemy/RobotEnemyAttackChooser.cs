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

    public void StartAttackCool()
    {
        StartCoroutine(AttackCoolCounter());
    }

    IEnumerator AttackCoolCounter()
    {
        yield return new WaitForSeconds(_atkCool);
        canAttack = true;
    }

    public override void AttackChoose()
    {
        if(canAttack)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) <= grendadeAttackDistance)
            {
                _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[0];
                _enemyShootAttack.EulerEnemyToPlayer(Player.transform);
                _enemyShootAttack.AttackEvent();
            }
            else if (Vector3.Distance(transform.position, Player.transform.position) > grendadeAttackDistance)
            {
                _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[1];
                _enemyGrenadeAttack.EulerEnemyToPlayer(Player.transform);
                _enemyGrenadeAttack.AttackEvent();
            }
            canAttack = false;
        }
    }
}
