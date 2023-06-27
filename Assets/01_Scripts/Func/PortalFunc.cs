using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalFunc : MonoBehaviour
{
    public PortalFunc LinkPortal;
    public bool canTeleport;
    [SerializeField] private float _distance;
    private GameObject _player;
    private FuncAnimator _fa;

    private void Awake()
    {
        _distance = 2;
        _player = GameObject.Find("Player");
        _fa = _player.GetComponent<FuncAnimator>();
    }

    private void Start()
    {
        LinkPortal = PortalManager.Instance.CurrentPortal;

        PortalManager.Instance.CurrentPortal = this;
        canTeleport = true;
    }

    private void CheckDistance()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < _distance)
        {
            Vector3 dir = (transform.position - _player.transform.position).normalized;
            dir.y = -dir.y;
            _player.transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.5f);
            //_player.transform.rotation = Quaternion.LookRotation(dir);

            _fa.Roll();
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.5f);
        _player.transform.position = LinkPortal.transform.position;
    }

    private void Update()
    {
        if(canTeleport && Input.GetKeyDown(KeyCode.Return))
            CheckDistance();
    }
}
