using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class EnemyAttack : MonoBehaviour
{
    GameObject thisParentEnemy;

    private void Awake()
    {
        thisParentEnemy = transform.parent.gameObject;
    }

    public void EulerEnemyToPlayer(Transform _pTrans)
    {
        Vector3 dir = _pTrans.transform.position - thisParentEnemy.transform.position;
        float yRot = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        thisParentEnemy.transform.DORotate(new Vector3(0, yRot, 0), 0.5f);
    }

    public abstract void AttackEvent();
}
