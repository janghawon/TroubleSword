using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
}
