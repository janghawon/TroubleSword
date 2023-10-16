using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum Direction
{
    front,
    back,
    right, 
    left
}

public class PortalFunc : MonoBehaviour
{
    public Direction thisDirection;
    public PortalFunc LinkPortal;
    public bool canTeleport
    {
        get
        {
            return _canTeleport;
        }
        set
        {
            _canTeleport = value;
            if(!_canTeleport)
                StartCoroutine(TeleportCool());
        }
    }

    IEnumerator TeleportCool()
    {
        yield return new WaitForSeconds(2);
        _canTeleport = true;
    }

    private bool _canTeleport;
    [SerializeField] private float _distance;
    private GameObject _player;

    private void Awake()
    {
        _distance = 2;
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        if(PortalManager.Instance.CurrentPortal != null)
        {
            LinkPortal = PortalManager.Instance.CurrentPortal;
            if(PortalManager.Instance.CurrentPortal.LinkPortal != null)
                Destroy(PortalManager.Instance.CurrentPortal.LinkPortal.gameObject);
            PortalManager.Instance.CurrentPortal.LinkPortal = this;
        }

        PortalManager.Instance.CurrentPortal = this;
        
        if(LinkPortal != null)
        {
            _canTeleport = true;
            LinkPortal.canTeleport = true;
        }
    }

    private void CheckDistance()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < _distance && _canTeleport)
        {
            canTeleport = false;
            LinkPortal.canTeleport = false;

            Vector3 dir = (transform.position - _player.transform.position).normalized;
            dir.y = -dir.y;
            _player.transform.position = LinkPortal.transform.position + new Vector3(0, -0.7f, 0);
            _player.transform.rotation = Quaternion.LookRotation(SetDir());
        }
    }

    Vector3 SetDir()
    {
        switch(LinkPortal.thisDirection)
        {
            case Direction.front:
                return Vector3.back;
            case Direction.back:
                return Vector3.forward;
            case Direction.right:
                return Vector3.left;
            case Direction.left:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }

    private void Update()
    {
        CheckDistance();
    }
}
