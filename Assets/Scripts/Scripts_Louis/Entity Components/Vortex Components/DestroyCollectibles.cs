using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script doit être attaché à un objet qui possède un collider trigger et va détruire les collectibles.
public class DestroyCollectibles : MonoBehaviour
{
    public event Action CollectibleDestroy;

    private int _blueAmount = 0;
    private int _redAmount = 0;

    public int BlueAmount { get => _blueAmount; set => _blueAmount = value; }
    public int RedAmount { get => _redAmount; set => _redAmount = value; }

    private void OnTriggerEnter(Collider other)
    {
        // On ne veut pas détecter la zone de détection des particules.
        if (other.isTrigger)
        {
            return;
        }

        // Dans le cas ou l'on est en contact avec un collectible on va chercher son animation de destruction.
        if (other.CompareTag("Blue"))
        {
            _blueAmount++;
            DestroyCollectible(other);
        }
        else if (other.CompareTag("Red"))
        {
            _redAmount++;
            DestroyCollectible(other);
        }
    }

    /// <summary>
    /// Méthode appelé pour détruire le collectible détecté et appelé les évènements lors d'une destruction.
    /// </summary>
    /// <param name="other"></param>
    private void DestroyCollectible(Collider other)
    {
        if (other.TryGetComponent<CollectibleVFX>(out CollectibleVFX collectibleVFX))
        {
            collectibleVFX.PlayVFXDestroy();
            CollectibleDestroy?.Invoke();
        }
    }
}
