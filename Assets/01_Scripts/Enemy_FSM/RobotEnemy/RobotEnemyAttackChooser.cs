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

    public override void AttackChoose()
    {
        if(Vector3.Distance(this.transform.position, Player.transform.position) < grendadeAttackDistance)
        {
            _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[1];
            _enemyShootAttack.AttackEvent();
        }
        else
        {
            _controller["Attack"] = EnemyManager.Instance.RobotEnemyClipList[2];
            _enemyGrenadeAttack.AttackEvent();
        }
    }
}
