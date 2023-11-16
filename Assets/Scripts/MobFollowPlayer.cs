using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobFollowPlayer : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void FollowPlayer(Vector3 playerPosition)
    {
        _agent.SetDestination(playerPosition);
    }
}
