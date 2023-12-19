using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script doit être attaché à un objet qui possède un collider trigger et va détruire les collectibles.
public class DestroyCollectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // On ne veut pas détecter la zone de détection des particules.
        if (other.isTrigger)
        {
            return;
        }

        if (other.CompareTag("Blue") || other.CompareTag("Red"))
        {
            Destroy(other.gameObject);
        }
    }
}
