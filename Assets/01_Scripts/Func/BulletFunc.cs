using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFunc : MonoBehaviour
{
    [SerializeField] PortalFunc _portal;
    [SerializeField] [Range(1, 10)] private float _firstSpeed;
    [SerializeField] [Range(1, 10)] private float _maxSpeed;

    public bool canfire;

    private void Update()
    {
        if(canfire)
            transform.position += transform.forward * Mathf.Lerp(_firstSpeed, _maxSpeed, 2) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject portal = Instantiate(_portal.gameObject);
        portal.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);

        SetDirection(portal);

        Destroy(this.gameObject);
    }

    void SetDirection(GameObject portal)
    {
        PortalFunc _pf = portal.GetComponent<PortalFunc>();
        GameObject Player = GameObject.Find("Player");
        Vector3 dir = (this.transform.position - Player.transform.position).normalized;

        if(Mathf.Abs(dir.z) > Mathf.Abs(dir.x)) // 앞 뒤 관계성이 더 크다.
        {
            portal.transform.rotation = Quaternion.Euler(0, 90, 0);

            if(dir.z > 0)
            {
                _pf.thisDirection = Direction.front;
            }
            else
            {
                _pf.thisDirection = Direction.back;
            }
        }
        else
        {
            if(dir.x > 0)
            {
                _pf.thisDirection = Direction.right;
            }
            else
            {
                _pf.thisDirection = Direction.left;
            }
        }
    }
}
