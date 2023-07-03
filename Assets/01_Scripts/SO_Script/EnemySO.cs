using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemySO/GroundEnemy")]
public class EnemySO : ScriptableObject
{
    public float EnemyHP;
    public int bulletCount;
    public string EnemyName;
    public float AttackValue;
    public float AttackSpeedValue;
    public float AttackRange;
    public float DetectDistance;
    public float MoveSpeed;
    public float grogiTime;
}
