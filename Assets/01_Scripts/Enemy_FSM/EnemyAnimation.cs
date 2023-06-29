using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimationSet(EnemyState currentState)
    {

    }
}
