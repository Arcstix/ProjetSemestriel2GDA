using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Ce script attach� � un vortex va le charger � chaque fois qu'il d�truit un collectible et son apparence va changer.
public class VortexCharge : MonoBehaviour
{
    [SerializeField] private VisualEffect _vortexVisual;
    [SerializeField] private DestroyCollectibles _destroyCollectibles;
    [SerializeField] private GameObject _purpleExplosionPrefab;
    [SerializeField] private GameObject _redExplosionPrefab;
    [SerializeField] private GameObject _blueExplosionPrefab;

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
        // Partie chargement.
        if(_currentCharge < _numberForSurcharge)
        {
            IncreaseCharge();
        }

        // Partie chargement compl�t�e ou surcharge
        if (_currentCharge >= _numberForSurcharge)
        {
            InstantiateExplosion();
            ResetVortexCharge();
        }
    }

    /// <summary>
    /// M�thode appel� lorsque la charge est compl�te.
    /// </summary>
    private void InstantiateExplosion()
    {
        if(_destroyCollectibles.BlueAmount > _destroyCollectibles.RedAmount)
        {
            Instantiate(_blueExplosionPrefab, transform);
        }
        else if(_destroyCollectibles.BlueAmount < _destroyCollectibles.RedAmount)
        {
            Instantiate(_redExplosionPrefab, transform);
        }
        else
        {
            Instantiate(_purpleExplosionPrefab, transform);
        }
    }

    /// <summary>
    /// M�thode appel� quand on veut charger le vortex
    /// </summary>
    private void IncreaseCharge()
    {
        _currentCharge++;
        _currentChargeParticle += _gainParticleEachCharge;
        _vortexVisual.SetInt("SpawnRate", _currentChargeParticle);
        transform.localScale += _gainSizeEachCharge;
    }

    // La charge est compl�te donc on r�initialise les param�tres.
    private void ResetVortexCharge()
    {
        _currentCharge = 0;
        _currentChargeParticle = 0;
        _vortexVisual.SetInt("SpawnRate", _currentChargeParticle);
        transform.localScale = _defaultScale;
        _destroyCollectibles.BlueAmount = 0;
        _destroyCollectibles.RedAmount = 0;
    }
}
