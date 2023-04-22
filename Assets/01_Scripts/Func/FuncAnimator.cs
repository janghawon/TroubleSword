using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncAnimator : MonoBehaviour
{
    private Animator _animator;
    private readonly int _moveHash = Animator.StringToHash("MoveValue");
    

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    public void SetMoveAnim(Vector3 value)
    {
        _animator.SetFloat(_moveHash, value.sqrMagnitude);
    }

    public void SetAttackAnim(bool value)
    {
        if (value)
            _animator.SetTrigger("atkTrigger");
        else
            _animator.ResetTrigger("atkTrigger");
    }

    
}
