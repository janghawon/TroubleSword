using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackFunc : MonoBehaviour
{
    TrailRenderer _trai;
    [SerializeField] private GameObject _effectPos;
    private MaterialPropertyBlock _material;
    private readonly int _isValueHash = Shader.PropertyToID("_value");
    private readonly int _isAlphaHash = Shader.PropertyToID("_alpha");


    private void Awake()
    {
        _material = new MaterialPropertyBlock();
        _trai = _effectPos.GetComponent<TrailRenderer>();
        _trai.GetPropertyBlock(_material);
        _trai.enabled = false;
    }
    public void OnAttack(float time)
    {
        _trai.enabled = true;
        StartCoroutine(MaterialValueEffect(time));
    }
    public void AttackEnd(float time)
    {
        _trai.enabled = false;
       // StartCoroutine(MaterialAlphaEffect(time));
    }

    //IEnumerator MaterialAlphaEffect(float duration)
    //{

    //}

    IEnumerator MaterialValueEffect(float duration)
    {
        _material.SetFloat(_isValueHash, 0);
        _material.SetFloat(_isAlphaHash, 1);

        float elaspedTime = 0;
        while (elaspedTime < duration)
        {
            float valueAmount = Mathf.Lerp(0f, 1f, elaspedTime / duration * 2);
            _material.SetFloat(_isValueHash, valueAmount);
            _trai.SetPropertyBlock(_material);
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        _trai.enabled = false;
    }

}
