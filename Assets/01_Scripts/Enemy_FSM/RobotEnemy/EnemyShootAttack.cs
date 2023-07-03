using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    [SerializeField] private GameObject _fireEffect;
    public override void AttackEvent(GameObject firePos)
    {
        GameObject effect = Instantiate(_fireEffect);
        effect.transform.position = firePos.transform.position;
    }
}
