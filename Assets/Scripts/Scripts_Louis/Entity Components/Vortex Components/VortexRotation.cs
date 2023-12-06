using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script va permettre de Rotate le mob d'une certaine valeur aléatoire.
public class VortexRotation : MonoBehaviour
{
    [Header("Intervalle de degré de rotation")]
    [SerializeField] private float _minRotate = -90f;
    [SerializeField] private float _maxRotate = 90f;

    [Header("Intervalle de vitesse de rotation")]
    [SerializeField] private float _minSpeed = 5f;
    [SerializeField] private float _maxSpeed = 50f;

    [Header("Intervalle de temps entre chaque rotation")]
    [SerializeField] private float _minTime = 6f;
    [SerializeField] private float _maxTime = 9f;

    private float _currentRotation;
    private float _currentSpeed;
    private float _intervalTime;

    private void Start()
    {
        // On définit un temps d'intervalle initial pour les Invokes. 
        DefineIntervalTime();

        InvokeRepeating("DefineRotation", 0, _intervalTime);
        InvokeRepeating("DefineSpeedRotation", 0, _intervalTime);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _currentRotation * _currentSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Méthode qui permet de définir une current speed pour le mob et va mettre à jour le prochaine intervale de temps ou il sera appelé.
    /// </summary>
    private void DefineRotation()
    {
        _currentRotation = Random.Range(_minRotate, _maxRotate);
    }

    /// <summary>
    /// Méthode qui permet de définir une current speed pour le mob et va mettre à jour le prochaine intervale de temps ou il sera appelé.
    /// </summary>
    private void DefineSpeedRotation()
    {
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        DefineIntervalTime();
    }

    private void DefineIntervalTime()
    {
        _intervalTime = Random.Range(_minTime, _maxTime);
    }
}
