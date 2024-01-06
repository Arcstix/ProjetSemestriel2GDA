using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Ce script attaché à un vortex va le charger à chaque fois qu'il détruit un collectible et son apparence va changer.
public class VortexCharge : MonoBehaviour
{
    [SerializeField] private VisualEffect _vortexVisual;
    [SerializeField] private DestroyCollectibles _destroyCollectibles;
    [SerializeField] private GameObject _explosionPrefab;

    [SerializeField] private int _numberForSurcharge = 20;
    [SerializeField] private int _gainParticleEachCharge = 20;
    [SerializeField] private Vector3 _gainSizeEachCharge = new Vector3(0.01f, 0.01f, 0.01f);

    private int _currentCharge;
    private int _currentChargeParticle;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _destroyCollectibles.CollectibleDestroy += HandleCharge;
        _defaultScale = transform.localScale;
    }

    private void Start()
    {
        _currentCharge = 0;
        _currentChargeParticle = 0;
    }

    private void HandleCharge()
    {
        if(_currentCharge < _numberForSurcharge)
        {
            _currentCharge++;
            _currentChargeParticle += _gainParticleEachCharge;
            _vortexVisual.SetInt("SpawnRate", _currentChargeParticle);
            transform.localScale += _gainSizeEachCharge;
        }

        if(_currentCharge >= _numberForSurcharge)
        {
            _currentCharge = 0;
            _currentChargeParticle = 0;
            _vortexVisual.SetInt("SpawnRate", _currentChargeParticle);
            transform.localScale = _defaultScale;
            GameObject newInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
