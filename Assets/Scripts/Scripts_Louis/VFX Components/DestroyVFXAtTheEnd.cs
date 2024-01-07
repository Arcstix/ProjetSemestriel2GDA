using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyVFXAtTheEnd : MonoBehaviour
{
    [SerializeField] private VisualEffect _visualEffect;

    private float _timeBeforeCheck = 1f;
    private float _timer;

    private void Update()
    {
        if(_timeBeforeCheck > _timer)
        {
            _timer += Time.deltaTime;
            return;
        }
        else
        {
            if (_visualEffect.aliveParticleCount == 0)
            {
                Destroy(gameObject);
            }
        }       
    }
}
