using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    public void EulerEnemyToPlayer(Transform _pTrans)
    {
        Vector3 dir = _pTrans.transform.position - transform.position;
        float yRot = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Debug.Log(1);
        transform.Rotate(new Vector3(0, yRot, 0));
    }

    public abstract void AttackEvent();
}
