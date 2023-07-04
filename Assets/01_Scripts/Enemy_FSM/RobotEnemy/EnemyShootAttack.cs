using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private float _Edamage;
    public override void AttackEvent(GameObject firePos)
    {
        RaycastHit hit;
        Debug.DrawRay(firePos.transform.position, transform.forward * 10, Color.green, 0.5f);
        if (Physics.Raycast(firePos.transform.position, transform.forward, out hit, 10))
        {
            if (hit.collider.gameObject.TryGetComponent<HealthFunc>(out HealthFunc hf))
            {
                hf.DamageCalcculate(_Edamage);
            }
        }
    }

}
