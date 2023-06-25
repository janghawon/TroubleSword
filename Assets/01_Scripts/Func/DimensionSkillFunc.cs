using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSkillFunc : MonoBehaviour
{
    [SerializeField] private GameObject _dimenBulletPrefab;

    public void ShootSkill()
    {
        Debug.Log(this.transform.forward);
    }
}
