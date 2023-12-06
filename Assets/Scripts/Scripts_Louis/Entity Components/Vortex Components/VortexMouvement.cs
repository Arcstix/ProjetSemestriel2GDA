using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VortexMouvement : MonoBehaviour
{
    [Tooltip("Utilise ou non l'al�atoire de vitesse de d�placement.")]
    [SerializeField] private bool _hasRandomSpeedMovement = false;

    [Tooltip("Cette variable est utile si on ne veut pas de vitesse al�atoire.")]
    [SerializeField] private float _speed = 10f;

    [Header("Intervalle pour le d�placement al�atoire.")]
    [SerializeField] private float _minSpeed = 5f;
    [SerializeField] private float _maxSpeed = 50f;

    [Header("Intervalle de temps entre les changements de vitesse de d�placement")]
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 6f;

    private Rigidbody _vortexRb;

    // Intervalle entre les changements de vitesse de d�placement.
    private float _intervalTime;

    // speed du vortex � un instant T.
    private float _currentSpeed; 

    private void Awake()
    {
        _vortexRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (_hasRandomSpeedMovement)
        {
            DefineIntervalTime();
            // On appelle une m�thode dans un intervalle de temps al�atoire qui va d�finir la vitesse de d�placement du vortex.
            InvokeRepeating("DefineSpeedVortex", 0, _intervalTime);
        }
        else
        {
            _currentSpeed = _speed;
        }
    }

    private void FixedUpdate()
    {
        // On d�place le Vortex dans sa direction forward � une vitesse d�finit.
        _vortexRb.velocity = transform.forward * _currentSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// M�thode qui permet de d�finir une current speed pour le mob et va mettre � jour le prochaine intervale de temps ou il sera appel�.
    /// </summary>
    private void DefineSpeedVortex()
    {
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        DefineIntervalTime();
    }

    /// <summary>
    /// D�finit un temps d'interval al�atoire pour l'invoke repeating.
    /// </summary>
    private void DefineIntervalTime()
    {
        _intervalTime = Random.Range(_minTime, _maxTime);
    }
}
