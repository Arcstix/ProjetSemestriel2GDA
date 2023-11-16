using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script permet de suivre le Player quand il est proche.
public class MobFollowPlayer : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void FollowPlayer(Vector3 playerPosition)
    {
        if (_agent.enabled)
        {
            _agent.SetDestination(playerPosition);
        }
    }
}
