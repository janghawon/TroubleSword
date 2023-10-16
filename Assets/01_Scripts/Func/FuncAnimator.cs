using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncAnimator : MonoBehaviour
{
    private Transform _playerTrans;
    [SerializeField] private Animator _animator;
   
    private readonly int _jumpHash = Animator.StringToHash("isJump");
    private readonly int _rollHash = Animator.StringToHash("isRoll");

    [SerializeField] private float _rollAmount;

    private void Awake()
    {
        _playerTrans = transform.parent;
    }

    public void PortalRoll()
    {
        _animator.SetBool(_rollHash, true);
    }

    public void RollEnd()
    {
        _animator.SetBool(_rollHash, false);
    }

    public void PortalJump()
    {
        _animator.SetBool(_jumpHash, true);
    }

    public void JumpEnd()
    {
        _animator.SetBool(_jumpHash, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

}
