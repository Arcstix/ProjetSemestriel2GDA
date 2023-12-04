using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script va servir à gérer les états du Mob.
public class ManagerMobMovementNavmesh : MonoBehaviour
{
    // On référence chaque état du mob qui correspond à un script.
    [SerializeField] private MobBaseMovementNavmesh _baseMovement;
    [SerializeField] private MobFollowPlayerNavmesh _followMovement;
    [SerializeField] private MobEscapeMovementNavmesh _escapeMovement;
    [SerializeField] private bool _isEscapeMob; // Ce booléen temporaire sert juste à définir si notre mob va fuir le joueur ou non.

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Les changements d'état se font dans le cas ou le player est détecté par le mob.
    private void OnTriggerEnter(Collider other)
    {
        // Important de savoir si c'est bien le Player que le mob détecte.
        if (other.CompareTag("Player"))
        {           
            _baseMovement.IsBaseState = false; // Le mob n'est plus dans son état de base.
            _agent.speed = 5f; // On augmente ça vitesse pour marquer un changement de comportement.

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

    // Dans le cas où le player n'est plus détecté le mob reprend son état initial.
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("personnage détecté");
        if (other.CompareTag("Player"))
        {
            _baseMovement.IsBaseState = true;
            _agent.speed = 1f;
        }
    }

    /// <summary>
    /// Désactive le navmesh du mob. 
    /// </summary>
    public void DisableMovement()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
    }
}
