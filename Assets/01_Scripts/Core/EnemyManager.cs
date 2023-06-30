using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public List<AnimationClip> RobotEnemyClipList = new List<AnimationClip>();
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!");
            return;
        }
        Instance = this;
    }

    
}
