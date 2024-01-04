using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script doit �tre attach� � un objet qui poss�de un collider trigger et va d�truire les collectibles.
public class DestroyCollectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // On ne veut pas d�tecter la zone de d�tection des particules.
        if (other.isTrigger)
        {
            return;
        }

        // Dans le cas ou l'on est en contact avec un collectible on va chercher son animation de destruction.
        if (other.CompareTag("Blue") || other.CompareTag("Red"))
        {
            if(other.TryGetComponent<CollectibleVFX>(out CollectibleVFX collectibleVFX))
            {
                collectibleVFX.PlayVFXDestroy();
            }
        }
    }
}
