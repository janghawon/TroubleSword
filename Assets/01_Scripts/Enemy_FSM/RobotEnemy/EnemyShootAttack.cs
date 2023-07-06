using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    LineRenderer[] _line = new LineRenderer[3];
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private GameObject _effectPos;
    [SerializeField] private float _Edamage;

    int count;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            _line[i] = transform.Find($"LR_{i}").GetComponent<LineRenderer>();
            _line[i].enabled = false;
        }
    }

    public override void AttackEvent(GameObject firePos)
    {
        GameObject effect = Instantiate(_fireEffect);
        effect.transform.position = _effectPos.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(firePos.transform.position, transform.forward, out hit, 10))
        {
            _line[count].enabled = true;
            _line[count].SetPosition(0, firePos.transform.position);
            _line[count].SetPosition(1, hit.point);
            StartCoroutine(TrailFade());
            if (hit.collider.gameObject.TryGetComponent<HealthFunc>(out HealthFunc hf))
            {
                hf.DamageCalcculate(_Edamage);
            }
        }

        count++;
        if(count == 3)
        {
            count = 0;
        }
    }

    IEnumerator TrailFade()
    {
        Material _mat = _line[count].material;
        float fadeCount = 0.5f;
        while(fadeCount > 0)
        {
            _mat.color = new Color(1, 0.9f, 0.7f, fadeCount);
            fadeCount -= 0.01f;
            yield return null;
        }
    }
}
