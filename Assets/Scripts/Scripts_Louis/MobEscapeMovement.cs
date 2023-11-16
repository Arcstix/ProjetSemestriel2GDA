using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script permet au Mob de fuir le Player si il s'approche de lui.
public class MobEscapeMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void EscapeMovement(Vector3 playerPosition)
    {
        // La ligne suivante ne correspond pas au comportement attendu.
        Vector3 escapeDestination = new Vector3(transform.position.x + playerPosition.x, transform.position.y, transform.position.z + playerPosition.z);
        _agent.SetDestination(escapeDestination);
    }
}
