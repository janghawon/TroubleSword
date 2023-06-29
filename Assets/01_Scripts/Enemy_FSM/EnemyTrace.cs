using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrace : MonoBehaviour
{
    NavMeshAgent _nav;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    public void EnemyMoveAction(Vector3 targetPos)
    {
        _nav.SetDestination(targetPos);
    }
}
