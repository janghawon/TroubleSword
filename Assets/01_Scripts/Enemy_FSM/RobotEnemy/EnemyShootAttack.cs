using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    LineRenderer _line;
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private float _Edamage;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.enabled = false;
    }

    public override void AttackEvent(GameObject firePos)
    {
        RaycastHit hit;
        if (Physics.Raycast(firePos.transform.position, transform.forward, out hit, 10))
        {
            _line.enabled = true;
            _line.SetPosition(0, firePos.transform.position);
            _line.SetPosition(1, hit.point);
            //StartCoroutine(TrailFade());
            if (hit.collider.gameObject.TryGetComponent<HealthFunc>(out HealthFunc hf))
            {
                hf.DamageCalcculate(_Edamage);
            }
        }
    }

    //IEnumerator TrailFade()
    //{

    //}
}
