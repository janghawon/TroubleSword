using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncAnimator : MonoBehaviour
{
    SKillCoolCounter _sKillCoolCounter;
    private Animator _animator;
    private readonly int _moveHash = Animator.StringToHash("MoveValue");
    private readonly int _atkHash = Animator.StringToHash("atkCombo");
    private readonly int _atkBooleanHash = Animator.StringToHash("isAtk");

    private int comboCount;
    private bool canAtk;
    private float _waitTime;

    RuntimeAnimatorController ac;
    [SerializeField] private List<AnimationClip> _atkClips = new List<AnimationClip>();
    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        ac = _animator.runtimeAnimatorController;
        _sKillCoolCounter = GameObject.Find("SkillSorting").GetComponent<SKillCoolCounter>();
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

    IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(_waitTime + 0.1f);
        _animator.SetBool(_atkBooleanHash, false);
        canAtk = true;
    }

    private float AttackDuration()
    {
        float time = 0;

        time = _atkClips[comboCount].length;
        return time;
    }
}
