using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va gérer la détection avec les autres entités et réagir en conséquence.
public class PlayerManager : MonoBehaviour
{
    private SpawnChild _spawner;

    private void Awake()
    {
        _spawner = GetComponent<SpawnChild>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Dans le cas où l'on détecte un Mob, on l'absorbe et le détruit.
        if (other.CompareTag("Mob"))
        {
            ManagerMobMovement mob = other.GetComponentInParent<ManagerMobMovement>();
            mob.DisableMovement();
            _spawner.SpawnChildEntity();
            Destroy(mob.gameObject);
        }
    }
}
