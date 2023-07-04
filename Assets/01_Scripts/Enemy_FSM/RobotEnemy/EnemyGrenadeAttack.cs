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
        Vector3 dir = -thisParentEnemy.transform.forward;
        thisParentEnemy.transform.DOMove(new Vector3(dir.x, thisParentEnemy.transform.position.y, dir.z), 0.3f);
        Debug.Log(new Vector3(dir.x, thisParentEnemy.transform.position.y, dir.z));
    }
}
