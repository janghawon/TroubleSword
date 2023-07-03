using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectScript : MonoBehaviour
{
    ParticleSystem _ps;
    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        Destroy(this.gameObject, _ps.duration);
    }
}
