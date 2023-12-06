using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectibleMovement))]
public class Detection : MonoBehaviour
{
    private CollectibleMovement _collectibleMovement;

    private void Awake()
    {
        _collectibleMovement = GetComponent<CollectibleMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie que ce que l'on détecte est bien une entité (Player ou Mobs)
        if (other.CompareTag("Entity"))
        {
            Transform target = other.GetComponent<Transform>();
            _collectibleMovement.AddPotentialTarget(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            Transform target = other.GetComponent<Transform>();
            _collectibleMovement.RemovePotentialTarget(target);
        }
    }
}
