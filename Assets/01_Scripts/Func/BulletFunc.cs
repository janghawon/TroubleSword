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
        Destroy(this.gameObject);
    }
}
