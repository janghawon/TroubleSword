using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKillCoolCounter : MonoBehaviour
{
    [SerializeField] private Image[] _outLine;
    [SerializeField] private Image[] _filter;
    [SerializeField] private Image[] _skillProfile;
    [SerializeField] private List<Sprite> _imageList = new List<Sprite>();

    private void Start()
    {
        for(int i = 0; i < _skillProfile.Length; i++)
        {
            _outLine[i].fillAmount = 1f;
            _filter[i].fillAmount = 0f;
        }
    }

    public void StartAttackCool(float time, int combo)
    {
        _skillProfile[0].sprite = _imageList[combo];
        StartCoroutine(AttackCoolLineCounting(time, 0));
        StartCoroutine(AttackCoolFilterCounting(time, 0));
    }

    public void DashAttackCool(float time)
    {
        StartCoroutine(AttackCoolLineCounting(time, 1));
        StartCoroutine(AttackCoolFilterCounting(time, 1));
    }

    public void DimensionSkillCool(float time)
    {
        StartCoroutine(AttackCoolLineCounting(time, 2));
        StartCoroutine(AttackCoolFilterCounting(time, 2));
    }

    IEnumerator AttackCoolLineCounting(float time, int num)
    {
        float elaspedTime = 0;
        while(elaspedTime < time)
        {
            float fillAmount = Mathf.Lerp(0f, 1f, elaspedTime / time);
            _outLine[num].fillAmount = fillAmount;
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        _outLine[num].fillAmount = 1;
    }

    IEnumerator AttackCoolFilterCounting(float time, int num)
    {
        float elaspedTime = 0;
        _filter[num].fillAmount = 1;
        while (elaspedTime < time)
        {
            float fillAmount = Mathf.Lerp(1f, 0f, elaspedTime / time);
            _filter[num].fillAmount = fillAmount;
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        _filter[num].fillAmount = 0;
    }
}
