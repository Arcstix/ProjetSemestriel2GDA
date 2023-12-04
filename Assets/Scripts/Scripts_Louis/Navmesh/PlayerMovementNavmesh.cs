using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script permet de bouger le Player grâce au NavMesh.
public class PlayerMovementNavmesh : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundLayer;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryToMove();
        }
    }

    // Cette méthode permet de tirer un Raycast la ou la souris à cliqué.
    private void TryToMove()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        // Si on touche le sol alors on peut définir la destination du Player.
        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _groundLayer))
        {
            _agent.SetDestination(hitInfo.point);
        }
    }
}
