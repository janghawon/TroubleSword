using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InputFunc : MonoBehaviour
{
    float _atkCount;
    
    Vector3 _dirInput;
    public UnityEvent<Vector3> OnMoveKeyPress = null;
    public UnityEvent OnAttackPress = null;

    void UpdateMoveInput()
    {
        float XInput = Input.GetAxis("Horizontal");
        float ZInput = Input.GetAxis("Vertical");

        _dirInput = new Vector3(XInput, 0, ZInput);
        _dirInput.Normalize();
        OnMoveKeyPress?.Invoke(_dirInput);
    }

    void UpdateAttackInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnAttackPress?.Invoke();
        }
    }

    void Update()
    {
        UpdateMoveInput();
        UpdateAttackInput();
    }
}
