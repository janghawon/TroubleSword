using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private Vector3 _camOriginalPos;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Error! GameManager has Multyfly Running!!");
        }
        Instance = this;
        DontDestroyOnLoad(this);

        _camera = GameObject.Find("PlayerStalker").GetComponent<CinemachineVirtualCamera>();
        _perlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeScreen(float duration, float mag)
    {
        StartCoroutine(ShakeScreenCoroutine(duration, mag));
    }

    IEnumerator ShakeScreenCoroutine(float duration, float mag)
    {
        float time = 0;
        _perlin.m_FrequencyGain = mag;
        while(time <= duration)
        {
            _perlin.m_AmplitudeGain = 1;
            
            time += Time.deltaTime;
            yield return null;
        }

        _perlin.m_AmplitudeGain = 0;
    }
}
