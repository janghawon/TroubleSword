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

    [SerializeField] private float _distance;
    private GameObject _player;
    private MovementInput _moveInput;
    private Camera _cam;

    private void Awake()
    {
        _distance = 2;
        _player = GameObject.Find("Player");
        _moveInput = _player.GetComponent<MovementInput>();
        _cam = Camera.main;
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
    }

    private void CheckDistance()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < _distance && LinkPortal != null
           && _moveInput.canEnterPortal)
        {
            _moveInput.canEnterPortal = false;
            StartCoroutine(_moveInput.EnterPortalCo());
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
