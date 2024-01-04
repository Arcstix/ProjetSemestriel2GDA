using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollectibleMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _minDistanceToApplyForce = 1f;

    private Transform _currentTarget;
    private Rigidbody _collectibleRb;
    private Vector3 _directionVector;

    private void Awake()
    {
        _collectibleRb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Définit la target principale du Collectible Parent
    /// </summary>
    /// <param name="target"></param>
    public void DefineTarget(Transform target)
    {
        _currentTarget = target;
    }

    private void FixedUpdate()
    {
        if (_currentTarget != null)
        {
            //On calcule le Vecteur direction entre le collectible et la position de la target et on le normalize
            _directionVector = (_currentTarget.position - transform.position).normalized;

            //On calcule la distance entre la position du collectible et de la target pour la vitesse.
            float distance = (_currentTarget.position - transform.position).magnitude;


            //On applique la force au Collectible.
            if (_collectibleRb.velocity.magnitude < _speed && distance > _minDistanceToApplyForce)
            {
                _collectibleRb.AddForce(_directionVector * distance * _speed * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
            //_collectibleRb.velocity = _directionVector * distance * _speed * Time.fixedDeltaTime;
        }
        else
        {
            _collectibleRb.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        // Direction du mouvement
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _directionVector * 3);
    }
}
