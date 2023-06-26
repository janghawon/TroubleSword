using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalFunc : MonoBehaviour
{
    public bool canTeleport;
    [SerializeField] private float _distance;
    private GameObject _player;
    private FuncAnimator _fa;

    private void Awake()
    {
        _player = GameObject.Find("Player");
        _fa = _player.GetComponent<FuncAnimator>();
    }

    private void Start()
    {
        canTeleport = true;
    }

    private void CheckDistance()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < _distance)
        {
            Vector3 dir = (transform.position - _player.transform.position).normalized;
            dir.y = -dir.y + 0.5f;
            _player.transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.5f);
            //_player.transform.rotation = Quaternion.LookRotation(dir);

            _fa.Roll();
        }
    }

    private void Update()
    {
        if(canTeleport && Input.GetKeyDown(KeyCode.Return))
            CheckDistance();
    }
}
