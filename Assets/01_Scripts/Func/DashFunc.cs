using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashFunc : MonoBehaviour
{
    readonly int _valueHash = Shader.PropertyToID("_value");

    MaterialPropertyBlock _material;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;

    [SerializeField] private GameObject _dashPos;
    TrailRenderer _trail;
    MovementFunc _moveFunc;
    Vector3 _dashDir;
    bool isDashing;
    float _dashTimeLeft;

    private void Awake()
    {
        _material = new MaterialPropertyBlock();
        _moveFunc = GameObject.Find("Player").GetComponent<MovementFunc>();
        _trail = _dashPos.GetComponent<TrailRenderer>();
        _trail.enabled = false;

        _trail.SetPropertyBlock(_material);
    }

    
    public void Dash()
    {
        _dashDir = _moveFunc.MoveDir.normalized;
        if(_dashDir.sqrMagnitude < 0.1f)
        {
            _dashDir = transform.forward;
        }
        _moveFunc.StopImmediately();
        _dashTimeLeft = _dashDuration;
        isDashing = true;
        _trail.enabled = true;
        _dashPos.gameObject.transform.DOShakePosition(_dashDuration, 1, 5);
        StartCoroutine(DashValueChange());
    }

    IEnumerator DashValueChange()
    {
        _material.SetFloat(_valueHash, 0);

        float elaspedTime = 0;
        while (elaspedTime < _dashDuration)
        {
            float valueAmount = Mathf.Lerp(0f, 1f, elaspedTime / _dashDuration);
            _material.SetFloat(_valueHash, valueAmount);
            _trail.SetPropertyBlock(_material);
            elaspedTime += Time.deltaTime * 0.7f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        _trail.enabled = false;
    }
    
    public void DashEnd()
    {
        StartCoroutine(DashEndCoroutine());
    }

    IEnumerator DashEndCoroutine()
    {
        _moveFunc.StopImmediately();
        _moveFunc.gameObject.transform.position += _dashDir * 1.5f;
        yield return null;
        _moveFunc.canMove = true;
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            _moveFunc.gameObject.transform.position += _dashDir * _dashSpeed * Time.fixedDeltaTime;
            
            _dashTimeLeft -= Time.fixedDeltaTime;

            if(_dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
    }
}
