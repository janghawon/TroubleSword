using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncAnimator : MonoBehaviour
{
    private Animator _animator;
    private readonly int _moveHash = Animator.StringToHash("MoveValue");
    private readonly int _atkhash = Animator.StringToHash("atkCombo");
    private readonly int _atkTriggerhash = Animator.StringToHash("atkTrigger");

    private int comboCount;
    private bool canAtk;
    private float _waitTime;

    RuntimeAnimatorController ac;
    [SerializeField] private List<AnimationClip> _atkClips = new List<AnimationClip>();
    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        ac = _animator.runtimeAnimatorController;
    }

    private void Start()
    {
        canAtk = true;
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
            _animator.SetFloat(_atkhash, comboCount);
            _animator.SetTrigger(_atkTriggerhash);

            _waitTime = AttackDuration();
            Debug.Log(_waitTime);
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

    IEnumerator ComboTimer()
    {
        yield return null;
        _animator.ResetTrigger(_atkTriggerhash);
        yield return new WaitForSeconds(_waitTime);
        canAtk = true;
    }

    private float AttackDuration()
    {
        float time = 0;

        time = _atkClips[comboCount].length;
        return time;
    }
}
