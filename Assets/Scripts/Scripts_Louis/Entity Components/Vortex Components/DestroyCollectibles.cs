using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script doit être attaché à un objet qui possède un collider trigger et va détruire les collectibles.
public class DestroyCollectibles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blueParticle;
    [SerializeField] private ParticleSystem _redParticle;

    private void OnTriggerEnter(Collider other)
    {
        // On ne veut pas détecter la zone de détection des particules.
        if (other.isTrigger)
        {
            return;
        }

        if (other.CompareTag("Blue") || other.CompareTag("Red"))
        {
            DestroyObject(other);
        }
    }

    private void DestroyObject(Collider other)
    {
        if (other.CompareTag("Blue"))
        {
            Instantiate(_blueParticle, other.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_redParticle, other.transform.position, Quaternion.identity);
        }
            

        Destroy(other.gameObject);
    }
}
