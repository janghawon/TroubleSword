using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void DieEvent()
    {

    }

    public void AnimationSet(EnemyState currentState)
    {
        if(currentState == EnemyState.Die)
        {
            _animator.SetTrigger("isDie");
            return;
        }

        if(_currentState != currentState.ToString())
        {
            _animator.SetBool($"is{_currentState}", false);
            _currentState = currentState.ToString();
        }

        _animator.SetBool($"is{_currentState}", true);
    }
}
