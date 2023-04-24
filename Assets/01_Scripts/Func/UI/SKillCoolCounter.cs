using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKillCoolCounter : MonoBehaviour
{
    private Image _outLine;
    private Image _filter;
    private Image _skillProfile;
    [SerializeField] private List<Sprite> _imageList = new List<Sprite>();
    private void Awake()
    {
        _outLine = GameObject.Find("SkillOutLine").GetComponent<Image>();
        _filter = GameObject.Find("Filter").GetComponent<Image>();
        _skillProfile = GameObject.Find("SkillProfile").GetComponent<Image>();
    }

    private void Start()
    {
        _outLine.fillAmount = 1f;
        _filter.fillAmount = 0f;
    }

    public void StartSkillCool(float time, int combo)
    {
        _skillProfile.sprite = _imageList[combo];
        StartCoroutine(SkillCoolLineCounting(time));
        StartCoroutine(SkillCoolFilterCounting(time));
    }

    IEnumerator SkillCoolLineCounting(float time)
    {
        float elaspedTime = 0;
        while(elaspedTime < time)
        {
            float fillAmount = Mathf.Lerp(0f, 1f, elaspedTime / time);
            _outLine.fillAmount = fillAmount;
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        _outLine.fillAmount = 1;
    }

    IEnumerator SkillCoolFilterCounting(float time)
    {
        float elaspedTime = 0;
        _filter.fillAmount = 1;
        while (elaspedTime < time)
        {
            float fillAmount = Mathf.Lerp(1f, 0f, elaspedTime / time);
            _filter.fillAmount = fillAmount;
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        _filter.fillAmount = 0;
    }
}
