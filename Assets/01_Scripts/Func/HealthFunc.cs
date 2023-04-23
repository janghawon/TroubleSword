using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFunc : MonoBehaviour
{
    Slider _hpBar;
    [SerializeField] private float _maxHP;
    [SerializeField] private float _currentHP;

    private void Awake()
    {
        _currentHP = _maxHP;
        _hpBar = GameObject.Find("HPBar").GetComponent<Slider>();
    }
    
    void HpCalculate()
    {
        _hpBar.value = Mathf.Lerp(_hpBar.value, (float)_currentHP / (float)_maxHP, Time.deltaTime * 10);
    }

    void SampleDamage()
    {
        _currentHP -= 10;
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            SampleDamage();
        }
        HpCalculate();
    }
}
