using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 _camOriginalPos;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Error! GameManager has Multyfly Running!!");
        }
        Instance = this;
        DontDestroyOnLoad(this);

    }

    public void ShakeScreen(float duration, float mag)
    {
        StartCoroutine(ShakeScreenCoroutine(duration, mag));
    }

    IEnumerator ShakeScreenCoroutine(float duration, float mag)
    {
        yield return null;
        //float time = 0;
        //_perlin.m_FrequencyGain = mag;
        //while(time <= duration)
        //{
        //    _perlin.m_AmplitudeGain = 1;
            
        //    time += Time.deltaTime;
        //    yield return null;
        //}

        //_perlin.m_AmplitudeGain = 0;
    }
}
