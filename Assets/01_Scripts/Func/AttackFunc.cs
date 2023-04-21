using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackFunc : MonoBehaviour
{
    float _atkCount = 3;
    public UnityEvent<float> AttackEvent = null;
    public void Attack()
    {
        switch(_atkCount)
        {
            case 3:
                AttackEvent?.Invoke(_atkCount);
                break;
        }
    }
}
