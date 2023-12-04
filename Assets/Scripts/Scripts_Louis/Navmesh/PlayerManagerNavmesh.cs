using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va g�rer la d�tection avec les autres entit�s et r�agir en cons�quence.
public class PlayerManagerNavmesh : MonoBehaviour
{
    private SpawnChildNavmesh _spawner;

    private void Awake()
    {
        _spawner = GetComponent<SpawnChildNavmesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Dans le cas o� l'on d�tecte un Mob, on l'absorbe et le d�truit.
        if (other.CompareTag("Mob"))
        {
            ManagerMobMovementNavmesh mob = other.GetComponentInParent<ManagerMobMovementNavmesh>();
            mob.DisableMovement();
            _spawner.SpawnChildEntity();
            Destroy(mob.gameObject);
        }
    }
}
