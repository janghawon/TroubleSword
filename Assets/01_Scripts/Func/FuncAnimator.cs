using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AnimType
{
    Jump = 1,
    Roll = 2,
    Throw = 3
}

public class FuncAnimator : MonoBehaviour
{
    private MovementInput _moveInput;
    [SerializeField] private Animator _animator;

    private readonly int _typeHash = Animator.StringToHash("type");
    private readonly int _moveHash = Animator.StringToHash("isMove");
    private readonly int _blendHash = Animator.StringToHash("blend");

    private AnimType _selectType;

    private void Awake()
    {
        _moveInput = transform.parent.GetComponent<MovementInput>();
    }

    private void Update()
    {
        if(_animator.GetFloat(_blendHash) > 0.2f && _animator.GetBool(_moveHash))
        {
            _animator.SetBool(_moveHash, false);
        }
    }

    public void PublishAnimation(AnimType at)
    {
        _animator.SetFloat(_blendHash, 0);
        Debug.Log(at);
        _animator.SetFloat(_typeHash, (float)at / Enum.GetValues(typeof(AnimType)).Length);
        _animator.SetBool(_moveHash, true);
        _selectType = at;
        switch (at)
        {
            case AnimType.Jump:
                break;
            case AnimType.Roll:
                break;
            case AnimType.Throw:
                {
                    _moveInput.canMove = false;
                }
                break;
        }
    }

    public void LandEnd()
    {
        _moveInput.isRoll = false;
    }

    public void BehaviourEnd()
    {
        //_animator.SetBool(_moveHash, false);
        switch (_selectType)
        {
            case AnimType.Jump:
                break;
            case AnimType.Roll:
                {
                    _moveInput.canMove = true;
                }
                break;
            case AnimType.Throw:
                break;
        }
    }

}
