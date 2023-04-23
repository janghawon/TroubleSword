using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKillCoolCounter : MonoBehaviour
{
    [SerializeField] private Image _outLine;
    [SerializeField] private Image _filter;

    private void Awake()
    {
        _outLine = GameObject.Find("SkillOutLine").GetComponent<Image>();
        _filter = GameObject.Find("Filter").GetComponent<Image>();
    }

    public void StartSkillCool(float time)
    {
        StartCoroutine(SkillCoolCounting(time));
    }

    IEnumerator SkillCoolCounting(float time)
    {

    }
}
