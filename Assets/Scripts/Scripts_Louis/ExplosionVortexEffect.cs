using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Ce script attaché à l'explosion du vortex va grossir son collider pour détruire des Vortex
public class ExplosionVortexEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private GameObject _circleWave;
    [SerializeField] private VisualEffect _explosionVisual;

    private float _timer;
    private float _reductionFactor = 2.5f;

    private void Update()
    {
        if(_timer < _lifeTime)
        {
            _timer += Time.deltaTime;
            _collider.radius += _timer * _lifeTime / _explosionVisual.GetFloat("Explosion Size");
            _circleWave.transform.localScale = new Vector3(_collider.radius / _reductionFactor, _collider.radius / _reductionFactor, _collider.radius / _reductionFactor);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
