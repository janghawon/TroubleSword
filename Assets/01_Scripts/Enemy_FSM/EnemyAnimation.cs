using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;
    bool canAnimating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        canAnimating = true;
    }

    public void AnimationSet(EnemyState currentState)
    {
        if(canAnimating)
        {
            if (currentState == EnemyState.Die)
            {
                _animator.SetTrigger("isDie");
                canAnimating = false;
                return;
            }

            if (_currentState != currentState.ToString())
            {
                _animator.SetBool($"is{_currentState}", false);
                _currentState = currentState.ToString();
            }

            _animator.SetBool($"is{_currentState}", true);
        }
    }
}
