using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncAnimator : MonoBehaviour
{
    MovementFunc _moveFunc;
    SKillCoolCounter _sKillCoolCounter;
    private Animator _animator;
    private readonly int _moveHash = Animator.StringToHash("MoveValue");
    private readonly int _atkHash = Animator.StringToHash("atkCombo");
    private readonly int _atkBooleanHash = Animator.StringToHash("isAtk");
    private readonly int _dashHash = Animator.StringToHash("canDash");

    private int comboCount;
    private bool canAtk;
    private float _waitTime;
    private bool canDash;

    [SerializeField] private List<AnimationClip> _aniClips = new List<AnimationClip>();
    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _sKillCoolCounter = GameObject.Find("SkillSorting").GetComponent<SKillCoolCounter>();
        _moveFunc = GetComponent<MovementFunc>();
    }

    private void Start()
    {
        canAtk = true;
        canDash = true;
    }

    public void SetMoveAnim(Vector3 value)
    {
        _animator.SetFloat(_moveHash, value.sqrMagnitude);
    }

    public void AttackAnim()
    {
        if(canAtk)
        {
            canAtk = false;
            _moveFunc.canMove = false;
            canDash = false;
            _animator.SetFloat(_moveHash, 0);
            _animator.SetFloat(_atkHash, comboCount);
            _animator.SetBool(_atkBooleanHash, true);

            _waitTime = AttackDuration();
            _sKillCoolCounter.StartSkillCool(_waitTime + 0.1f, comboCount);
            if (comboCount == 2)
            {
                comboCount = 0;
            }
            else
            {
                comboCount++;
            }
            
            StartCoroutine(ComboTimer());
        }
    }

    public void DashAnim()
    {
        if(canDash)
        {
            canDash = false;
            canAtk = false;
            _moveFunc.canMove = false;
            _animator.SetBool(_dashHash, true);
            StartCoroutine(DashTimer());
        }
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(_aniClips[3].length + 0.1f);
        _animator.SetBool(_dashHash, false);
        canAtk = true;
        yield return new WaitForSeconds(3);
        canDash = true;
        _moveFunc.canMove = true;
    }

    IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(_waitTime + 0.1f);
        _animator.SetBool(_atkBooleanHash, false);
        canAtk = true;
        canDash = true;
        _moveFunc.canMove = true;   
    }

    private float AttackDuration()
    {
        float time = 0;

        time = _aniClips[comboCount].length;
        return time;
    }
}
