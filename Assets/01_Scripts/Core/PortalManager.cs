using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalManager : MonoBehaviour
{
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
    }

    public void ClearPortal()
    {
        Destroy(CurrentPortal.LinkPortal.gameObject, 1);
        Destroy(CurrentPortal.gameObject, 1);
    }
}
