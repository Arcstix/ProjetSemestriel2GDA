using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script doit �tre attach� � un objet qui poss�de un collider trigger et va d�truire les collectibles.
public class DestroyCollectibles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blueParticle;
    [SerializeField] private ParticleSystem _redParticle;

    private void OnTriggerEnter(Collider other)
    {
        // On ne veut pas d�tecter la zone de d�tection des particules.
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
