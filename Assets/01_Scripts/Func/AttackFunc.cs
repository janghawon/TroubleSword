using System.Collections;
using UnityEngine;
using UnityEditor;

public class AttackFunc : MonoBehaviour
{
    TrailRenderer _trai;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _effectPos;
    [SerializeField] private GameObject _player;
    private MaterialPropertyBlock _material;
    private readonly int _isValueHash = Shader.PropertyToID("_value");

    Light _effectLight;

    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [SerializeField] private GameObject _feedbackPrefab;

    [SerializeField] [Range(0f, 10f)] private float _atkRange;
    [SerializeField] [Range(0, 359f)] private float _atkAngle;
    private void Awake()
    {
        _material = new MaterialPropertyBlock();
        _effectLight = GameObject.Find("EffectLight").GetComponent<Light>();
        _trai = _effectPos.GetComponent<TrailRenderer>();
        _trai.GetPropertyBlock(_material);
        _trai.enabled = false;
        _effectLight.enabled = false;
    }

    public void OnAttack(float time, int count)
    {
        switch (count)
        {
            case 1:
                _atkRange = 1.4f;
                _atkAngle = 92f;
                break;
            case 2:
                _atkRange = 2.15f;
                _atkAngle = 70f;
                break;
            case 0:
                _atkRange = 1.2f;
                _atkAngle = 82f;
                break;
            default:
                break;
        }

        _trai.enabled = true;
        _effectLight.enabled = true;
        StartCoroutine(MaterialValueEffect(time));
    }

    public void Attack(int count)
    {
        

        Collider[] hitColliders = Physics.OverlapSphere(_player.transform.position, _atkRange);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                Transform target = hitColliders[i].transform;
                Vector3 dirtoTarget = (target.position - _player.transform.position).normalized;

                if (Vector3.Angle(_player.transform.forward, dirtoTarget) < _atkAngle / 2)
                {
                    GameObject feedbackEff = Instantiate(_feedbackPrefab);
                    feedbackEff.transform.position = hitColliders[i].gameObject.transform.position;

                    TimeController.Instance.ModifyTimeScale(0.2f, 0.1f, () =>
                    {
                        TimeController.Instance.ModifyTimeScale(1, 0.02f, null);
                    });
                    GameManager.Instance.ShakeScreen(0.1f, 3f);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_player.transform.position, _atkRange);

        Handles.color = Color.red;
        Handles.DrawSolidArc(_player.transform.position, Vector3.up, transform.forward, _atkAngle / 2, _atkRange);
        Handles.DrawSolidArc(_player.transform.position, Vector3.up, transform.forward, -_atkAngle / 2, _atkRange);
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
