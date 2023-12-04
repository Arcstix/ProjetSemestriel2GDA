using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script va servir � g�rer les �tats du Mob.
public class ManagerMobMovementNavmesh : MonoBehaviour
{
    // On r�f�rence chaque �tat du mob qui correspond � un script.
    [SerializeField] private MobBaseMovementNavmesh _baseMovement;
    [SerializeField] private MobFollowPlayerNavmesh _followMovement;
    [SerializeField] private MobEscapeMovementNavmesh _escapeMovement;
    [SerializeField] private bool _isEscapeMob; // Ce bool�en temporaire sert juste � d�finir si notre mob va fuir le joueur ou non.

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Les changements d'�tat se font dans le cas ou le player est d�tect� par le mob.
    private void OnTriggerEnter(Collider other)
    {
        // Important de savoir si c'est bien le Player que le mob d�tecte.
        if (other.CompareTag("Player"))
        {           
            _baseMovement.IsBaseState = false; // Le mob n'est plus dans son �tat de base.
            _agent.speed = 5f; // On augmente �a vitesse pour marquer un changement de comportement.

            if (_isEscapeMob)
            {
                _escapeMovement.EscapeMovement(other.transform.position);
            }
            else
            {
                _followMovement.FollowPlayer(other.transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _baseMovement.IsBaseState = false;
            _agent.speed = 5f;
            if (_isEscapeMob)
            {
                _escapeMovement.EscapeMovement(other.transform.position);
            }
            else
            {
                _followMovement.FollowPlayer(other.transform.position);
            }
        }
    }

    // Dans le cas o� le player n'est plus d�tect� le mob reprend son �tat initial.
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("personnage d�tect�");
        if (other.CompareTag("Player"))
        {
            _baseMovement.IsBaseState = true;
            _agent.speed = 1f;
        }
    }

    /// <summary>
    /// D�sactive le navmesh du mob. 
    /// </summary>
    public void DisableMovement()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
    }
}
