using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyParticle;

    private void OnDestroy()
    {
        Instantiate(_destroyParticle, transform.position, Quaternion.identity);
    }
}
