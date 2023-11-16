using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBaseMovement : MonoBehaviour
{
    [SerializeField] private float _radiusOfTheCircleForRandomDestination = 20f;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _destination;
    private bool _isBaseState = true;

    public bool IsBaseState { get => _isBaseState; set => _isBaseState = value; }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isBaseState)
        {
            ContinueMovement();
        }        
    }

    // Destination qui change à chaque fois que le mob arrive à destination
    private void ContinueMovement()
    {
        if (_navMeshAgent.remainingDistance == 0)
        {
            SetNewDestination();
        }
    }

    // On définit une nouvelle destination aléatoire.
    public void SetNewDestination()
    {
        _destination = UnityEngine.Random.insideUnitCircle * (_radiusOfTheCircleForRandomDestination / 2);
        _destination.z = transform.position.z + _destination.y;
        _destination.x = transform.position.x + _destination.x;
        _destination.y = 0;
        _navMeshAgent.SetDestination(_destination);
    }
}
