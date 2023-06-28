using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalManager : MonoBehaviour
{
    public CinemachineVirtualCamera cvcam;
    public static PortalManager Instance;
    public PortalFunc CurrentPortal;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!!");
            return;
        }
        Instance = this;
        cvcam = GameObject.Find("PlayerStalker").GetComponent<CinemachineVirtualCamera>();
    }
}
