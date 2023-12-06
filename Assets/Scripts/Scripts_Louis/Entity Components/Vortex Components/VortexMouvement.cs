using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VortexMouvement : MonoBehaviour
{
    [Tooltip("Utilise ou non l'aléatoire de vitesse de déplacement.")]
    [SerializeField] private bool _hasRandomSpeedMovement = false;

    [Tooltip("Cette variable est utile si on ne veut pas de vitesse aléatoire.")]
    [SerializeField] private float _speed = 10f;

    [Header("Intervalle pour le déplacement aléatoire.")]
    [SerializeField] private float _minSpeed = 5f;
    [SerializeField] private float _maxSpeed = 50f;

    [Header("Intervalle de temps entre les changements de vitesse de déplacement")]
    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 6f;

    private Rigidbody _vortexRb;

    // Intervalle entre les changements de vitesse de déplacement.
    private float _intervalTime;

    // speed du vortex à un instant T.
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
            // On appelle une méthode dans un intervalle de temps aléatoire qui va définir la vitesse de déplacement du vortex.
            InvokeRepeating("DefineSpeedVortex", 0, _intervalTime);
        }
        else
        {
            _currentSpeed = _speed;
        }
    }

    private void FixedUpdate()
    {
        // On déplace le Vortex dans sa direction forward à une vitesse définit.
        _vortexRb.velocity = transform.forward * _currentSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Méthode qui permet de définir une current speed pour le mob et va mettre à jour le prochaine intervale de temps ou il sera appelé.
    /// </summary>
    private void DefineSpeedVortex()
    {
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        DefineIntervalTime();
    }

    /// <summary>
    /// Définit un temps d'interval aléatoire pour l'invoke repeating.
    /// </summary>
    private void DefineIntervalTime()
    {
        _intervalTime = Random.Range(_minTime, _maxTime);
    }
}
