using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va gérer la détection avec les autres entités et réagir en conséquence.
public class PlayerManagerNavmesh : MonoBehaviour
{
    private SpawnChildNavmesh _spawner;

    private void Awake()
    {
        _spawner = GetComponent<SpawnChildNavmesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Dans le cas où l'on détecte un Mob, on l'absorbe et le détruit.
        if (other.CompareTag("Mob"))
        {
            ManagerMobMovementNavmesh mob = other.GetComponentInParent<ManagerMobMovementNavmesh>();
            mob.DisableMovement();
            _spawner.SpawnChildEntity();
            Destroy(mob.gameObject);
        }
    }
}
