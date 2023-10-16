using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSkillFunc : MonoBehaviour
{
    [SerializeField] private GameObject _dimenBulletPrefab;

    
    public void ShootSkill()
    {
        GameObject bullet = Instantiate(_dimenBulletPrefab);
        bullet.transform.position = this.transform.position;
        bullet.transform.position += new Vector3(0, 1, 0);
        BulletFunc bf = bullet.GetComponent<BulletFunc>();

        bullet.transform.rotation = this.transform.rotation;
        bf.canfire = true;
    }
}
