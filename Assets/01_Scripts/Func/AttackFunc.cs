using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackFunc : MonoBehaviour
{
    TrailRenderer _trai;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _effectPos;
    [SerializeField] private GameObject _rayAnchor;
    private MaterialPropertyBlock _material;
    private readonly int _isValueHash = Shader.PropertyToID("_value");
    private readonly int _isAlphaHash = Shader.PropertyToID("_alpha");

    Light _effectLight;

    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [SerializeField] private GameObject _feedbackPrefab;
    private void Awake()
    {
        _material = new MaterialPropertyBlock();
        _effectLight = GameObject.Find("EffectLight").GetComponent<Light>();
        _trai = _effectPos.GetComponent<TrailRenderer>();
        _trai.GetPropertyBlock(_material);
        _trai.enabled = false;
        _effectLight.enabled = false;
    }

    //private void Start()
    //{
    //    _startColor = _material.GetColor("_StartColor");
    //    _endColor = _material.GetColor("_EndColor");
    //}

    public void OnAttack(float time)
    {
        _trai.enabled = true;
        _effectLight.enabled = true;
        StartCoroutine(MaterialValueEffect(time));
    }

    

    public void Attack()
    {
        Ray ray = new Ray(_rayAnchor.transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2, _layerMask, QueryTriggerInteraction.Ignore))
        {
            TimeController.Instance.ModifyTimeScale(0.2f, 0.1f, () =>
            {
                TimeController.Instance.ModifyTimeScale(1, 0.02f, null);
                Time.timeScale = 1;
            });
            GameManager.Instance.ShakeScreen(0.1f, 3f);

            GameObject feedbackEff = Instantiate(_feedbackPrefab);
            feedbackEff.transform.position = hit.point;
        }
    }

    private void Update()
    {
        Debug.DrawRay(_rayAnchor.transform.position, transform.forward * 2, Color.green);
    }

    IEnumerator MaterialValueEffect(float duration)
    {
        _material.SetFloat(_isValueHash, 0);

        float elaspedTime = 0;
        while (elaspedTime < duration)
        {
            float valueAmount = Mathf.Lerp(0f, 1f, elaspedTime / duration * 2);
            _material.SetFloat(_isValueHash, valueAmount);
            _trai.SetPropertyBlock(_material);

            _effectLight.color = Color.Lerp(_startColor, _endColor, elaspedTime / duration * 2);

            elaspedTime += Time.deltaTime;
            yield return null;
        }

        _trai.enabled = false;
        _effectLight.enabled = false;
    }

}
