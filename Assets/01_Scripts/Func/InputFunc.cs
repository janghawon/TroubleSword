using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InputFunc : MonoBehaviour
{
    Vector3 _dirInput;
    public UnityEvent<Vector3> OnMoveKeyPress = null;
    public UnityEvent OnAttackPress = null;
    public UnityEvent<Vector3> OnDashPress = null;
    public UnityEvent OnDimensionPress = null;

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

    void UpdateDashInput()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnDashPress?.Invoke(_dirInput);
        }
    }

    void UpdateDimenInput()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnDimensionPress?.Invoke();
        }
    }

    void Update()
    {
        UpdateMoveInput();
        UpdateAttackInput();
        UpdateDashInput();
        UpdateDimenInput();
    }
}
