using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSkillFunc : MonoBehaviour
{
    [SerializeField] private GameObject _dimenBulletPrefab;

    MovementInput mi;

    private void Awake()
    {
        mi = transform.parent.GetComponent<MovementInput>();
    }

    public void ShootSkill()
    {
        if (!mi.canEnterPortal) return;
        mi.canMove = true;
        GameObject bullet = Instantiate(_dimenBulletPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        BulletFunc bf = bullet.GetComponent<BulletFunc>();

        bullet.transform.rotation = this.transform.rotation;
        bf.canfire = true;
    }
}
