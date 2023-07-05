using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGrenadeAttack : EnemyAttack
{
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private float _Edamage;
    LineRenderer _line;

    private void Start()
    {
        _line = GetComponentInChildren<LineRenderer>();
    }

    public override void AttackEvent(GameObject firePos)
    {
        GameObject effect = Instantiate(_fireEffect);
        effect.transform.position = firePos.transform.position;
        Vector3 dir = transform.position +  -thisParentEnemy.transform.forward;
        AttackLogic(firePos);
        thisParentEnemy.transform.DOMove(new Vector3(dir.x, thisParentEnemy.transform.position.y, dir.z), 0.3f);
    }

    private void AttackLogic(GameObject firePos)
    {
        RaycastHit hit;
        Physics.Raycast(firePos.transform.position, transform.forward, out hit, 10);
        if (hit.collider.gameObject.TryGetComponent<HealthFunc>(out HealthFunc hf))
        {
            hf.DamageCalcculate(_Edamage);
        }
        _line.enabled = true;
        _line.startWidth = 0.2f;
        _line.endWidth = 0.2f;
        _line.SetPosition(0, firePos.transform.position);
        _line.SetPosition(1, hit.point);
        StartCoroutine(TrailFade());
    }
    IEnumerator TrailFade()
    {
        Material _mat = _line.material;
        float fadeCount = 0.7f;
        while (fadeCount > 0)
        {
            _mat.color = new Color(1, 0, 0f, fadeCount);
            fadeCount -= 0.005f;
            yield return null;
        }
        _line.startWidth = 0.07f;
        _line.endWidth = 0.07f;
        _line.enabled = false;
    }
}
