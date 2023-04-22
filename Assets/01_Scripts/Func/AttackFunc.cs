using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackFunc : MonoBehaviour
{
    public UnityEvent<bool> AttackEndEvent = null;
    private Animator _animator;

    private readonly int _atkHash = Animator.StringToHash("atkCount");
    private float _timer = 0;

    private bool _canAtk;
    public bool canAtk => _canAtk;
    int _atkCount = 0;
    private bool _setTimer;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    public void Attack()
    {
        if (_canAtk)
        {
            _animator.SetInteger(_atkHash, _atkCount);
            _atkCount++;

            _timer = 0;
            if (_atkCount == 2)
            {
                _atkCount = 0;
            }
            _canAtk = false;
        }
    }

    private void Update()
    {
        if (_setTimer)
        {
            _timer += Time.time;
        }

        if(_timer > 0.7f)
        {
            _canAtk = true;
        }

        if (_timer > 1.5f)
        {
            _setTimer = false;
            _timer = 0;
            _atkCount = 0;
            AttackEndEvent?.Invoke(false);
        }
    }
}
