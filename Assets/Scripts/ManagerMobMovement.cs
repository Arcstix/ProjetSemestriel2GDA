using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManagerMobMovement : MonoBehaviour
{
    [SerializeField] private MobBaseMovement _baseMovement;
    [SerializeField] private MobFollowPlayer _followMovement;
    [SerializeField] private MobEscapeMovement _escapeMovement;
    [SerializeField] private bool _isEscapeMob;


    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("personnage détecté");
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

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("personnage détecté");
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

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("personnage détecté");
        if (other.CompareTag("Player"))
        {
            _baseMovement.IsBaseState = true;
            _agent.speed = 1f;
        }
    }
}
