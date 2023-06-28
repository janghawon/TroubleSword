using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalFunc : MonoBehaviour
{
    MovementFunc _moveFunc;
    public PortalFunc LinkPortal;
    public bool canTeleport;
    [SerializeField] private float _distance;
    private GameObject _player;
    private FuncAnimator _fa;

    private void Awake()
    {
        _distance = 2;
        _player = GameObject.Find("Player");
        _moveFunc = _player.GetComponent<MovementFunc>();
        _fa = _player.GetComponent<FuncAnimator>();
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
        canTeleport = true;
    }

    private void CheckDistance()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < _distance)
        {
            Vector3 dir = (transform.position - _player.transform.position).normalized;
            dir.y = -dir.y;
            _player.transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.5f);

            _fa.PortalJump();
            StartCoroutine(Teleport());
        }
    }


    IEnumerator Teleport()
    {
        _moveFunc.canMove = false;
        yield return new WaitForSeconds(0.63f);
        _player.transform.position = LinkPortal.transform.position + new Vector3(0, -0.7f, 0);
        Vector3 dir = new Vector3(0 -_player.transform.rotation.y, 0);
        Debug.Log(dir);
        if(dir.x < 0)
        {
            dir.x = -dir.x;
        }
        Debug.Log(dir);
        _player.transform.rotation = Quaternion.LookRotation(dir);
        yield return new WaitForSeconds(0.1f);
        _fa.PortalRoll();
    }

    private void Update()
    {
        if(canTeleport && Input.GetKeyDown(KeyCode.Return))
            CheckDistance();
    }
}
