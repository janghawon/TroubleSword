using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGrenadeAttack : EnemyAttack
{
    [SerializeField] private GameObject _fireEffect;
    public override void AttackEvent(GameObject firePos)
    {
        GameObject effect = Instantiate(_fireEffect);
        effect.transform.position = firePos.transform.position;

        thisParentEnemy.transform.DOMove(Vector3.back * 0.3f, 0.2f);
    }
}
