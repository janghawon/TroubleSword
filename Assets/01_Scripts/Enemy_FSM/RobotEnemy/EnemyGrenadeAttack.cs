using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGrenadeAttack : EnemyAttack
{
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private float _Edamage;
    LineRenderer[] _line = new LineRenderer[3];

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            _line[i] = transform.Find($"LR_{i}").GetComponent<LineRenderer>();
        }
    }

    public override void AttackEvent(GameObject firePos)
    {
        GameObject effect = Instantiate(_fireEffect);
        effect.transform.position = firePos.transform.position;
        Vector3 dir = -thisParentEnemy.transform.forward;
        AttackLogic(firePos);
        thisParentEnemy.transform.DOMove(new Vector3(dir.x, thisParentEnemy.transform.position.y, dir.z), 0.3f);
    }

    private void AttackLogic(GameObject firePos)
    {
        RaycastHit hit;
        for (int i = 0; i < 3; i++)
        {
            Physics.Raycast(firePos.transform.position, transform.forward * (i - 1) * 30, out hit, 10);
            Debug.Log(_line[i]);
            _line[i].enabled = true;
            _line[i].SetPosition(0, firePos.transform.position);
            _line[i].SetPosition(1, hit.point);
            StartCoroutine(TrailFade(i));
            if (hit.collider.gameObject.TryGetComponent<HealthFunc>(out HealthFunc hf))
            {
                hf.DamageCalcculate(_Edamage);
            }
        }
        IEnumerator TrailFade(int count)
        {
            Material _mat = _line[count].material;
            float fadeCount = 0.5f;
            while (fadeCount > 0)
            {
                _mat.color = new Color(1, 0.9f, 0.7f, fadeCount);
                fadeCount -= 0.005f;
                yield return null;
            }
        }

    }
}
