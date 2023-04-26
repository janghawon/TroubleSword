using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackEffect : MonoBehaviour
{
    ParticleSystem _particleModule;

    private void Awake()
    {
        _particleModule = GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(EndEffect());
    }
    IEnumerator EndEffect()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    
}
