using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    public void SetMoveAnim(Vector3 value)
    {
        _animator.SetFloat("MoveValue", value.sqrMagnitude);
    }
}
